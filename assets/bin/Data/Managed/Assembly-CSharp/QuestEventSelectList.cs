using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEventSelectList : QuestSpecialSelect
{
	protected new enum UI
	{
		TEX_EVENT_BG,
		BTN_INFO,
		TGL_BUTTON_ROOT,
		SPR_DELIVERY_BTN_SELECTED,
		OBJ_DELIVERY_ROOT,
		TEX_NPCMODEL,
		LBL_NPC_MESSAGE,
		GRD_DELIVERY_QUEST,
		TBL_DELIVERY_QUEST,
		STR_DELIVERY_NON_LIST,
		OBJ_REQUEST_COMPLETED,
		LBL_LOCATION_NAME,
		LBL_LOCATION_NAME_EFFECT,
		WGT_LOCATION_NAME_LIMIT,
		SCR_DELIVERY_QUEST,
		OBJ_IMAGE,
		BTN_EVENT,
		OBJ_FRAME,
		SPR_BG_FRAME,
		LBL_STORY_TITLE,
		OBJ_ACTIVE_ROOT,
		OBJ_INACTIVE_ROOT,
		LBL_MAX,
		LBL_NOW
	}

	protected class Story
	{
		public int id;

		public string title;

		public Story(int id, string title)
		{
			this.id = id;
			this.title = title;
		}
	}

	public class ShowDeliveryData
	{
		public int index = -1;

		public bool isCompleted;

		public DeliveryTable.DeliveryData data;

		public ShowDeliveryData(int i, bool isComp, DeliveryTable.DeliveryData d)
		{
			index = i;
			isCompleted = isComp;
			data = d;
		}

		public ShowDeliveryData(int i, bool isComp, Delivery d)
		{
			index = i;
			isCompleted = isComp;
			data = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.dId);
		}
	}

	protected Network.EventData eventData;

	protected List<DeliveryTable.DeliveryData> clearedDeliveries;

	protected List<Story> stories = new List<Story>();

	protected LabelWidthLimitter title;

	protected LabelWidthLimitter titleEffect;

	protected Transform mapItem;

	protected bool isResetUI;

	protected int nowPage = 1;

	protected int pageMax = 1;

	protected virtual bool showStory => true;

	protected virtual bool showMap => true;

	public override void Initialize()
	{
		eventData = (GameSection.GetEventData() as Network.EventData);
		SkipTween((Enum)UI.SPR_DELIVERY_BTN_SELECTED, true, 0);
		SetActive((Enum)UI.OBJ_DELIVERY_ROOT, true);
		int width = GetWidth(UI.WGT_LOCATION_NAME_LIMIT);
		title = new LabelWidthLimitter(GetCtrl(UI.LBL_LOCATION_NAME).GetComponent<UILabel>(), width, false);
		titleEffect = new LabelWidthLimitter(GetCtrl(UI.LBL_LOCATION_NAME_EFFECT).GetComponent<UILabel>(), width, true);
		base.Initialize();
	}

	protected override IEnumerator DoInitialize()
	{
		string resourceName = ResourceName.GetEventBG(eventData.bannerId);
		Hash128 hash = default(Hash128);
		if (MonoBehaviourSingleton<ResourceManager>.I.manifest != null)
		{
			hash = MonoBehaviourSingleton<ResourceManager>.I.manifest.GetAssetBundleHash(RESOURCE_CATEGORY.EVENT_BG.ToAssetBundleName(resourceName));
		}
		if (MonoBehaviourSingleton<ResourceManager>.I.manifest == null || hash.get_isValid())
		{
			LoadingQueue load_queue = new LoadingQueue(this);
			LoadObject lo_bg = load_queue.Load(RESOURCE_CATEGORY.EVENT_BG, resourceName, false);
			LoadObject lo_item = null;
			if (ShouldShowEventMapButton())
			{
				lo_item = load_queue.Load(RESOURCE_CATEGORY.QUEST_ITEM, "QEM_10000000", false);
			}
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			SetTexture(texture: lo_bg.loadedObject as Texture2D, texture_enum: UI.TEX_EVENT_BG);
			if (lo_item != null && null != lo_item.loadedObject)
			{
				GameObject mapItemObj = lo_item.loadedObject as GameObject;
				mapItem = mapItemObj.get_transform();
			}
		}
		GetDeliveryList();
		EndInitialize();
	}

	public override void Exit()
	{
		base.Exit();
	}

	protected override void OnOpen()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(InitScroll());
		base.OnOpen();
	}

	protected override void RemoveRecommend()
	{
	}

	private IEnumerator InitScroll()
	{
		UIScrollView scroll = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
		while (base.state != STATE.OPEN)
		{
			yield return (object)null;
		}
		scroll.set_enabled(scroll.shouldMoveVertically);
		RepositionTable();
	}

	protected virtual bool ShouldShowEventMapButton()
	{
		if (!showMap)
		{
			return false;
		}
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		RegionTable.Data data2 = Array.Find(data, (RegionTable.Data o) => o.eventId == eventData.eventId);
		return null != data2;
	}

	protected override bool IsVisibleDelivery(Delivery delivery, DeliveryTable.DeliveryData tableData)
	{
		if (eventData == null)
		{
			return false;
		}
		if (tableData.IsEvent() && tableData.eventID == eventData.eventId)
		{
			return true;
		}
		return false;
	}

	protected void RepositionTable()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		UITable component = base.GetComponent<UITable>((Enum)UI.TBL_DELIVERY_QUEST);
		if (Object.op_Implicit(component))
		{
			component.Reposition();
			List<Transform> childList = component.GetChildList();
			int i = 0;
			for (int count = childList.Count; i < count; i++)
			{
				Vector3 localPosition = childList[i].get_localPosition();
				localPosition.x = 0f;
				childList[i].set_localPosition(localPosition);
			}
		}
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_LOCATION_NAME, eventData.name);
		SetLabelText((Enum)UI.LBL_LOCATION_NAME_EFFECT, eventData.name);
		SetActive((Enum)UI.BTN_INFO, !string.IsNullOrEmpty(eventData.linkName));
		title.Update();
		titleEffect.Update();
		stories.Clear();
		if (eventData.prologueStoryId > 0)
		{
			stories.Add(new Story(eventData.prologueStoryId, eventData.prologueTitle));
		}
		clearedDeliveries = CreateClearedDliveryList();
		UpdateList();
		UpdateAnchors();
		isResetUI = false;
	}

	protected virtual List<DeliveryTable.DeliveryData> CreateClearedDliveryList()
	{
		List<DeliveryTable.DeliveryData> list = new List<DeliveryTable.DeliveryData>();
		List<ClearStatusDelivery> clearStatusDelivery = MonoBehaviourSingleton<DeliveryManager>.I.clearStatusDelivery;
		int i = 0;
		for (int count = clearStatusDelivery.Count; i < count; i++)
		{
			ClearStatusDelivery d = clearStatusDelivery[i];
			if (d.deliveryStatus == 3)
			{
				DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)d.deliveryId);
				if (deliveryTableData.eventID == eventData.eventId && !Array.Exists(deliveryInfo, (Delivery x) => x.dId == d.deliveryId))
				{
					list.Add(deliveryTableData);
					if (deliveryTableData.clearEventID != 0)
					{
						string value = deliveryTableData.clearEventTitle;
						if (string.IsNullOrEmpty(value))
						{
							value = deliveryTableData.name;
						}
						stories.Add(new Story((int)deliveryTableData.clearEventID, value));
					}
				}
			}
		}
		return list;
	}

	protected void UpdateList()
	{
		if (Object.op_Implicit(GetCtrl(UI.TBL_DELIVERY_QUEST)))
		{
			UpdateTable();
		}
		else
		{
			UpdateGrid();
		}
	}

	protected virtual void UpdateTable()
	{
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Expected O, but got Unknown
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int count = stories.Count;
		if (count > 0)
		{
			num++;
		}
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, false, deliveryInfo[j]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, true, clearedDeliveries[k]);
				list.Add(item2);
			}
		}
		pageMax = 1 + (list.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
		ShowDeliveryData[] showList = GetPagingList(list.ToArray(), 10, nowPage);
		int num2 = showList.Length;
		if (showStory)
		{
			num2 += num + stories.Count;
		}
		if (num2 == 0)
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, true);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
			SetActive((Enum)UI.TBL_DELIVERY_QUEST, false);
		}
		else
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, false);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
			SetActive((Enum)UI.TBL_DELIVERY_QUEST, true);
			bool flag2 = false;
			if (ShouldShowEventMapButton())
			{
				flag2 = true;
				num2++;
			}
			int questStartIndex = 0;
			if (flag2)
			{
				questStartIndex++;
			}
			int borderIndex = questStartIndex + showList.Length;
			int storyStartIndex = borderIndex;
			if (stories.Count > 0)
			{
				storyStartIndex++;
			}
			Transform ctrl = GetCtrl(UI.TBL_DELIVERY_QUEST);
			if (Object.op_Implicit(ctrl))
			{
				int l = 0;
				for (int childCount = ctrl.get_childCount(); l < childCount; l++)
				{
					Transform val = ctrl.GetChild(0);
					val.set_parent(null);
					Object.Destroy(val.get_gameObject());
				}
			}
			SetTable(UI.TBL_DELIVERY_QUEST, string.Empty, num2, isResetUI, delegate(int i, Transform parent)
			{
				//IL_008f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0096: Expected O, but got Unknown
				Transform val2 = null;
				if (i < storyStartIndex)
				{
					if (i < borderIndex)
					{
						if (i < questStartIndex)
						{
							if (!(null != mapItem))
							{
								return Realizes("QuestEventBorderItem", parent, true);
							}
							return ResourceUtility.Realizes(mapItem.get_gameObject(), parent, -1);
						}
						return Realizes("QuestRequestItem", parent, true);
					}
					return Realizes("QuestEventBorderItem", parent, true);
				}
				return Realizes("QuestEventStoryItem", parent, true);
			}, delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, true);
				if (i >= storyStartIndex)
				{
					int storyIndex = i - storyStartIndex;
					InitStory(storyIndex, t);
				}
				else if (i < borderIndex)
				{
					if (i >= questStartIndex)
					{
						int num3 = i - questStartIndex;
						InitDelivery(showList[num3], t);
					}
					else
					{
						InitMap(t);
					}
				}
			});
			UIScrollView component = base.GetComponent<UIScrollView>((Enum)UI.SCR_DELIVERY_QUEST);
			component.set_enabled(true);
			RepositionTable();
		}
	}

	protected virtual void UpdateGrid()
	{
		List<ShowDeliveryData> list = new List<ShowDeliveryData>();
		if (deliveryInfo != null)
		{
			for (int j = 0; j < deliveryInfo.Length; j++)
			{
				ShowDeliveryData item = new ShowDeliveryData(j, false, deliveryInfo[j]);
				list.Add(item);
			}
		}
		if (clearedDeliveries != null)
		{
			for (int k = 0; k < clearedDeliveries.Count; k++)
			{
				ShowDeliveryData item2 = new ShowDeliveryData(k, true, clearedDeliveries[k]);
				list.Add(item2);
			}
		}
		pageMax = 1 + (list.Count - 1) / 10;
		bool flag = pageMax > 1;
		SetActive((Enum)UI.OBJ_ACTIVE_ROOT, flag);
		SetActive((Enum)UI.OBJ_INACTIVE_ROOT, !flag);
		SetLabelText((Enum)UI.LBL_MAX, pageMax.ToString());
		SetLabelText((Enum)UI.LBL_NOW, nowPage.ToString());
		ShowDeliveryData[] showList = GetPagingList(list.ToArray(), 10, nowPage);
		if (list.Count == 0)
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, true);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, false);
		}
		else
		{
			SetActive((Enum)UI.STR_DELIVERY_NON_LIST, false);
			SetActive((Enum)UI.GRD_DELIVERY_QUEST, true);
			SetDynamicList((Enum)UI.GRD_DELIVERY_QUEST, "QuestRequestItem", showList.Length, isResetUI, (Func<int, bool>)null, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycle)
			{
				SetActive(t, true);
				if (!showList[i].isCompleted)
				{
					SetEvent(t, "SELECT_DELIVERY", showList[i].index);
				}
				else
				{
					SetEvent(t, "SELECT_COMPLETED_DELIVERY", showList[i].index);
				}
				SetupDeliveryListItem(t, showList[i].data);
				if (showList[i].isCompleted)
				{
					SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
				}
			});
		}
	}

	protected void InitDelivery(ShowDeliveryData showData, Transform t)
	{
		if (showData.isCompleted)
		{
			bool flag = false;
			if (showData.data != null)
			{
				QuestTable.QuestTableData questData = showData.data.GetQuestData();
				if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
				{
					flag = true;
				}
			}
			if (flag)
			{
				SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", showData.index);
			}
			else
			{
				SetEvent(t, "SELECT_COMPLETED_DELIVERY", showData.index);
			}
			SetupDeliveryListItem(t, showData.data);
			SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
			SetCompletedHaveCount(t, showData.data);
		}
		else
		{
			SetEvent(t, "SELECT_DELIVERY", showData.index);
			SetupDeliveryListItem(t, showData.data);
		}
	}

	protected virtual void InitNormalDelivery(int index, Transform t)
	{
		SetEvent(t, "SELECT_DELIVERY", index);
		DeliveryTable.DeliveryData deliveryTableData = Singleton<DeliveryTable>.I.GetDeliveryTableData((uint)deliveryInfo[index].dId);
		SetupDeliveryListItem(t, deliveryTableData);
	}

	protected virtual void InitCompletedDelivery(int completedIndex, Transform t)
	{
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[completedIndex];
		bool flag = false;
		if (deliveryData != null)
		{
			QuestTable.QuestTableData questData = deliveryData.GetQuestData();
			if (questData != null && questData.questType == QUEST_TYPE.HAPPEN)
			{
				flag = true;
			}
		}
		if (flag)
		{
			SetEvent(t, "SELECT_COMPLETED_DELIVERY_HAPPEN", completedIndex);
		}
		else
		{
			SetEvent(t, "SELECT_COMPLETED_DELIVERY", completedIndex);
		}
		SetupDeliveryListItem(t, deliveryData);
		SetActive(t, UI.OBJ_REQUEST_COMPLETED, true);
		SetCompletedHaveCount(t, deliveryData);
	}

	protected virtual void InitStory(int storyIndex, Transform t)
	{
		SetEvent(t, "SELECT_STORY", storyIndex);
		SetLabelText(t, UI.LBL_STORY_TITLE, stories[storyIndex].title);
	}

	protected virtual void InitMap(Transform t)
	{
		SetEvent(t, "WORLDMAP", eventData.eventId);
		SetLabelText(t, UI.LBL_STORY_TITLE, "マップ");
	}

	private void OnQuery_INFO()
	{
		string text = string.Format(WebViewManager.NewsWithLinkParamFormat, eventData.linkName);
		GameSection.SetEventData(text);
	}

	private void OnQuery_SELECT_COMPLETED_DELIVERY()
	{
		SetCompletedDeliveryEventData();
	}

	private void OnQuery_SELECT_COMPLETED_DELIVERY_HAPPEN()
	{
		SetCompletedDeliveryEventData();
	}

	private void SetCompletedDeliveryEventData()
	{
		int index = (int)GameSection.GetEventData();
		DeliveryTable.DeliveryData deliveryData = clearedDeliveries[index];
		int id = (int)deliveryData.id;
		DeliveryRewardList deliveryRewardList = new DeliveryRewardList();
		GameSection.SetEventData(new object[3]
		{
			id,
			deliveryRewardList,
			true
		});
	}

	protected virtual void OnQuery_SELECT_STORY()
	{
		int index = (int)GameSection.GetEventData();
		Story story = stories[index];
		EventData[] array = new EventData[3]
		{
			new EventData("MAIN_MENU_HOME", null),
			new EventData("TO_EVENT", null),
			new EventData("SELECT", eventData)
		};
		GameSection.SetEventData(new object[4]
		{
			story.id,
			string.Empty,
			string.Empty,
			array
		});
	}

	protected virtual void OnQuery_WORLDMAP()
	{
		int eventId = (int)GameSection.GetEventData();
		RegionTable.Data[] data = Singleton<RegionTable>.I.GetData();
		RegionTable.Data data2 = Array.Find(data, (RegionTable.Data o) => o.eventId == eventId);
		if (data2 != null)
		{
			MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial = true;
			RequestEvent("DIRECT_EVENT", (int)data2.regionId);
		}
	}

	private void OnQuery_PAGE_PREV()
	{
		isResetUI = true;
		nowPage = ((nowPage <= 1) ? pageMax : (nowPage - 1));
		RefreshUI();
	}

	private void OnQuery_PAGE_NEXT()
	{
		isResetUI = true;
		nowPage = ((nowPage >= pageMax) ? 1 : (nowPage + 1));
		RefreshUI();
	}
}
