using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClanRequestSelect : GameSection
{
	protected enum UI
	{
		TEX_NPCMODEL,
		LBL_DELIVERY_NON_LIST,
		STR_QUEST_NON_LIST,
		GRD_DELIVERY,
		GRD_QUEST,
		LBL_QUEST_NAME,
		LBL_QUEST_TIME,
		SPR_QUESTBOARD_BANNER_ON,
		SPR_QUESTBOARD_BANNER_OFF,
		QuestExploreRequestItemToSearch
	}

	private List<ClanDelivery> questList = new List<ClanDelivery>();

	public override void Initialize()
	{
		RequestDeliveryQuest(delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success);
		});
		if (SpecialDeviceManager.HasSpecialDeviceInfo && SpecialDeviceManager.SpecialDeviceInfo.NeedClanRequestToQuestBoard)
		{
			DeviceIndividualInfo specialDeviceInfo = SpecialDeviceManager.SpecialDeviceInfo;
			Transform ctrl = GetCtrl(UI.QuestExploreRequestItemToSearch);
			if (ctrl != null)
			{
				UIWidget component = ctrl.GetComponent<UIWidget>();
				component.leftAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.left;
				component.rightAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.right;
				component.bottomAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.bottom;
				component.topAnchor.absolute = specialDeviceInfo.ClanRequestToQuestBoardAnchor.top;
				component.UpdateAnchors();
			}
		}
		this.StartCoroutine(DoInitialize());
	}

	protected virtual IEnumerator DoInitialize()
	{
		base.Initialize();
		yield return null;
	}

	public override void UpdateUI()
	{
		SetQuestBoardAccess();
		SetDailyQuestList();
	}

	private void SetDailyQuestList()
	{
		if (questList != null)
		{
			if (questList == null || questList.Count == 0)
			{
				SetActive((Enum)UI.GRD_QUEST, is_visible: false);
				SetActive((Enum)UI.STR_QUEST_NON_LIST, is_visible: true);
			}
			else
			{
				SetActive((Enum)UI.STR_QUEST_NON_LIST, is_visible: false);
				SetActive((Enum)UI.GRD_DELIVERY, is_visible: true);
				SetDynamicList((Enum)UI.GRD_DELIVERY, "ClanRequestItem", questList.Count, reset: false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
				{
					SetActive(t, is_visible: true);
					if (!Object.op_Implicit(t.get_transform().GetComponent<ClanRequestItem>()))
					{
						t.get_gameObject().AddComponent<ClanRequestItem>();
					}
					t.get_transform().GetComponent<ClanRequestItem>().Setup(t, questList[i]);
				});
			}
		}
	}

	public void RequestDeliveryQuest(Action<bool> call_back)
	{
		Protocol.Send(ClanDeliveryModel.URL, delegate(ClanDeliveryModel ret)
		{
			questList = ret.result.deliveryList;
			call_back(ret.Error == Error.None);
		}, string.Empty);
	}

	private void SetQuestBoardAccess()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
		{
			SetActive((Enum)UI.SPR_QUESTBOARD_BANNER_ON, is_visible: true);
			SetActive((Enum)UI.SPR_QUESTBOARD_BANNER_OFF, is_visible: false);
		}
		else
		{
			SetActive((Enum)UI.SPR_QUESTBOARD_BANNER_ON, is_visible: false);
			SetActive((Enum)UI.SPR_QUESTBOARD_BANNER_OFF, is_visible: true);
		}
	}

	private void OnQuery_CLAN_QUEST()
	{
		if (MonoBehaviourSingleton<ClanMatchingManager>.I.userClanData.level >= 2)
		{
			EventData[] autoEvents = new EventData[1]
			{
				new EventData("CLAN_QUEST_COUNTER", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
		}
		else
		{
			EventData[] autoEvents2 = new EventData[1]
			{
				new EventData("CLAN_QUEST_COUNTER_OFF", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents2);
		}
	}
}