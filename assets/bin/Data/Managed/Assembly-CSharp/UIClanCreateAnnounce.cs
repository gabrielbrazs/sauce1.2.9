using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClanCreateAnnounce : UIInGameSelfAnnounce
{
	public enum eType
	{
		Create,
		LevelUp
	}

	[SerializeField]
	private UISprite[] announceSpr;

	private const int COUNT_WAIT = 3;

	private const int SE_CLAN_CREATE = 40000011;

	private Coroutine m_coroutine;

	private AudioClip m_audioClip;

	private List<EventItemCounts> m_eventItemCountList = new List<EventItemCounts>();

	private string m_raidBossHp_Str = string.Empty;

	private bool isPlaying;

	private void Start()
	{
		StoreAudioClip();
	}

	private void StoreAudioClip()
	{
		string sE = ResourceName.GetSE(40000011);
		if (!string.IsNullOrEmpty(sE))
		{
			ResourceLink component = this.get_gameObject().GetComponent<ResourceLink>();
			if (!(component == null))
			{
				m_audioClip = component.Get<AudioClip>(sE);
			}
		}
	}

	private void PlayAudioKnockDown()
	{
		if (!(m_audioClip == null))
		{
			SoundManager.PlayOneshotJingle(m_audioClip, 40000011);
		}
	}

	private bool IsAbleToPlay()
	{
		if (MonoBehaviourSingleton<UIManager>.I.IsTransitioning())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.isChangeing)
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.IsExecutionAutoEvent())
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameProgress>.IsValid() && MonoBehaviourSingleton<InGameProgress>.I.isGameProgressStop)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestHappen)
		{
			return false;
		}
		if (MonoBehaviourSingleton<InGameManager>.IsValid() && MonoBehaviourSingleton<InGameManager>.I.isQuestFromGimmick)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.isNoticeNewDeliveryAtHomeScene)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableStoryDelivery() != 0)
		{
			return false;
		}
		if (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.GetEventCleardDeliveryData() != null)
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsWaitDelay())
		{
			return false;
		}
		if (MonoBehaviourSingleton<UIManager>.I.levelUp.IsPlaying())
		{
			return false;
		}
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "ClanScene" && MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName() == "ClanTop")
		{
			return true;
		}
		return false;
	}

	public void SetEventItemCountList(List<EventItemCounts> eventItemCounts)
	{
		m_eventItemCountList = eventItemCounts;
	}

	public bool IsKnockDownRaidBossByEventItemCountList()
	{
		if (m_eventItemCountList.IsNullOrEmpty())
		{
			return false;
		}
		int i = 0;
		for (int count = m_eventItemCountList.Count; i < count; i++)
		{
			if (m_eventItemCountList[i].eventType == 28)
			{
				long result = 0L;
				long result2 = 0L;
				if (long.TryParse(m_eventItemCountList[i].maxCount, out result) && long.TryParse(m_eventItemCountList[i].count, out result2) && result2 >= result)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsKnockDownRaidBossByRaidBossHp()
	{
		if (string.IsNullOrEmpty(m_raidBossHp_Str))
		{
			return false;
		}
		long result = 0L;
		if (!long.TryParse(m_raidBossHp_Str, out result))
		{
			return false;
		}
		if (result <= 0)
		{
			return true;
		}
		return false;
	}

	private void ClearKnockDownData()
	{
		m_eventItemCountList.Clear();
		m_raidBossHp_Str = string.Empty;
	}

	public void Play(bool isForcePlay = false, Action callback = null, eType type = eType.Create)
	{
		if (!TutorialStep.HasQuestSpecialUnlocked())
		{
			return;
		}
		SetTypeSprite(type);
		if (!isForcePlay && !IsAbleToPlay())
		{
			if (m_coroutine == null)
			{
				m_coroutine = this.StartCoroutine(DelayPlay());
			}
			return;
		}
		if (isForcePlay && m_coroutine != null)
		{
			this.StopCoroutine(m_coroutine);
			m_coroutine = null;
		}
		Play(callback);
		PlayAudioKnockDown();
		ClearKnockDownData();
	}

	private IEnumerator DelayPlay()
	{
		int waitCount = 0;
		while (!IsAbleToPlay() || waitCount < 3)
		{
			waitCount = (IsAbleToPlay() ? (waitCount + 1) : 0);
			yield return null;
		}
		Play(null);
		PlayAudioKnockDown();
		PlayerPrefs.SetInt("IS_SHOWED_RAID_BOSS_DIRECTION", 1);
		m_coroutine = null;
		ClearKnockDownData();
	}

	public void ClearAnnounce()
	{
		if (m_coroutine != null)
		{
			this.StopCoroutine(m_coroutine);
			m_coroutine = null;
		}
	}

	private void SetTypeSprite(eType type)
	{
		string empty = string.Empty;
		switch (type)
		{
		default:
			return;
		case eType.Create:
			empty = "ClanFormationTxt";
			break;
		case eType.LevelUp:
			empty = "ClanLevelupTxt";
			break;
		}
		int i = 0;
		for (int num = announceSpr.Length; i < num; i++)
		{
			announceSpr[i].spriteName = empty;
			announceSpr[i].SetDirty();
		}
	}
}