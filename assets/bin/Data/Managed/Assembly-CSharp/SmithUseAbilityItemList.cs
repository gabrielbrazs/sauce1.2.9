using System;
using System.Collections.Generic;
using UnityEngine;

public class SmithUseAbilityItemList : GameSection
{
	public enum UI
	{
		OBJ_CAPTION_3,
		LBL_CAPTION,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		TGL_CHANGE_INVENTORY,
		BTN_SELL_MODE_END,
		BTN_SELL,
		STR_HAVE_NUM,
		LBL_NOW_HAVE_NUM,
		LBL_MAX_HAVE_NUM,
		OBJ_BTN_SELL_MODE,
		OBJ_SELL_MODE_ROOT,
		LBL_TOTAL,
		LBL_MAX_SELECT_NUM,
		LBL_SELECT_NUM,
		LBL_SORT,
		TGL_ICON_ASC
	}

	public enum SHOW_INVENTORY_MODE
	{
		MAIN_STATUS,
		SMALL
	}

	private EquipItemInfo equipItemInfo;

	private bool isSellMode;

	private List<AbilityItemSortData> sellItemData = new List<AbilityItemSortData>();

	private SHOW_INVENTORY_MODE currentShowInventoryMode;

	private ItemStorageTop.AbilityItemInventory inventory;

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return base.GetUpdateUINotifyFlags() | NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY;
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_ABILITY_ITEM_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			inventory = null;
			SetDirty(GetCurrentInventoryRoot());
		}
		base.OnNotify(flags);
	}

	public override void Initialize()
	{
		equipItemInfo = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>().selectEquipData;
		currentShowInventoryMode = SHOW_INVENTORY_MODE.MAIN_STATUS;
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		if (inventory == null)
		{
			inventory = new ItemStorageTop.AbilityItemInventory();
		}
		SetActive((Enum)UI.OBJ_BTN_SELL_MODE, !isSellMode);
		SetActive((Enum)UI.OBJ_SELL_MODE_ROOT, isSellMode);
		SetLabelText((Enum)UI.LBL_MAX_HAVE_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userStatus.maxAbilityItem.ToString());
		SetLabelText((Enum)UI.LBL_NOW_HAVE_NUM, inventory.datas.Length.ToString());
		SetActive((Enum)UI.GRD_INVENTORY, false);
		SetActive((Enum)UI.GRD_INVENTORY_SMALL, false);
		SetLabelText((Enum)UI.LBL_SORT, inventory.sortSettings.GetSortLabel());
		SetToggle((Enum)UI.TGL_ICON_ASC, inventory.sortSettings.orderTypeAsc);
		if (isSellMode)
		{
			SetLabelText((Enum)UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
			SetLabelText((Enum)UI.LBL_SELECT_NUM, sellItemData.Count.ToString());
			int num = 0;
			foreach (AbilityItemSortData sellItemDatum in sellItemData)
			{
				num += sellItemDatum.itemData.GetItemTableData().price;
			}
			SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
		}
		UI currentInventoryRoot = GetCurrentInventoryRoot();
		SetActive((Enum)currentInventoryRoot, true);
		SetDynamicList((Enum)currentInventoryRoot, (string)null, inventory.datas.Length, false, (Func<int, bool>)delegate(int i)
		{
			SortCompareData sortCompareData = inventory.datas[i];
			if (sortCompareData == null || !sortCompareData.IsPriority(inventory.sortSettings.orderTypeAsc))
			{
				return false;
			}
			return true;
		}, (Func<int, Transform, Transform>)null, (Action<int, Transform, bool>)delegate(int i, Transform t, bool is_recycre)
		{
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			SmithUseAbilityItemList smithUseAbilityItemList = this;
			AbilityItemSortData abilityItem = inventory.datas[i] as AbilityItemSortData;
			int num2 = sellItemData.FindIndex((AbilityItemSortData x) => x.GetUniqID() == abilityItem.GetUniqID());
			ItemIcon itemIcon = CreateIcon(abilityItem, t, i);
			if (itemIcon != null)
			{
				itemIcon.SetUniqID(abilityItem.GetUniqID());
				bool flag = abilityItem.itemData.GetItemTableData().rarity <= equipItemInfo.tableData.rarity;
				itemIcon.SetGrayout(!flag);
				if (itemIcon is ItemIconDetail)
				{
					(itemIcon as ItemIconDetail).setupperMaterial.SetDescription(abilityItem.itemData.GetDescription());
					(itemIcon as ItemIconDetail).setupperMaterial.SetActiveInfo(1);
				}
				itemIcon.textLabel.get_gameObject().SetActive(true);
				if (isSellMode)
				{
					itemIcon.selectFrame.get_gameObject().SetActive(num2 >= 0);
					if (num2 >= 0)
					{
						if (itemIcon is ItemIconDetail)
						{
							(itemIcon as ItemIconDetail).setupperEquip.SetupSelectNumberSprite(num2 + 1);
						}
						else
						{
							(itemIcon as ItemIconDetailSmall).SetupSelectNumberSprite(num2 + 1);
						}
					}
				}
				SetEvent(itemIcon.transform, (!flag) ? "LESS_RARITY" : "SELECT_ITEM", abilityItem);
			}
		});
		SetToggle((Enum)UI.TGL_CHANGE_INVENTORY, currentShowInventoryMode != SHOW_INVENTORY_MODE.SMALL);
		UpdateAnchors();
		base.UpdateUI();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<InventoryManager>.I.DoRemoveNewFragsAbilityItem();
		base.Exit();
	}

	private ItemIcon CreateIcon(AbilityItemSortData item_data, Transform parent, int index)
	{
		bool is_new = MonoBehaviourSingleton<InventoryManager>.I.IsNewItem(ITEM_ICON_TYPE.ABILITY_ITEM, item_data.GetUniqID());
		MonoBehaviourSingleton<InventoryManager>.I.AddShowFragsAbilityItem(item_data.GetUniqID());
		ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(item_data.GetTableID());
		if (currentShowInventoryMode != SHOW_INVENTORY_MODE.SMALL)
		{
			return ItemIconDetail.CreateMaterialIcon(item_data.GetIconType(), item_data.GetIconID(), item_data.GetRarity(), itemData, true, parent, item_data.GetNum(), item_data.GetName(), "SELECT_ITEM", index, -1, false, is_new);
		}
		return ItemIconDetailSmall.CreateSmallMaterialIcon(item_data.GetIconType(), item_data.GetIconID(), item_data.GetRarity(), parent, item_data.GetNum(), item_data.GetName(), "SELECT_ITEM", index, -1, false, is_new, 0, 0, ItemIconDetail.ICON_STATUS.NONE);
	}

	private void OnQuery_CHANGE_INVENTORY()
	{
		currentShowInventoryMode = ((currentShowInventoryMode == SHOW_INVENTORY_MODE.MAIN_STATUS) ? SHOW_INVENTORY_MODE.SMALL : SHOW_INVENTORY_MODE.MAIN_STATUS);
		SetToggle((Enum)UI.TGL_CHANGE_INVENTORY, currentShowInventoryMode != SHOW_INVENTORY_MODE.SMALL);
		RefreshUI();
	}

	private void OnQuery_SELECT_ITEM()
	{
		AbilityItemSortData abilityItemSortData = GameSection.GetEventData() as AbilityItemSortData;
		if (abilityItemSortData == null)
		{
			GameSection.StopEvent();
		}
		else if (isSellMode)
		{
			ChangeSellModeSelectItem(abilityItemSortData);
		}
		else
		{
			GameSection.SetEventData(new object[2]
			{
				equipItemInfo,
				abilityItemSortData
			});
		}
	}

	private void OnQuery_SELL_MODE()
	{
		isSellMode = !isSellMode;
		RefreshUI();
	}

	private void OnQuery_SELL_MODE_END()
	{
		sellItemData.Clear();
		isSellMode = !isSellMode;
		RefreshUI();
	}

	protected void OnQuery_SELL()
	{
		if (sellItemData.Count == 0)
		{
			GameSection.ChangeEvent("SELL_NOT_SELECT", null);
		}
		else
		{
			GameSection.SetEventData(sellItemData);
		}
	}

	private void OnQuery_LESS_RARITY()
	{
		AbilityItemSortData abilityItemSortData = GameSection.GetEventData() as AbilityItemSortData;
		if (abilityItemSortData == null)
		{
			GameSection.StopEvent();
		}
		else if (isSellMode)
		{
			ChangeSellModeSelectItem(abilityItemSortData);
		}
	}

	private void OnQuery_SECTION_BACK()
	{
		GameSection.SetEventData(equipItemInfo);
	}

	private void OnQuery_SORT()
	{
		GameSection.SetEventData(inventory.sortSettings.Clone());
	}

	private void InitializeCaption()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		SetLabelText(ctrl, UI.LBL_CAPTION, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 26u));
		UITweenCtrl component = ctrl.get_gameObject().GetComponent<UITweenCtrl>();
		if (component != null)
		{
			component.Reset();
			int i = 0;
			for (int num = component.tweens.Length; i < num; i++)
			{
				component.tweens[i].ResetToBeginning();
			}
			component.Play(true, null);
		}
	}

	private UI GetCurrentInventoryRoot()
	{
		return (currentShowInventoryMode != 0) ? UI.GRD_INVENTORY_SMALL : UI.GRD_INVENTORY;
	}

	private void ChangeSellModeSelectItem(AbilityItemSortData abilityItem)
	{
		if (sellItemData.Contains(abilityItem))
		{
			sellItemData.Remove(abilityItem);
			RefreshSelectSell();
			GameSection.StopEvent();
		}
		else if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX > sellItemData.Count)
		{
			sellItemData.Add(abilityItem);
			RefreshSelectSell();
			GameSection.StopEvent();
		}
		else
		{
			GameSection.ChangeEvent("OVER_SELL_ITEM", null);
		}
	}

	private void RefreshSelectSell()
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		ItemIcon[] componentsInChildren = GetCtrl(GetCurrentInventoryRoot()).GetComponentsInChildren<ItemIcon>();
		foreach (ItemIcon itemIcon in componentsInChildren)
		{
			ulong uniqueId = itemIcon.GetUniqID;
			int num = sellItemData.FindIndex((AbilityItemSortData x) => x.GetUniqID() == uniqueId);
			itemIcon.selectFrame.get_gameObject().SetActive(num >= 0);
			if (itemIcon is ItemIconDetail)
			{
				(itemIcon as ItemIconDetail).setupperEquip.SetupSelectNumberSprite(num + 1);
			}
			else
			{
				(itemIcon as ItemIconDetailSmall).SetupSelectNumberSprite(num + 1);
			}
		}
		SetSellInfoView();
	}

	private void SetSellInfoView()
	{
		SetLabelText((Enum)UI.LBL_MAX_SELECT_NUM, MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX.ToString());
		SetLabelText((Enum)UI.LBL_SELECT_NUM, sellItemData.Count.ToString());
		int num = 0;
		foreach (AbilityItemSortData sellItemDatum in sellItemData)
		{
			num += sellItemDatum.itemData.GetItemTableData().price;
		}
		SetLabelText((Enum)UI.LBL_TOTAL, num.ToString());
	}

	private void OnCloseDialog_AbilityItemSellIncludeRareConfirm()
	{
		OnCloseDialog_AbilityItemSellConfirm();
	}

	private void OnCloseDialog_AbilityItemSellConfirm()
	{
		inventory = null;
		isSellMode = false;
		sellItemData.Clear();
		SetDirty(GetCurrentInventoryRoot());
		RefreshUI();
	}

	private void OnCloseDialog_ItemStorageAbilityItemSort()
	{
		CloseSort();
	}

	protected void CloseSort()
	{
		if (GameSection.GetEventData() is SortSettings && inventory.Sort(GameSection.GetEventData() as SortSettings))
		{
			SetDirty(UI.GRD_INVENTORY);
			SetDirty(UI.GRD_INVENTORY_SMALL);
			RefreshUI();
		}
	}
}
