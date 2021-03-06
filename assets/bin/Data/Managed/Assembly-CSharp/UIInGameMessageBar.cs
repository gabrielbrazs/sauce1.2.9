using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameMessageBar : MonoBehaviourSingleton<UIInGameMessageBar>
{
	public class AnnounceInfo
	{
		public string name;

		public string messeage;

		public int stampId;
	}

	[SerializeField]
	protected Transform tweenCtrl;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected UILabel nameLabel;

	[SerializeField]
	protected UILabel messeageLabel;

	[SerializeField]
	protected float dispTime = 2f;

	[SerializeField]
	private UITexture texStamp;

	[SerializeField]
	private UIWidget anchor;

	private bool isOpen;

	private bool isLock;

	private float lockTimer;

	private List<AnnounceInfo> announceQueue = new List<AnnounceInfo>();

	private List<AnnounceInfo> announceStock = new List<AnnounceInfo>();

	private IEnumerator coroutineLoadStamp;

	protected override void Awake()
	{
		base.Awake();
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate += OnScreenRotate;
		}
	}

	protected override void _OnDestroy()
	{
		if (MonoBehaviourSingleton<ScreenOrientationManager>.IsValid())
		{
			MonoBehaviourSingleton<ScreenOrientationManager>.I.OnScreenRotate -= OnScreenRotate;
		}
	}

	private void _AnnounceStart()
	{
		UITweenCtrl.Reset(tweenCtrl, 0);
		UITweenCtrl.Play(tweenCtrl, true, delegate
		{
			isLock = true;
		}, false, 0);
		lockTimer = dispTime;
		isOpen = true;
	}

	private void _Announce(string name, string messeage, int stamp_id)
	{
		if (isOpen || announceQueue.Count > 0)
		{
			AnnounceInfo announceInfo = null;
			if (announceStock.Count > 0)
			{
				announceInfo = announceStock[0];
				announceStock.RemoveAt(0);
			}
			else
			{
				announceInfo = new AnnounceInfo();
			}
			announceInfo.name = name;
			announceInfo.messeage = messeage;
			announceInfo.stampId = stamp_id;
			announceQueue.Add(announceInfo);
		}
		else
		{
			if (stamp_id != 0)
			{
				_AnnounceStamp(name, stamp_id);
			}
			else
			{
				_AnnounceMesseage(name, messeage);
			}
			panelChange.UnLock();
		}
	}

	private void _AnnounceMesseage(string name, string messeage)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		nameLabel.text = name;
		messeageLabel.get_gameObject().SetActive(true);
		messeageLabel.text = messeage;
		texStamp.get_gameObject().SetActive(false);
		_AnnounceStart();
	}

	private void _AnnounceStamp(string name, int stamp_id)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		coroutineLoadStamp = CoroutineLoadStamp(name, stamp_id);
		this.StartCoroutine(coroutineLoadStamp);
	}

	private IEnumerator CoroutineLoadStamp(string name, int stampId)
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo_stamp = load_queue.LoadChatStamp(stampId, false);
		yield return (object)load_queue.Wait();
		if (!(lo_stamp.loadedObject == null))
		{
			Texture2D stamp = lo_stamp.loadedObject as Texture2D;
			texStamp.get_gameObject().SetActive(true);
			texStamp.mainTexture = stamp;
			nameLabel.text = name;
			messeageLabel.get_gameObject().SetActive(false);
			coroutineLoadStamp = null;
			_AnnounceStart();
		}
	}

	public void Announce(string name, string messeage)
	{
		_Announce(name, messeage, 0);
		panelChange.UnLock();
	}

	public void Announce(string name, int stamp_id)
	{
		_Announce(name, null, stamp_id);
		panelChange.UnLock();
	}

	private void LateUpdate()
	{
		if (coroutineLoadStamp == null && isLock)
		{
			lockTimer -= Time.get_deltaTime();
			if (!(lockTimer > 0f))
			{
				if (isOpen)
				{
					if (!NextAnnounce())
					{
						isOpen = false;
						UITweenCtrl.Play(tweenCtrl, false, delegate
						{
							isLock = true;
						}, false, 0);
						lockTimer = 0.1f;
					}
				}
				else if (!NextAnnounce())
				{
					panelChange.Lock();
				}
				isLock = false;
			}
		}
	}

	private bool NextAnnounce()
	{
		if (announceQueue.Count <= 0)
		{
			return false;
		}
		if (announceQueue[0].stampId != 0)
		{
			_AnnounceStamp(announceQueue[0].name, announceQueue[0].stampId);
		}
		else
		{
			_AnnounceMesseage(announceQueue[0].name, announceQueue[0].messeage);
		}
		announceStock.Add(announceQueue[0]);
		announceQueue.RemoveAt(0);
		return true;
	}

	private void OnScreenRotate(bool isPortrait)
	{
		anchor.UpdateAnchors();
	}
}
