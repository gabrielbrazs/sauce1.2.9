using System;
using System.Collections;
using UnityEngine;

public class SmithAbilityItemResult : GameSection
{
	private enum UI
	{
		TEX_MODEL,
		STR_NEXT_REFLECT,
		OBJ_DELAY_1,
		OBJ_DELAY_2,
		OBJ_ABILITY_LIST_ROOT,
		OBJ_DIRECTION_1,
		LBL_ADD_ABILITY_1,
		BTN_TAP_FULL_SCREEN
	}

	private EquipItemInfo equipItemInfo;

	private AbilityChangeAbilityList abilityList;

	private int endDirectionCount;

	private bool m_isFirstOpen;

	private bool isFirstTap = true;

	public override string overrideBackKeyEvent => "GO_BACK";

	public override void Initialize()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		Transform val = SetPrefab((Enum)UI.OBJ_ABILITY_LIST_ROOT, "AbilityChangeAbilityList");
		abilityList = val.get_gameObject().AddComponent<AbilityChangeAbilityList>();
		abilityList.uiVisible = true;
		SetRenderEquipModel((Enum)UI.TEX_MODEL, equipItemInfo.tableID, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex, -1, 1f);
		SetLabelText((Enum)UI.STR_NEXT_REFLECT, base.sectionData.GetText("STR_NEXT"));
		ResetTween((Enum)UI.OBJ_DELAY_2, 0);
		MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = false;
		SetFullScreenButton((Enum)UI.BTN_TAP_FULL_SCREEN);
		m_isFirstOpen = true;
		base.Initialize();
	}

	protected override void OnOpen()
	{
		abilityList.InitUI();
		abilityList.Open(UITransition.TYPE.OPEN);
		PlayTween(callback: PlayDirection, ctrl_enum: UI.OBJ_DELAY_1, forward: true, is_input_block: true, tween_ctrl_id: 0);
		SetActive((Enum)UI.OBJ_DELAY_2, false);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		abilityList.SetParameter(equipItemInfo);
	}

	private void EndAllDirection()
	{
		SetActive((Enum)UI.OBJ_DELAY_2, true);
		ResetTween((Enum)UI.OBJ_DELAY_2, 0);
		PlayTween((Enum)UI.OBJ_DELAY_2, true, (EventDelegate.Callback)null, true, 0);
		SetActive((Enum)UI.TEX_MODEL, false);
		RefreshUI();
	}

	private void PlayDirection()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_PlayDirection());
	}

	private IEnumerator _PlayDirection()
	{
		PlayDirection(UI.OBJ_DIRECTION_1, UI.LBL_ADD_ABILITY_1, equipItemInfo.GetAbilityItem().GetName());
		yield return (object)new WaitForSeconds(0.46f);
		if (m_isFirstOpen)
		{
			SoundManager.PlayOneShotUISE(40000049);
		}
		m_isFirstOpen = false;
	}

	private void PlayDirection(UI directionObj, UI label, string text)
	{
		SetFontStyle((Enum)label, 2);
		SetLabelText((Enum)label, text);
		PlayTween(callback: EndAllDirection, ctrl_enum: directionObj, forward: true, is_input_block: false, tween_ctrl_id: 0);
	}

	private void OnQuery_NEXT()
	{
		GameSection.SetEventData(equipItemInfo);
	}

	private void OnQuery_GO_BACK()
	{
		if (!MonoBehaviourSingleton<GameSceneManager>.I.GetHistoryList().Exists((GameSectionHistory.HistoryData x) => x.sectionName == "SmithAbilityChangeSelect"))
		{
			GameSection.StopEvent();
			DispatchEvent("MAIN_MENU_STATUS", null);
		}
	}

	private void OnQuery_TAP_FULL_SCREEN()
	{
		if (isFirstTap)
		{
			isFirstTap = false;
			EquipItemAbility[] lotteryAbility = equipItemInfo.GetLotteryAbility();
			UITweenCtrl.SetDurationWithRate(GetCtrl(UI.OBJ_DIRECTION_1), 0.5f, 0);
		}
	}

	protected void OnQuery_LOTTERY_LIST()
	{
		EquipItemInfo selectEquipData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		GameSection.SetEventData(new object[2]
		{
			selectEquipData,
			SmithEquipBase.SmithType.ABILITY_CHANGE
		});
	}
}
