using System.Collections.Generic;

public class SmithGrowItemSelect : SmithEquipSelectBase
{
	protected override string prefabSuffix => "Grow";

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "GrowEquipItemNeedItemTable";
			foreach (string item in base.requireDataTable)
			{
				yield return item;
			}
		}
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		smithType = (SmithType)(int)array[0];
		GameSection.SetEventData(array[1]);
		base.Initialize();
		GameSection.SetEventData(null);
		string caption = (!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(base.selectInventoryType)) ? base.sectionData.GetText("CAPTION_DEFENCE") : base.sectionData.GetText("CAPTION_WEAPON");
		InitializeCaption(caption);
	}

	public override void UpdateUI()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		SetActive(GetCtrl(uiTypeTab[weaponPickupIndex]).get_parent(), false);
		SetActive(GetCtrl(uiTypeTab[armorPickupIndex]).get_parent(), false);
		if (MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>() == null)
		{
			MonoBehaviourSingleton<SmithManager>.I.CreateSmithData<SmithManager.SmithGrowData>();
		}
		base.UpdateUI();
	}

	protected override string GetSelectTypeText()
	{
		return base.sectionData.GetText((smithType != SmithType.GROW) ? "TYPE_EVOLVE" : "TYPE_GROW");
	}

	protected override void OnOpen()
	{
		object eventData = GameSection.GetEventData();
		if (eventData is SmithType)
		{
			SmithType smithType = (SmithType)(int)eventData;
			if (base.smithType != smithType)
			{
				base.smithType = smithType;
				InitLocalInventory();
			}
		}
		SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
		if (smithData != null)
		{
			ulong uniqueID = smithData.selectEquipData.uniqueID;
			int i = 0;
			for (int num = localInventoryEquipData.Length; i < num; i++)
			{
				if (uniqueID == localInventoryEquipData[i].GetUniqID())
				{
					localInventoryEquipData[i].SetItem(smithData.selectEquipData);
				}
			}
		}
	}

	protected override void InitSort()
	{
		SortBase.DIALOG_TYPE dialog_type = (!MonoBehaviourSingleton<InventoryManager>.I.IsWeaponInventoryType(base.selectInventoryType)) ? SortBase.DIALOG_TYPE.ARMOR : SortBase.DIALOG_TYPE.WEAPON;
		sortSettings = SortSettings.CreateMemSortSettings(dialog_type, SortSettings.SETTINGS_TYPE.EQUIP_ITEM);
	}

	protected override void InitLocalInventory()
	{
		MonoBehaviourSingleton<InventoryManager>.I.changeInventoryType = base.selectInventoryType;
		MonoBehaviourSingleton<SmithManager>.I.CreateLocalInventory();
		selectInventoryIndex = -1;
		localInventoryEquipData = sortSettings.CreateSortAry<EquipItemInfo, EquipItemSortData>(MonoBehaviourSingleton<SmithManager>.I.localInventoryEquipData as EquipItemInfo[]);
	}

	protected override bool sorting()
	{
		InitLocalInventory();
		return true;
	}

	protected override void SelectingInventoryFirst()
	{
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0 && localInventoryEquipData[0] != null)
		{
			SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
			if (smithData != null)
			{
				smithData.selectEquipData = (localInventoryEquipData[0].GetItemData() as EquipItemInfo);
				selectInventoryIndex = 0;
			}
		}
	}

	protected override int GetSelectItemIndex()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			return -1;
		}
		EquipItemInfo equipData = GetEquipData();
		if (equipData == null)
		{
			return -2;
		}
		int i = 0;
		for (int num = localInventoryEquipData.Length; i < num; i++)
		{
			if (equipData.uniqueID == localInventoryEquipData[i].GetUniqID())
			{
				return i;
			}
		}
		return -1;
	}

	protected void OnCloseDialog_SmithEquipChangeSort()
	{
		OnCloseSortDialog();
	}

	protected override void OnQuery_TRY_ON()
	{
		bool flag = GameSceneEvent.current.eventName == "TRY_ON";
		if (localInventoryEquipData != null && localInventoryEquipData.Length != 0)
		{
			selectInventoryIndex = (int)GameSection.GetEventData();
			SortCompareData sortCompareData = localInventoryEquipData[selectInventoryIndex];
			if (sortCompareData != null)
			{
				ulong uniqID = sortCompareData.GetUniqID();
				if (uniqID != 0L)
				{
					SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
					smithData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
				}
				base.OnQuery_TRY_ON();
				if (flag)
				{
					GameSection.ChangeEvent("SELECT_ITEM", null);
					OnQuery_SELECT_ITEM();
				}
			}
		}
	}

	protected override void OnQuery_SELECT_ITEM()
	{
		if (localInventoryEquipData == null || localInventoryEquipData.Length == 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
			if (smithData == null)
			{
				GameSection.StopEvent();
			}
			else
			{
				EquipItemInfo selectEquipData = smithData.selectEquipData;
				if (selectEquipData.IsLevelMax())
				{
					if (selectEquipData.tableData.IsEvolve())
					{
						GameSection.ChangeEvent("EVOLVE", null);
					}
					else if (selectEquipData.IsExceedMax() && !selectEquipData.tableData.IsShadow())
					{
						GameSection.ChangeEvent("ALREADY_LV_MAX", null);
					}
				}
			}
		}
	}

	protected override void OnQueryDetail()
	{
		int num = (int)GameSection.GetEventData();
		SortCompareData sortCompareData = localInventoryEquipData[num];
		if (sortCompareData == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			ulong uniqID = sortCompareData.GetUniqID();
			if (uniqID != 0L)
			{
				SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
				smithData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(uniqID);
			}
			base.OnQueryDetail();
		}
	}

	public override void OnNotify(NOTIFY_FLAG notify_flags)
	{
		if ((notify_flags & (NOTIFY_FLAG.UPDATE_EQUIP_GROW | NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE)) != (NOTIFY_FLAG)0L)
		{
			SmithManager.SmithGrowData smithData = MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithGrowData>();
			if (smithData != null && smithData.selectEquipData != null)
			{
				smithData.selectEquipData = MonoBehaviourSingleton<InventoryManager>.I.GetEquipItem(smithData.selectEquipData.uniqueID);
			}
		}
		if ((notify_flags & NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY) != (NOTIFY_FLAG)0L || (notify_flags & NOTIFY_FLAG.UPDATE_SKILL_INVENTORY) != (NOTIFY_FLAG)0L)
		{
			InitLocalInventory();
			if (GetSelectItemIndex() < 0)
			{
				SelectingInventoryFirst();
			}
		}
		base.OnNotify(notify_flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_USER_STATUS | NOTIFY_FLAG.UPDATE_EQUIP_GROW | NOTIFY_FLAG.UPDATE_EQUIP_EVOLVE | NOTIFY_FLAG.UPDATE_EQUIP_FAVORITE | NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_ITEM_INVENTORY | NOTIFY_FLAG.UPDATE_EQUIP_INVENTORY | NOTIFY_FLAG.UPDATE_SKILL_INVENTORY;
	}

	public override EventData CheckAutoEvent(string event_name, object event_data)
	{
		if (event_name == "TRY_ON")
		{
			ulong num = (ulong)event_data;
			int i = 0;
			for (int num2 = localInventoryEquipData.Length; i < num2; i++)
			{
				if (localInventoryEquipData[i].GetUniqID() == num)
				{
					return new EventData(event_name, i);
				}
			}
		}
		return base.CheckAutoEvent(event_name, event_data);
	}
}
