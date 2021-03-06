using UnityEngine;

public class HomeNPCCharacter : HomeCharacterBase
{
	private const float LoungeBorudonScaleRate = 1.3f;

	private NPCTable.NPCData npcData;

	public OutGameSettingsManager.HomeScene.NPC npcInfo
	{
		get;
		private set;
	}

	public PLCA nearAnim
	{
		get;
		private set;
	}

	public void SetNPCInfo(OutGameSettingsManager.HomeScene.NPC npcInfo)
	{
		this.npcInfo = npcInfo;
	}

	public void SetNPCData(NPCTable.NPCData data)
	{
		npcData = data;
	}

	protected override ModelLoaderBase LoadModel()
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		bool useSpecialModel = false;
		HomeThemeTable.HomeThemeData homeThemeData = Singleton<HomeThemeTable>.I.GetHomeThemeData(Singleton<HomeThemeTable>.I.CurrentHomeTheme);
		if (homeThemeData != null && (npcData.specialModelID > 0 || homeThemeData.name != "NORMAL"))
		{
			useSpecialModel = true;
		}
		return npcData.LoadModel(this.get_gameObject(), true, true, null, useSpecialModel);
	}

	protected override void InitCollider()
	{
		if (!string.IsNullOrEmpty(npcInfo.eventName))
		{
			base.InitCollider();
		}
		else
		{
			SetCollider(0.3f, 0.1f);
		}
	}

	protected override void ChangeScale()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<LoungeManager>.IsValid() && npcInfo.npcID == 4)
		{
			Vector3 localScale = this.get_transform().get_localScale();
			float num = 1.3f;
			this.get_transform().set_localScale(new Vector3(localScale.x * num, localScale.y * num, localScale.z * num));
		}
	}

	protected override void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_01;
		string loopAnim = npcInfo.GetLoopAnim();
		if (!string.IsNullOrEmpty(loopAnim))
		{
			default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
		}
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, OnAnimPlay, null, base.OnAnimEnd);
		string nearAnim = npcInfo.GetNearAnim();
		if (!string.IsNullOrEmpty(nearAnim))
		{
			this.nearAnim = PlayerAnimCtrl.StringToEnum(nearAnim);
		}
		else
		{
			this.nearAnim = PLCA.IDLE_01;
		}
	}

	public void Play(PLCA anim, bool instant)
	{
		if (animCtrl == null)
		{
			InitAnim();
		}
		animCtrl.Play(anim, instant);
	}

	public override bool DispatchEvent()
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		if (!TutorialStep.HasAllTutorialCompleted() || MonoBehaviourSingleton<UIManager>.I.IsEnableTutorialMessage() || TutorialMessage.GetCursor(0) != null)
		{
			return false;
		}
		if (HomeBase.OnAfterGacha2Tutorial)
		{
			return false;
		}
		if (state == STATE.FREE && npcInfo != null && !string.IsNullOrEmpty(npcInfo.eventName))
		{
			MonoBehaviourSingleton<GameSceneManager>.I.ExecuteSceneEvent("HomeNPCCharacter", this.get_gameObject(), npcInfo.eventName, null, null, true);
			return true;
		}
		return false;
	}

	public void SetQuestBalloon(Transform t)
	{
		t.set_name(HomeBase.QuestBalloonName);
		namePlate = t;
	}

	protected override bool IsVisibleNamePlate()
	{
		if (state != 0)
		{
			return false;
		}
		return base.IsVisibleNamePlate();
	}

	public bool IsLeaveState()
	{
		return state == STATE.LEAVE;
	}

	public void HideShadow()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		NPCLoader nPCLoader = base.loader as NPCLoader;
		if (Object.op_Implicit(nPCLoader))
		{
			if (Object.op_Implicit(nPCLoader.shadow))
			{
				nPCLoader.shadow.get_gameObject().SetActive(false);
			}
		}
		else
		{
			Transform val = base._transform.Find("CircleShadow");
			if (null != val)
			{
				val.get_gameObject().SetActive(false);
			}
		}
	}
}
