using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSpecialSelect : GameSection
{
	protected enum UI
	{
		TEX_NPCMODEL,
		OBJ_NPC_MESSAGE,
		LBL_NPC_MESSAGE,
		BTN_SEARCH,
		BTN_EVENT,
		BTN_SORT,
		LBL_SORT,
		TGL_ICON_ASC,
		SPR_FRAME,
		SPR_BG_BTN_CLOSE,
		BTN_INPUT_CLOSE,
		BTN_INPUT_CLOSE_BG,
		BG,
		TIELEBAR,
		FRAMEDOWN,
		FRAMEUP,
		LIST_BASE,
		DELIVERY_SCROLLBAR_OVER,
		DELIVERY_SCROLLBAR_BASE,
		OBJ_ORDER_ROOT,
		BTN_ORDER,
		SPR_ORDER_TEXT,
		SPR_ORDER_ICON,
		OBJ_DELIVERY_ROOT,
		BTN_DELIVERY,
		SPR_DELIVERY_TEXT,
		SPR_DELIVERY_ICON,
		OBJ_QUEST_ROOT,
		BTN_QUEST,
		SPR_QUEST_TEXT,
		SPR_QUEST_ICON,
		STR_ORDER_NON_LIST,
		GRD_ORDER_QUEST,
		SCR_ORDER_QUEST2,
		LBL_DELIVERY_NON_LIST,
		SCR_DELIVERY_QUEST,
		GRD_DELIVERY,
		BTN_TAB_NORMAL,
		BTN_TAB_DAILY,
		BTN_TAB_WEEKLY,
		SPR_NEW,
		SPR_TAB_NORMAL,
		SPR_TAB_DAILY,
		SPR_TAB_WEEKLY,
		GRD_QUEST,
		STR_QUEST_NON_LIST,
		GRD_ICON_ROOT,
		OBJ_ICON_ROOT_1,
		OBJ_ICON_ROOT_2,
		OBJ_BUTTON_ROOT,
		SCR_ORDER_QUEST,
		SPR_ORDER_RARITY_FRAME,
		LBL_ORDER_NUM,
		LBL_REMAIN,
		TEX_NPC,
		LBL_DELIVERY_COMMENT,
		LBL_NEED_ITEM_NAME,
		LBL_HAVE,
		LBL_NEED,
		LBL_LIMIT,
		OBJ_REQUEST_OK,
		OBJ_REQUEST_COMPLETED,
		SPR_TYPE_STORY,
		SPR_TYPE_EVENT,
		SPR_TYPE_EVENT_TEXT,
		SPR_TYPE_DAILY_TEXT,
		SPR_TYPE_WEEKLY_TEXT,
		SPR_TYPE_HARD,
		SPR_TYPE_NORMAL,
		SPR_TYPE_SUB_EVENT,
		SPR_DIFFICULTY_EASY,
		SPR_DIFFICULTY_NORMAL,
		SPR_DIFFICULTY_HARD,
		SPR_DROP_DIFFICULTY_RARE,
		SPR_DROP_DIFFICULTY_SUPER_RARE,
		SCR_NORMAL_QUEST,
		OBJ_ROT_SPRITE,
		LBL_QUEST_NAME,
		LBL_QUEST_NUM,
		OBJ_MISSION_INFO_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_ENEMY,
		SPR_MONSTER_ICON,
		SPR_ELEMENT_ROOT,
		SPR_ELEMENT,
		SPR_WEAK_ELEMENT,
		STR_NON_WEAK_ELEMENT,
		TWN_DIFFICULT_STAR,
		OBJ_DIFFICULT_STAR_1,
		OBJ_DIFFICULT_STAR_2,
		OBJ_DIFFICULT_STAR_3,
		OBJ_DIFFICULT_STAR_4,
		OBJ_DIFFICULT_STAR_5,
		OBJ_DIFFICULT_STAR_6,
		OBJ_DIFFICULT_STAR_7,
		OBJ_DIFFICULT_STAR_8,
		OBJ_DIFFICULT_STAR_9,
		OBJ_DIFFICULT_STAR_10,
		OBJ_ICON,
		OBJ_ICON_NEW,
		OBJ_ICON_CLEARED,
		OBJ_ICON_COMPLETE,
		SPR_ICON_NEW,
		SPR_ICON_CLEARED,
		SPR_ICON_COMPLETE,
		OBJ_BANNER_ROOT,
		SPR_BANNER,
		PORTRAIT_SPR_BG_BTN_CLOSE,
		PORTRAIT_BTN_INPUT_CLOSE,
		PORTRAIT_BTN_INPUT_CLOSE_BG,
		PORTRAIT_OBJ_BUTTON_ROOT,
		PORTRAIT_BG,
		PORTRAIT_TIELEBAR,
		PORTRAIT_FRAMEDOWN,
		PORTRAIT_FRAMEUP,
		PORTRAIT_OBJ_DELIVERY_ROOT,
		PORTRAIT_LIST_BASE,
		PORTRAIT_STR_DELIVERY_NON_LIST,
		PORTRAIT_SCR_DELIVERY_QUEST,
		PORTRAIT_DELIVERY_SCROLLBAR,
		LANDSCAPE_SPR_BG_BTN_CLOSE,
		LANDSCAPE_BTN_INPUT_CLOSE,
		LANDSCAPE_BTN_INPUT_CLOSE_BG,
		LANDSCAPE_OBJ_BUTTON_ROOT,
		LANDSCAPE_BG,
		LANDSCAPE_TIELEBAR,
		LANDSCAPE_FRAMEDOWN,
		LANDSCAPE_FRAMEUP,
		LANDSCAPE_OBJ_DELIVERY_ROOT,
		LANDSCAPE_LIST_BASE,
		LANDSCAPE_STR_DELIVERY_NON_LIST,
		LANDSCAPE_SCR_DELIVERY_QUEST,
		LANDSCAPE_DELIVERY_SCROLLBAR,
		TEX_AREA_BANNER,
		SPR_CLEARED_AREA,
		SPR_NEW_AREA,
		GRD_AREA,
		OBJ_DELIVERY_BAR,
		OBJ_AREA_BAR
	}

	public enum SHOW_MODE
	{
		DELIVERY,
		ORDER,
		QUEST
	}

	private struct AreaQuestInfo
	{
		public RegionTable.Data regionData;

		public Texture2D bannerTex;

		public bool cleared;

		public AreaQuestInfo(RegionTable.Data data, Texture2D tex, bool cleared)
		{
			regionData = data;
			bannerTex = tex;
			this.cleared = cleared;
		}
	}

	private const string SPR_QUEST_TAB_TEXT = "QuestTabBtnText";

	private const string SPR_QUEST_TAB_ICON = "QuestTabBtnIcon";

	private const string SPR_QUEST_TAB_BASE = "QuestTabBtnBase";

	private const string spriteTabName_ON = "PickeShopBtn_Green_on";

	private const string spriteTabName_OFF = "PickeShopBtn_Normal_off";

	protected SHOW_MODE showMode;

	private readonly string[] SPR_INDEX = new string[3]
	{
		"01",
		"02",
		"03"
	};

	private readonly string[] SPR_ON_OFF = new string[3]
	{
		"_on",
		"_off",
		"_Gray"
	};

	private SortSettings sortSettings;

	private QuestSortData[] questSortData;

	private QuestItemInfo[] questItemAry;

	protected Delivery[] deliveryInfo;

	protected Delivery[] normalDeliveryInfo;

	private Delivery[] dailyDeliveryInfo;

	private Delivery[] weeklyDeliveryInfo;

	private readonly string[] SPR_FRAME_TYPE = new string[5]
	{
		"RequestPlate_Base",
		"RequestPlate_Event",
		"RequestPlate_Story",
		"RequestPlate_Hard",
		"RequestPlate_SubEvent"
	};

	private QuestInfoData[] questInfo;

	private UI[] ui_top_crown = new UI[3]
	{
		UI.OBJ_TOP_CROWN_1,
		UI.OBJ_TOP_CROWN_2,
		UI.OBJ_TOP_CROWN_3
	};

	private UI[] ui_crown = new UI[3]
	{
		UI.SPR_CROWN_1,
		UI.SPR_CROWN_2,
		UI.SPR_CROWN_3
	};

	protected bool isInGameScene;

	private string npcText = string.Empty;

	private UI[] difficult = new UI[10]
	{
		UI.OBJ_DIFFICULT_STAR_1,
		UI.OBJ_DIFFICULT_STAR_2,
		UI.OBJ_DIFFICULT_STAR_3,
		UI.OBJ_DIFFICULT_STAR_4,
		UI.OBJ_DIFFICULT_STAR_5,
		UI.OBJ_DIFFICULT_STAR_6,
		UI.OBJ_DIFFICULT_STAR_7,
		UI.OBJ_DIFFICULT_STAR_8,
		UI.OBJ_DIFFICULT_STAR_9,
		UI.OBJ_DIFFICULT_STAR_10
	};

	private DELIVERY_TYPE[][] TAB_TYPES = new DELIVERY_TYPE[3][]
	{
		new DELIVERY_TYPE[2]
		{
			DELIVERY_TYPE.STORY,
			DELIVERY_TYPE.ONCE
		},
		new DELIVERY_TYPE[1],
		new DELIVERY_TYPE[1]
		{
			DELIVERY_TYPE.WEEKLY
		}
	};

	protected UI selectedTab = UI.BTN_TAB_NORMAL;

	private bool isDeliveryGridReset = true;

	private List<Texture2D> areaBanners = new List<Texture2D>();

	private uint[] openRegionIds;

	protected bool changeToDeliveryClearEvent;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "DeliveryRewardTable";
			yield return "FieldMapTable";
		}
	}

	public override void Initialize()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		isInGameScene = (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "InGameScene");
		this.StartCoroutine(DoInitialize());
	}

	protected virtual IEnumerator DoInitialize()
	{
		LoadingQueue load_queue = new LoadingQueue(this);
		if (!isInGameScene)
		{
			bool is_recv_delivery = false;
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_new");
			load_queue.CacheEffect(RESOURCE_CATEGORY.EFFECT_UI, "ef_ui_questselect_complete");
			MonoBehaviourSingleton<QuestManager>.I.SendGetDeliveryList(delegate
			{
				((_003CDoInitialize_003Ec__IteratorB2)/*Error near IL_007a: stateMachine*/)._003Cis_recv_delivery_003E__1 = true;
			});
			while (!is_recv_delivery)
			{
				yield return (object)null;
			}
			sortSettings = SortSettings.CreateMemSortSettings(SortBase.DIALOG_TYPE.QUEST, SortSettings.SETTINGS_TYPE.ORDER_QUEST);
			InitQuestInfoData();
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
		}
		GetDeliveryList();
		string tab = GameSection.GetEventData() as string;
		switch (tab)
		{
		case "DAILY":
			selectedTab = UI.BTN_TAB_DAILY;
			break;
		case "WEEKLY":
			selectedTab = UI.BTN_TAB_WEEKLY;
			break;
		case "NORMAL":
			selectedTab = UI.BTN_TAB_NORMAL;
			break;
		default:
			selectedTab = SelectTabByPriority();
			break;
		}
		openRegionIds = MonoBehaviourSingleton<WorldMapManager>.I.GetOpenRegionIdListInWorldMap(REGION_DIFFICULTY_TYPE.NORMAL);
		Array.Reverse(openRegionIds);
		for (int i = 0; i < openRegionIds.Length; i++)
		{
			int regionId = (int)openRegionIds[i];
			string bannerName = ResourceName.GetAreaBanner(regionId);
			LoadObject bannerObj = load_queue.Load(RESOURCE_CATEGORY.AREA_BANNER, bannerName, false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			Texture2D bannerTex = bannerObj.loadedObject as Texture2D;
			areaBanners.Add(bannerTex);
		}
		EndInitialize();
	}

	private UI SelectTabByPriority()
	{
		if (normalDeliveryInfo != null)
		{
			int i = 0;
			for (int num = normalDeliveryInfo.Length; i < num; i++)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)normalDeliveryInfo[i].dId);
				if (deliveryTableData.type == DELIVERY_TYPE.STORY)
				{
					return UI.BTN_TAB_NORMAL;
				}
			}
		}
		if (dailyDeliveryInfo != null && dailyDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_DAILY;
		}
		if (normalDeliveryInfo != null && normalDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_NORMAL;
		}
		if (weeklyDeliveryInfo != null && weeklyDeliveryInfo.Length >= 1)
		{
			return UI.BTN_TAB_WEEKLY;
		}
		return UI.BTN_TAB_DAILY;
	}

	protected void EndInitialize()
	{
		base.Initialize();
	}

	private void InitQuestInfoData()
	{
		questInfo = MonoBehaviourSingleton<QuestManager>.I.GetQuestInfoData();
		if (questInfo != null)
		{
			Array.Sort(questInfo, (QuestInfoData l, QuestInfoData r) => (int)(r.questData.tableData.questID - l.questData.tableData.questID));
		}
	}

	private void Update()
	{
		if (MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime > 0f && MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime > 0f)
		{
			MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime -= Time.get_deltaTime();
			MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime -= Time.get_deltaTime();
			ShowNonDeliveryList();
		}
	}

	public override void UpdateUI()
	{
		if (!changeToDeliveryClearEvent)
		{
			ShowSelectUI();
			UpdateAnchors();
			SetBadge((Enum)UI.BTN_EVENT, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableEventDeliveryNum(), 1, 24, -10, true);
			SetBadge((Enum)UI.BTN_TAB_NORMAL, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[0]), 3, -10, -10, true);
			SetBadge((Enum)UI.BTN_TAB_DAILY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[1]), 3, -10, -10, true);
			SetBadge((Enum)UI.BTN_TAB_WEEKLY, MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableDeliveryNum(TAB_TYPES[2]), 3, -10, -10, true);
			SetNew(UI.BTN_TAB_DAILY, GameSaveData.instance.IsRecommendedDailyDeliveryCheck());
			SetNew(UI.BTN_TAB_WEEKLY, GameSaveData.instance.IsRecommendedWeeklyDeliveryCheck());
		}
	}

	private void SetNew(UI btn, bool is_visible)
	{
		SetActive(GetCtrl(btn), UI.SPR_NEW, is_visible);
	}

	private void OpenTutorial()
	{
		if (!isInGameScene && HomeTutorialManager.DoesTutorial())
		{
			MonoBehaviourSingleton<UIManager>.I.tutorialMessage.ForceRun("HomeScene", "TutorialStep2_1", null);
		}
	}

	protected void SetupDeliveryListItem(Transform t, DeliveryTable.DeliveryData info)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		QuestRequestItem questRequestItem = t.GetComponent<QuestRequestItem>();
		if (questRequestItem == null)
		{
			questRequestItem = t.get_gameObject().AddComponent<QuestRequestItem>();
		}
		questRequestItem.InitUI();
		questRequestItem.Setup(t, info);
	}

	protected void SetCompletedHaveCount(Transform t, DeliveryTable.DeliveryData info)
	{
		MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryDataAllNeeds((int)info.id, out int _, out int need, out string _, out string _);
		SetLabelText(t, UI.LBL_HAVE, need.ToString());
	}

	protected virtual void ShowSelectUI()
	{
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		int num = (!isInGameScene && TutorialStep.HasQuestSpecialUnlocked()) ? 1 : 2;
		int num2 = (showMode != 0) ? num : 0;
		int num3 = (showMode != SHOW_MODE.QUEST) ? num : 0;
		int num4 = (showMode != SHOW_MODE.ORDER) ? num : 0;
		SetActive((Enum)UI.OBJ_DELIVERY_ROOT, showMode == SHOW_MODE.DELIVERY);
		SetActive((Enum)UI.OBJ_QUEST_ROOT, showMode == SHOW_MODE.QUEST);
		SetActive((Enum)UI.OBJ_ORDER_ROOT, showMode == SHOW_MODE.ORDER);
		SetButtonSprite((Enum)UI.BTN_DELIVERY, "QuestTabBtnBase" + SPR_ON_OFF[num2], true);
		SetSprite((Enum)UI.SPR_DELIVERY_TEXT, "QuestTabBtnText" + SPR_INDEX[0] + SPR_ON_OFF[num2]);
		SetSprite((Enum)UI.SPR_DELIVERY_ICON, "QuestTabBtnIcon" + SPR_INDEX[0] + SPR_ON_OFF[num2]);
		SetButtonSprite((Enum)UI.BTN_QUEST, "QuestTabBtnBase" + SPR_ON_OFF[num3], true);
		SetSprite((Enum)UI.SPR_QUEST_TEXT, "QuestTabBtnText" + SPR_INDEX[1] + SPR_ON_OFF[num3]);
		SetSprite((Enum)UI.SPR_QUEST_ICON, "QuestTabBtnIcon" + SPR_INDEX[1] + SPR_ON_OFF[num3]);
		SetButtonSprite((Enum)UI.BTN_ORDER, "QuestTabBtnBase" + SPR_ON_OFF[num4], true);
		SetSprite((Enum)UI.SPR_ORDER_TEXT, "QuestTabBtnText" + SPR_INDEX[2] + SPR_ON_OFF[num4]);
		SetSprite((Enum)UI.SPR_ORDER_ICON, "QuestTabBtnIcon" + SPR_INDEX[2] + SPR_ON_OFF[num4]);
		if (!TutorialStep.HasQuestSpecialUnlocked())
		{
			SetButtonEnabled((Enum)UI.BTN_QUEST, false);
			SetButtonEnabled((Enum)UI.BTN_ORDER, false);
			SetActive((Enum)UI.BTN_EVENT, false);
		}
		if (!isInGameScene)
		{
			SetRenderNPCModel((Enum)UI.TEX_NPCMODEL, 0, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCPos, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCRot, MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.questCenterNPCFOV, (Action<NPCLoader>)null);
			SetLabelText((Enum)UI.LBL_NPC_MESSAGE, npcText);
		}
		SetActive((Enum)UI.OBJ_NPC_MESSAGE, !isInGameScene);
		if (showMode == SHOW_MODE.DELIVERY)
		{
			SetActive((Enum)UI.SPR_TAB_NORMAL, UI.BTN_TAB_NORMAL == selectedTab);
			SetActive((Enum)UI.SPR_TAB_DAILY, UI.BTN_TAB_DAILY == selectedTab);
			SetActive((Enum)UI.SPR_TAB_WEEKLY, UI.BTN_TAB_WEEKLY == selectedTab);
			SetButtonSprite((Enum)UI.BTN_TAB_NORMAL, (selectedTab != UI.BTN_TAB_NORMAL) ? "PickeShopBtn_Normal_off" : "PickeShopBtn_Green_on", false);
			SetButtonSprite((Enum)UI.BTN_TAB_DAILY, (selectedTab != UI.BTN_TAB_DAILY) ? "PickeShopBtn_Normal_off" : "PickeShopBtn_Green_on", false);
			SetButtonSprite((Enum)UI.BTN_TAB_WEEKLY, (selectedTab != UI.BTN_TAB_WEEKLY) ? "PickeShopBtn_Normal_off" : "PickeShopBtn_Green_on", false);
			SetNPCMessage(selectedTab);
			switch (selectedTab)
			{
			case UI.BTN_TAB_NORMAL:
				SetDeliveryList(normalDeliveryInfo);
				break;
			case UI.BTN_TAB_DAILY:
				SetDeliveryList(dailyDeliveryInfo);
				break;
			case UI.BTN_TAB_WEEKLY:
				SetDeliveryList(weeklyDeliveryInfo);
				break;
			}
		}
		else if (showMode == SHOW_MODE.ORDER)
		{
			if (questItemAry == null && MonoBehaviourSingleton<InventoryManager>.I.questItemInventory.GetCount() > 0)
			{
				List<QuestItemInfo> list = new List<QuestItemInfo>();
				MonoBehaviourSingleton<InventoryManager>.I.ForAllQuestInvetory(delegate(QuestItemInfo item)
				{
					if (item.infoData.questData.num > 0)
					{
						list.Add(item);
					}
				});
				questItemAry = list.ToArray();
				GetCtrl(UI.GRD_ORDER_QUEST).DestroyChildren();
			}
			if (questItemAry == null || questItemAry.Length == 0)
			{
				SetActive((Enum)UI.BTN_SORT, false);
				SetActive((Enum)UI.GRD_ORDER_QUEST, false);
				SetActive((Enum)UI.STR_ORDER_NON_LIST, true);
			}
			else
			{
				questSortData = sortSettings.CreateSortAry<QuestItemInfo, QuestSortData>(questItemAry);
				SetActive((Enum)UI.GRD_ORDER_QUEST, true);
				SetActive((Enum)UI.STR_ORDER_NON_LIST, false);
				SetActive((Enum)UI.BTN_SORT, true);
				SetLabelText((Enum)UI.LBL_SORT, sortSettings.GetSortLabel());
				SetToggle((Enum)UI.TGL_ICON_ASC, sortSettings.orderTypeAsc);
				SetDynamicList((Enum)UI.GRD_ORDER_QUEST, "QuestListOrderItem", questSortData.Length, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
				{
					QuestSpecialSelect questSpecialSelect2 = this;
					SetActive(t, true);
					SetEvent(t, "SELECT_ORDER", i);
					QuestInfoData info2 = this.questSortData[i].itemData.infoData;
					int num9 = (int)(info2.questData.tableData.difficulty + 1);
					int l = 0;
					for (int num10 = difficult.Length; l < num10; l++)
					{
						SetActive(t, difficult[l], l < num9);
					}
					if (!is_recycle)
					{
						ResetTween(t, UI.TWN_DIFFICULT_STAR, 0);
						PlayTween(t, UI.TWN_DIFFICULT_STAR, true, null, false, 0);
					}
					EnemyTable.EnemyData enemyData2 = Singleton<EnemyTable>.I.GetEnemyData((uint)info2.questData.tableData.GetMainEnemyID());
					QuestSortData questSortData = this.questSortData[i];
					ItemIcon itemIcon2 = ItemIcon.Create(questSortData.GetIconType(), questSortData.GetIconID(), questSortData.GetRarity(), FindCtrl(t, UI.OBJ_ENEMY), questSortData.GetIconElement(), null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY);
					itemIcon2.SetEnableCollider(false);
					SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData2.element != ELEMENT_TYPE.MAX);
					SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData2.element);
					SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData2.weakElement);
					SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData2.weakElement == ELEMENT_TYPE.MAX);
					SetLabelText(t, UI.LBL_QUEST_NAME, info2.questData.tableData.questText);
					int num11 = 1;
					ClearStatusQuest clearStatusQuest2 = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => info2.questData.tableData.questID == data.questId);
					if (clearStatusQuest2 != null)
					{
						num11 = clearStatusQuest2.questStatus;
					}
					int value2 = i + 100;
					SetToggleGroup(t, UI.OBJ_ICON_NEW, value2);
					SetToggleGroup(t, UI.OBJ_ICON_CLEARED, value2);
					SetToggleGroup(t, UI.OBJ_ICON_COMPLETE, value2);
					switch (num11)
					{
					default:
						SetToggle(t, UI.OBJ_ICON_NEW, false);
						SetToggle(t, UI.OBJ_ICON_CLEARED, false);
						SetToggle(t, UI.OBJ_ICON_COMPLETE, false);
						break;
					case 1:
						SetToggle(t, UI.OBJ_ICON_NEW, true);
						SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
						break;
					case 3:
						SetToggle(t, UI.OBJ_ICON_CLEARED, true);
						break;
					case 4:
						SetToggle(t, UI.OBJ_ICON_COMPLETE, true);
						SetVisibleWidgetEffect(UI.SCR_ORDER_QUEST, t, UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
						break;
					}
					SetLabelText(t, UI.LBL_ORDER_NUM, info2.questData.num.ToString());
					SetActive(t, UI.LBL_REMAIN, false);
				});
			}
		}
		else if (showMode == SHOW_MODE.QUEST)
		{
			if (questInfo == null || questInfo.Length == 0)
			{
				SetActive((Enum)UI.GRD_QUEST, false);
				SetActive((Enum)UI.STR_QUEST_NON_LIST, true);
			}
			else
			{
				SetActive((Enum)UI.STR_QUEST_NON_LIST, false);
				SetActive((Enum)UI.GRD_QUEST, true);
				SetDynamicList((Enum)UI.GRD_QUEST, "QuestListItem", questInfo.Length, false, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
				{
					QuestSpecialSelect questSpecialSelect = this;
					SetEvent(t, "SELECT_QUEST", i);
					QuestInfoData info = questInfo[i];
					int num5 = (int)(info.questData.tableData.difficulty + 1);
					int j = 0;
					for (int num6 = difficult.Length; j < num6; j++)
					{
						SetActive(t, difficult[j], j < num5);
					}
					if (!is_recycle)
					{
						ResetTween(t, UI.TWN_DIFFICULT_STAR, 0);
						PlayTween(t, UI.TWN_DIFFICULT_STAR, true, null, false, 0);
					}
					EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)info.questData.tableData.GetMainEnemyID());
					if (enemyData != null)
					{
						SetActive(t, UI.OBJ_ENEMY, true);
						int iconId = enemyData.iconId;
						RARITY_TYPE? rarity = (info.questData.tableData.questType != QUEST_TYPE.ORDER) ? null : new RARITY_TYPE?(info.questData.tableData.rarity);
						ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, iconId, rarity, FindCtrl(t, UI.OBJ_ENEMY), enemyData.element, null, -1, null, 0, false, -1, false, null, false, 0, 0, false, GET_TYPE.PAY);
						itemIcon.SetEnableCollider(false);
						SetActive(t, UI.SPR_ELEMENT_ROOT, enemyData.element != ELEMENT_TYPE.MAX);
						SetElementSprite(t, UI.SPR_ELEMENT, (int)enemyData.element);
						SetElementSprite(t, UI.SPR_WEAK_ELEMENT, (int)enemyData.weakElement);
						SetActive(t, UI.STR_NON_WEAK_ELEMENT, enemyData.weakElement == ELEMENT_TYPE.MAX);
					}
					else
					{
						SetActive(t, UI.OBJ_ENEMY, false);
						SetElementSprite(t, UI.SPR_WEAK_ELEMENT, 6);
						SetActive(t, UI.STR_NON_WEAK_ELEMENT, true);
					}
					SetLabelText(t, UI.LBL_QUEST_NUM, string.Empty);
					SetLabelText(t, UI.LBL_QUEST_NAME, info.questData.tableData.questText);
					if (!info.isExistMission)
					{
						SetActive(t, UI.OBJ_MISSION_INFO_ROOT, false);
					}
					else
					{
						SetActive(t, UI.OBJ_MISSION_INFO_ROOT, true);
						int k = 0;
						for (int num7 = info.missionData.Length; k < num7; k++)
						{
							SetActive(t, ui_top_crown[k], info.missionData[k] != null);
							if (info.missionData[k] != null)
							{
								SetActive(t, ui_crown[k], info.missionData[k].state >= CLEAR_STATUS.CLEAR);
							}
						}
					}
					int num8 = 1;
					ClearStatusQuest clearStatusQuest = MonoBehaviourSingleton<QuestManager>.I.clearStatusQuest.Find((ClearStatusQuest data) => info.questData.tableData.questID == data.questId);
					if (clearStatusQuest != null)
					{
						num8 = clearStatusQuest.questStatus;
					}
					int value = i + 100;
					SetToggleGroup(t, UI.OBJ_ICON_NEW, value);
					SetToggleGroup(t, UI.OBJ_ICON_CLEARED, value);
					SetToggleGroup(t, UI.OBJ_ICON_COMPLETE, value);
					switch (num8)
					{
					default:
						SetToggle(t, UI.OBJ_ICON_NEW, false);
						SetToggle(t, UI.OBJ_ICON_CLEARED, false);
						SetToggle(t, UI.OBJ_ICON_COMPLETE, false);
						break;
					case 1:
						SetToggle(t, UI.OBJ_ICON_NEW, true);
						SetVisibleWidgetEffect(UI.SCR_NORMAL_QUEST, t, UI.SPR_ICON_NEW, "ef_ui_questselect_new");
						break;
					case 3:
						SetToggle(t, UI.OBJ_ICON_CLEARED, true);
						break;
					case 4:
						SetToggle(t, UI.OBJ_ICON_COMPLETE, true);
						SetVisibleWidgetEffect(UI.SCR_NORMAL_QUEST, t, UI.SPR_ICON_COMPLETE, "ef_ui_questselect_complete");
						break;
					}
				});
			}
		}
	}

	protected virtual void SetDeliveryList(Delivery[] list)
	{
		if (selectedTab == UI.BTN_TAB_NORMAL && openRegionIds.Length >= 2)
		{
			SetActive((Enum)UI.OBJ_DELIVERY_BAR, false);
			SetActive((Enum)UI.GRD_DELIVERY, false);
			SetActive((Enum)UI.LBL_DELIVERY_NON_LIST, false);
			SetActive((Enum)UI.GRD_AREA, true);
			SetActive((Enum)UI.OBJ_AREA_BAR, true);
			RegionTable.Data data = null;
			List<AreaQuestInfo> areaInfos = new List<AreaQuestInfo>();
			List<AreaQuestInfo> list2 = new List<AreaQuestInfo>();
			for (int j = 0; j < openRegionIds.Length; j++)
			{
				data = Singleton<RegionTable>.I.GetData(openRegionIds[j]);
				if (!IsCleardRegion(data))
				{
					areaInfos.Add(new AreaQuestInfo(data, areaBanners[j], false));
				}
				else
				{
					list2.Add(new AreaQuestInfo(data, areaBanners[j], true));
				}
			}
			areaInfos.AddRange(list2);
			SetDynamicList((Enum)UI.GRD_AREA, "QuestAreaListItem", areaInfos.Count, true, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				AreaQuestInfo areaQuestInfo = areaInfos[i];
				Texture2D bannerTex = areaQuestInfo.bannerTex;
				if (bannerTex != null)
				{
					Transform t2 = FindCtrl(t, UI.TEX_AREA_BANNER);
					SetActive(t2, true);
					SetTexture(t2, bannerTex);
				}
				AreaQuestInfo areaQuestInfo2 = areaInfos[i];
				RegionTable.Data regionData = areaQuestInfo2.regionData;
				SetEvent(t, "SELECT_AREA", (int)regionData.regionId);
				int completableRegionDeliveryNum = MonoBehaviourSingleton<DeliveryManager>.I.GetCompletableRegionDeliveryNum((int)regionData.regionId, regionData.groupId);
				SetBadge(FindCtrl(t, UI.TEX_AREA_BANNER), completableRegionDeliveryNum, 3, -16, -3, false);
				QuestSpecialSelect questSpecialSelect = this;
				object ctrl_enum = UI.SPR_CLEARED_AREA;
				AreaQuestInfo areaQuestInfo3 = areaInfos[i];
				questSpecialSelect.SetActive(t, (Enum)ctrl_enum, areaQuestInfo3.cleared);
			});
			isDeliveryGridReset = false;
		}
		else if (list != null)
		{
			SetActive((Enum)UI.OBJ_AREA_BAR, false);
			SetActive((Enum)UI.GRD_AREA, false);
			SetActive((Enum)UI.GRD_DELIVERY, list.Length > 0);
			SetActive((Enum)UI.LBL_DELIVERY_NON_LIST, list.Length == 0);
			SetActive((Enum)UI.OBJ_DELIVERY_BAR, true);
			SetDynamicList((Enum)UI.GRD_DELIVERY, "QuestRequestItem", list.Length, isDeliveryGridReset, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, true);
				int num = Array.FindIndex(deliveryInfo, (Delivery d) => d.uId == list[i].uId);
				SetEvent(t, "SELECT_DELIVERY", num);
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[num].dId);
				SetupDeliveryListItem(t, deliveryTableData);
			});
			isDeliveryGridReset = false;
			ShowNonDeliveryList();
		}
	}

	private bool IsCleardRegion(RegionTable.Data data)
	{
		int i = 0;
		for (int num = normalDeliveryInfo.Length; i < num; i++)
		{
			Delivery delivery = normalDeliveryInfo[i];
			if (delivery.dId != 0)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
				if (deliveryTableData.regionId == data.regionId)
				{
					return false;
				}
				if (data.groupId > 0 && deliveryTableData.regionId == data.groupId)
				{
					return false;
				}
			}
		}
		return true;
	}

	private void ShowNonDeliveryList()
	{
		switch (selectedTab)
		{
		case UI.BTN_TAB_NORMAL:
			if (normalDeliveryInfo != null && normalDeliveryInfo.Length == 0)
			{
				SetLabelText((Enum)UI.LBL_DELIVERY_NON_LIST, StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 100u));
			}
			break;
		case UI.BTN_TAB_DAILY:
			if (dailyDeliveryInfo != null && dailyDeliveryInfo.Length == 0)
			{
				TimeSpan span2 = TimeSpan.FromSeconds((double)MonoBehaviourSingleton<DeliveryManager>.I.dailyUpdateRemainTime);
				SetLabelText((Enum)UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 101u), GetRemainTimeText(span2)));
			}
			break;
		case UI.BTN_TAB_WEEKLY:
			if (weeklyDeliveryInfo != null && weeklyDeliveryInfo.Length == 0)
			{
				TimeSpan span = TimeSpan.FromSeconds((double)MonoBehaviourSingleton<DeliveryManager>.I.weeklyUpdateRemainTime);
				SetLabelText((Enum)UI.LBL_DELIVERY_NON_LIST, string.Format(StringTable.Get(STRING_CATEGORY.QUEST_DELIVERY, 102u), GetRemainTimeText(span)));
			}
			break;
		}
	}

	public static string GetRemainTimeText(TimeSpan span)
	{
		string text = string.Empty;
		if (span.Seconds > 0)
		{
			span = span.Add(TimeSpan.FromMinutes(1.0));
		}
		if (span.Days > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0u), span.Days);
		}
		if (span.Hours > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1u), span.Hours);
		}
		if (span.Minutes > 0)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), span.Minutes);
		}
		if (text == string.Empty)
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), 0);
		}
		return text;
	}

	protected void OnQuery_TAB_DELIVERY()
	{
		if (showMode != 0)
		{
			SetDirty(UI.GRD_DELIVERY);
			SetDirty(UI.GRD_AREA);
			ChangeToggle(SHOW_MODE.DELIVERY);
			ResetTween((Enum)UI.BTN_DELIVERY, 0);
			PlayTween((Enum)UI.BTN_DELIVERY, true, (EventDelegate.Callback)null, false, 0);
		}
	}

	protected void OnQuery_TAB_ORDER()
	{
		if (showMode != SHOW_MODE.ORDER)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
			ChangeToggle(SHOW_MODE.ORDER);
			ResetTween((Enum)UI.BTN_ORDER, 0);
			PlayTween((Enum)UI.BTN_ORDER, true, (EventDelegate.Callback)null, false, 0);
		}
	}

	protected void OnQuery_TAB_QUEST()
	{
		if (showMode != SHOW_MODE.QUEST)
		{
			SetDirty(UI.GRD_QUEST);
			ChangeToggle(SHOW_MODE.QUEST);
			ResetTween((Enum)UI.BTN_QUEST, 0);
			PlayTween((Enum)UI.BTN_QUEST, true, (EventDelegate.Callback)null, false, 0);
			if (GameSection.GetEventData() != null)
			{
				MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = (uint)GameSection.GetEventData();
			}
		}
	}

	public void ChangeToggle(SHOW_MODE show_mode)
	{
		showMode = show_mode;
		ShowSelectUI();
	}

	protected override void OnOpen()
	{
		SetNPCMessage(selectedTab);
		RemoveRecommend();
		changeToDeliveryClearEvent = false;
		MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(0u, true);
		MonoBehaviourSingleton<QuestManager>.I.currentDeliveryId = 0u;
		base.OnOpen();
	}

	protected override void OnClose()
	{
		RemoveNew(selectedTab);
		base.OnClose();
	}

	private void SetNPCMessage(UI tab)
	{
		if (!isInGameScene)
		{
			string str = string.Empty;
			switch (tab)
			{
			case UI.BTN_TAB_NORMAL:
				str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[0])) ? "_COMPLETE" : string.Empty);
				break;
			case UI.BTN_TAB_DAILY:
				str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[1])) ? "_COMPLETE" : "_DAILY");
				break;
			case UI.BTN_TAB_WEEKLY:
				str = ((!MonoBehaviourSingleton<DeliveryManager>.I.IsExistDelivery(TAB_TYPES[2])) ? "_COMPLETE" : "_WEEKLY");
				break;
			}
			NPCMessageTable.Section section = Singleton<NPCMessageTable>.I.GetSection(base.sectionData.sectionName + str + "_TEXT");
			if (section != null)
			{
				NPCMessageTable.Message nPCMessage = section.GetNPCMessage();
				if (nPCMessage != null)
				{
					npcText = nPCMessage.GetReplaceText();
				}
			}
			else
			{
				npcText = base.sectionData.GetText("NPC_MESSAGE_" + Random.Range(0, 3));
			}
			SetLabelText((Enum)UI.LBL_NPC_MESSAGE, npcText);
		}
	}

	protected virtual void RemoveRecommend()
	{
		if (GameSaveData.instance.IsRecommendedDeliveryCheck())
		{
			GameSaveData.instance.recommendedDeliveryCheck = 0;
			GameSaveData.Save();
		}
	}

	public override void StartSection()
	{
		UI[] array = new UI[3]
		{
			UI.GRD_DELIVERY,
			UI.GRD_ORDER_QUEST,
			UI.GRD_QUEST
		};
		UITweenAddToChildrenCtrl component = base.GetComponent<UITweenAddToChildrenCtrl>((Enum)array[(int)showMode]);
		if (component != null)
		{
			component.InitTween();
		}
		OpenTutorial();
		base.StartSection();
	}

	public void OnQuery_SELECT_ORDER()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questSortData.Length)
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questSortData[num].GetTableID(), true);
			GameSection.SetEventData(questSortData[num].itemData.infoData);
		}
	}

	public void OnQuery_SELECT_DELIVERY()
	{
		int index = (int)GameSection.GetEventData();
		bool is_enough_material = MonoBehaviourSingleton<DeliveryManager>.I.IsCompletableDelivery(deliveryInfo[index].dId);
		int delivery_id = deliveryInfo[index].dId;
		bool is_happen_quest = false;
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery_id);
		if (deliveryTableData != null)
		{
			QuestTable.QuestTableData questData = deliveryTableData.GetQuestData();
			if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
			{
				is_happen_quest = true;
			}
		}
		if (is_enough_material)
		{
			if (isInGameScene && deliveryTableData.IsInvalidClearIngame())
			{
				GameSection.ChangeEvent("DELIVERY_ITEM_COMPLETE", null);
			}
			else if (isInGameScene)
			{
				GameSection.StayEvent();
				MonoBehaviourSingleton<CoopManager>.I.coopStage.fieldRewardPool.SendFieldDrop(delegate(bool b)
				{
					if (b)
					{
						SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
						if (Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id) != null)
						{
							MonoBehaviourSingleton<DeliveryManager>.I.CheckAnnouncePortalOpen();
						}
					}
				});
			}
			else
			{
				GameSection.StayEvent();
				SendDeliveryComplete(index, delivery_id, is_enough_material, is_happen_quest);
			}
		}
		else if (is_happen_quest)
		{
			GameSection.ChangeEvent("SELECT_DELIVERY_HAPPEN", new object[2]
			{
				delivery_id,
				null
			});
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				delivery_id,
				null
			});
		}
	}

	private void SendDeliveryComplete(int index, int delivery_id, bool is_enough_material, bool is_happen_quest)
	{
		DeliveryTable.DeliveryData table = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		changeToDeliveryClearEvent = true;
		bool is_tutorial = !TutorialStep.HasFirstDeliveryCompleted();
		bool enable_clear_event = table.clearEventID != 0;
		MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryComplete(deliveryInfo[index].uId, enable_clear_event, delegate(bool is_success, DeliveryRewardList recv_reward)
		{
			if (is_success)
			{
				List<FieldMapTable.PortalTableData> deliveryRelationPortalData = Singleton<FieldMapTable>.I.GetDeliveryRelationPortalData((uint)delivery_id);
				for (int i = 0; i < deliveryRelationPortalData.Count; i++)
				{
					GameSaveData.instance.newReleasePortals.Add(deliveryRelationPortalData[i].portalID);
				}
				if (is_tutorial)
				{
					TutorialStep.isSendFirstRewardComplete = true;
				}
				if (!enable_clear_event)
				{
					MonoBehaviourSingleton<DeliveryManager>.I.isStoryEventEnd = false;
					if (is_happen_quest)
					{
						GameSection.ChangeStayEvent("DELIVERY_REWARD_HAPPEN", new object[2]
						{
							delivery_id,
							recv_reward
						});
					}
					else
					{
						GameSection.ChangeStayEvent("DELIVERY_REWARD", new object[2]
						{
							delivery_id,
							recv_reward
						});
					}
				}
				else
				{
					GameSection.ChangeStayEvent("CLEAR_EVENT", new object[3]
					{
						(int)table.clearEventID,
						delivery_id,
						recv_reward
					});
				}
			}
			else
			{
				changeToDeliveryClearEvent = false;
			}
			GameSection.ResumeEvent(is_success, null);
		});
	}

	public void OnQuery_SELECT_QUEST()
	{
		int num = (int)GameSection.GetEventData();
		if (num < 0 || num >= questInfo.Length)
		{
			GameSection.StopEvent();
		}
		else
		{
			MonoBehaviourSingleton<QuestManager>.I.SetCurrentQuestID(questInfo[num].questData.tableData.questID, true);
			GameSection.SetEventData(questInfo[num]);
		}
	}

	private void OnQuery_QuestAcceptDeliveryItemComplete_YES()
	{
		BackHome();
	}

	private void OnQuery_InGameQuestAcceptDeliveryItemComplete_YES()
	{
		BackHome();
	}

	private void BackHome()
	{
		if (isInGameScene && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			MonoBehaviourSingleton<InGameProgress>.I.FieldToHome();
		}
	}

	private void OnQuery_SORT()
	{
		GameSection.SetEventData(sortSettings.Clone());
	}

	protected void _OnCloseDialogSort()
	{
		SortSettings sortSettings = (SortSettings)GameSection.GetEventData();
		if (sortSettings != null)
		{
			this.sortSettings = sortSettings;
			SetDirty(UI.GRD_ORDER_QUEST);
			RefreshUI();
		}
	}

	private void OnCloseDialog_QuestSort()
	{
		_OnCloseDialogSort();
	}

	protected virtual bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
	{
		return !tableData.IsEvent();
	}

	protected virtual void GetDeliveryList()
	{
		deliveryInfo = MonoBehaviourSingleton<DeliveryManager>.I.GetDeliveryList(true);
		List<Delivery> list = new List<Delivery>();
		int i = 0;
		for (int num = deliveryInfo.Length; i < num; i++)
		{
			Delivery delivery = deliveryInfo[i];
			DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)delivery.dId);
			if (deliveryTableData == null)
			{
				Log.Warning("DeliveryTable Not Found : dId " + delivery.dId);
			}
			else if (IsVisibleDelivery(delivery, deliveryTableData))
			{
				NPCTable.NPCData nPCData = Singleton<NPCTable>.I.GetNPCData((int)deliveryTableData.npcID);
				if (nPCData == null)
				{
					Log.Error("DeliveryTable NPC ID Found  : dId " + delivery.dId + " : npcID " + deliveryTableData.npcID);
				}
				else
				{
					list.Add(deliveryInfo[i]);
				}
			}
		}
		deliveryInfo = list.ToArray();
		List<Delivery> list2 = new List<Delivery>();
		List<Delivery> list3 = new List<Delivery>();
		List<Delivery> list4 = new List<Delivery>();
		Delivery[] array = deliveryInfo;
		foreach (Delivery delivery2 in array)
		{
			switch (delivery2.type)
			{
			case 0:
				list3.Add(delivery2);
				break;
			case 10:
				list4.Add(delivery2);
				break;
			default:
				list2.Add(delivery2);
				break;
			}
		}
		normalDeliveryInfo = list2.ToArray();
		dailyDeliveryInfo = list3.ToArray();
		weeklyDeliveryInfo = list4.ToArray();
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		bool flag = false;
		if ((flags & NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			SetDirty(UI.GRD_ORDER_QUEST);
			questItemAry = null;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_DELIVERY_OVER) != (NOTIFY_FLAG)0L)
		{
			flag = true;
			MonoBehaviourSingleton<DeliveryManager>.I.SendDeliveryUpdate(delegate
			{
				GetDeliveryList();
				SetDirty(UI.GRD_AREA);
				SetDirty(UI.GRD_DELIVERY);
				base.OnNotify(flags);
			});
		}
		if ((flags & NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE) != (NOTIFY_FLAG)0L)
		{
			GetDeliveryList();
			SetDirty(UI.GRD_AREA);
			SetDirty(UI.GRD_DELIVERY);
		}
		if (!flag)
		{
			base.OnNotify(flags);
		}
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_QUEST_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_DELIVERY_UPDATE | NOTIFY_FLAG.UPDATE_DELIVERY_OVER;
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "SELECT_ORDER")
		{
			int num = -1;
			uint table_id = (uint)event_data;
			num = Array.FindIndex(questSortData, (QuestSortData data) => (int)data.GetTableID() == table_id);
			if (num != -1)
			{
				return new EventData(event_name, num);
			}
			return new EventData(event_name, -1);
		}
		if (event_name == "SELECT_QUEST")
		{
			int num2 = -1;
			uint table_id2 = (uint)event_data;
			num2 = Array.FindIndex(questInfo, (QuestInfoData data) => (int)data.questData.tableData.questID == table_id2);
			if (num2 != -1)
			{
				return new EventData(event_name, num2);
			}
			return new EventData(event_name, -1);
		}
		if (event_name == "SELECT_DELIVERY")
		{
			int num3 = -1;
			uint table_id3 = (uint)event_data;
			num3 = Array.FindIndex(deliveryInfo, (Delivery data) => data.dId == table_id3);
			if (num3 != -1)
			{
				return new EventData(event_name, num3);
			}
			return new EventData(event_name, -1);
		}
		return base.CheckAutoEvent(event_name, event_data);
	}

	private void OnQuery_SELECT_NORMAL()
	{
		SelectTab(UI.BTN_TAB_NORMAL);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void OnQuery_SELECT_DAILY()
	{
		SelectTab(UI.BTN_TAB_DAILY);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void OnQuery_SELECT_WEEKLY()
	{
		SelectTab(UI.BTN_TAB_WEEKLY);
		isDeliveryGridReset = true;
		RefreshUI();
	}

	private void SelectTab(UI tab)
	{
		if (selectedTab != tab)
		{
			RemoveNew(selectedTab);
		}
		selectedTab = tab;
	}

	private void RemoveNew(UI old_select)
	{
		switch (old_select)
		{
		case UI.BTN_TAB_NORMAL:
			break;
		case UI.BTN_TAB_DAILY:
			SetNew(UI.BTN_TAB_DAILY, false);
			GameSaveData.instance.recommendedDailyDeliveryCheck = 0;
			GameSaveData.Save();
			break;
		case UI.BTN_TAB_WEEKLY:
			SetNew(UI.BTN_TAB_WEEKLY, false);
			GameSaveData.instance.recommendedWeeklyDeliveryCheck = 0;
			GameSaveData.Save();
			break;
		}
	}
}
