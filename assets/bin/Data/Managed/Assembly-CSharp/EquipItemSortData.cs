using Network;
using UnityEngine;

public class EquipItemSortData : SortCompareData
{
	public EquipItemInfo equipData;

	public override object GetItemData()
	{
		return equipData;
	}

	public override void SetItem(object item)
	{
		equipData = (EquipItemInfo)item;
	}

	public override void SetupSortingData(SortBase.SORT_REQUIREMENT requirement, EquipItemStatus status = null)
	{
		switch (requirement)
		{
		default:
			sortingData = (long)equipData.uniqueID;
			break;
		case SortBase.SORT_REQUIREMENT.RARITY:
			sortingData = (long)equipData.tableData.rarity;
			break;
		case SortBase.SORT_REQUIREMENT.LV:
			sortingData = equipData.level;
			break;
		case SortBase.SORT_REQUIREMENT.ATK:
			sortingData = equipData.atk + equipData.elemAtk;
			break;
		case SortBase.SORT_REQUIREMENT.DEF:
			sortingData = equipData.def + equipData.elemDef;
			break;
		case SortBase.SORT_REQUIREMENT.SALE:
			sortingData = equipData.tableData.sale;
			break;
		case SortBase.SORT_REQUIREMENT.SOCKET:
			sortingData = equipData.GetMaxSlot();
			break;
		case SortBase.SORT_REQUIREMENT.PRICE:
			sortingData = 0L;
			break;
		case SortBase.SORT_REQUIREMENT.NUM:
			sortingData = 1L;
			break;
		case SortBase.SORT_REQUIREMENT.ELEMENT:
			sortingData = 6L - (long)GetIconElement();
			break;
		case SortBase.SORT_REQUIREMENT.ELEM_ATK:
			sortingData = equipData.elemAtk;
			break;
		case SortBase.SORT_REQUIREMENT.ELEM_DEF:
			sortingData = equipData.elemDef;
			break;
		}
	}

	public override bool IsFavorite()
	{
		return equipData.isFavorite;
	}

	public override int GetItemType()
	{
		return EquipmentTypeToSortBaseType(equipData.tableData.type);
	}

	public override ulong GetUniqID()
	{
		return equipData.uniqueID;
	}

	public override uint GetTableID()
	{
		return equipData.tableID;
	}

	public override string GetName()
	{
		return equipData.tableData.name;
	}

	public override int GetIconID()
	{
		return equipData.tableData.GetIconID(MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex);
	}

	public override ITEM_ICON_TYPE GetIconType()
	{
		return ItemIcon.GetItemIconType(equipData.tableData.type);
	}

	public override ELEMENT_TYPE GetIconElement()
	{
		return equipData.GetTargetElementPriorityToTable();
	}

	public override int GetNum()
	{
		return -1;
	}

	public override RARITY_TYPE GetRarity()
	{
		return equipData.tableData.rarity;
	}

	public override int GetSalePrice()
	{
		return equipData.sellPrice;
	}

	public override bool CanSale()
	{
		return !IsEquipping() && !equipData.isFavorite;
	}

	public override int GetLevel()
	{
		return equipData.level;
	}

	public override ItemStatus GetItemStatus()
	{
		return equipData.GetEquipSkillParam();
	}

	public override REWARD_TYPE GetMaterialType()
	{
		return REWARD_TYPE.EQUIP_ITEM;
	}

	public override bool IsExceeded()
	{
		return equipData.exceed > 0;
	}

	public override GET_TYPE GetGetType()
	{
		return equipData.tableData.getType;
	}

	public override uint GetMainorSortWeight()
	{
		uint num = 0u;
		uint num2 = EquipmentTypeToMinorSortValue(equipData.tableData.type);
		num += num2 << 26;
		uint num3 = ElementTypeToMinorSortValue(GetIconElement());
		num += num3 << 23;
		uint rarity = (uint)GetRarity();
		num += rarity << 20;
		uint spAttackType = (uint)equipData.tableData.spAttackType;
		num += spAttackType << 15;
		uint level = (uint)equipData.level;
		num += level << 8;
		uint typeToMinorSortValue = GetTypeToMinorSortValue(GetGetType());
		return num + (typeToMinorSortValue << 7);
	}

	public override bool IsEquipping()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "StatusScene")
		{
			return MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocal(equipData) || IsVisualEquip();
		}
		return _EquipSomewhere() || IsVisualEquip();
	}

	public override bool IsEquipSomewhere()
	{
		bool flag = IsEquipping();
		if (!flag)
		{
			flag = _EquipSomewhere();
		}
		return flag;
	}

	private bool _EquipSomewhere()
	{
		int i = 0;
		for (int num = MonoBehaviourSingleton<StatusManager>.I.EquipSetNum(); i < num; i++)
		{
			int j = 0;
			for (int num2 = 7; j < num2; j++)
			{
				EquipItemInfo equippingItemInfo = MonoBehaviourSingleton<StatusManager>.I.GetEquippingItemInfo(j, i);
				if (equippingItemInfo != null && equippingItemInfo.uniqueID == equipData.uniqueID)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsVisualEquip()
	{
		if (MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSceneName() == "StatusScene")
		{
			return MonoBehaviourSingleton<StatusManager>.I.IsEquippingLocalVisual(equipData);
		}
		UserStatus userStatus = MonoBehaviourSingleton<UserInfoManager>.I.userStatus;
		EQUIPMENT_TYPE type = equipData.tableData.type;
		bool result = false;
		switch (type)
		{
		default:
			return false;
		case EQUIPMENT_TYPE.ARMOR:
		case EQUIPMENT_TYPE.VISUAL_ARMOR:
		{
			ulong result4 = 0uL;
			if (ulong.TryParse(userStatus.armorUniqId, out result4))
			{
				result = (result4 == GetUniqID());
			}
			break;
		}
		case EQUIPMENT_TYPE.HELM:
		case EQUIPMENT_TYPE.VISUAL_HELM:
		{
			ulong result5 = 0uL;
			if (ulong.TryParse(userStatus.helmUniqId, out result5))
			{
				result = (result5 == GetUniqID());
			}
			break;
		}
		case EQUIPMENT_TYPE.ARM:
		case EQUIPMENT_TYPE.VISUAL_ARM:
		{
			ulong result3 = 0uL;
			if (ulong.TryParse(userStatus.armUniqId, out result3))
			{
				result = (result3 == GetUniqID());
			}
			break;
		}
		case EQUIPMENT_TYPE.LEG:
		case EQUIPMENT_TYPE.VISUAL_LEG:
		{
			ulong result2 = 0uL;
			if (ulong.TryParse(userStatus.legUniqId, out result2))
			{
				result = (result2 == GetUniqID());
			}
			break;
		}
		}
		return result;
	}

	public ItemIconDetail.ICON_STATUS GetIconStatus()
	{
		bool flag = IsLvMaxAndEnableEvolve();
		bool flag2 = IsEnoughMaterial();
		if (equipData.IsLevelMax() && !equipData.tableData.IsEvolve() && equipData.IsExceedMax())
		{
			return ItemIconDetail.ICON_STATUS.GROW_MAX;
		}
		if (!flag2)
		{
			return ItemIconDetail.ICON_STATUS.NOT_ENOUGH_MATERIAL;
		}
		if (flag)
		{
			return ItemIconDetail.ICON_STATUS.VALID_EVOLVE;
		}
		return ItemIconDetail.ICON_STATUS.NONE;
	}

	private bool IsLvMaxAndEnableEvolve()
	{
		if (equipData.tableData.IsEvolve() && equipData.IsLevelMax())
		{
			return true;
		}
		return false;
	}

	private bool IsEnoughMaterial()
	{
		if (equipData.IsLevelMax())
		{
			if (!equipData.tableData.IsEvolve())
			{
				return true;
			}
			EvolveEquipItemTable.EvolveEquipItemData[] evolveEquipItemData = Singleton<EvolveEquipItemTable>.I.GetEvolveEquipItemData(equipData.tableID);
			if (evolveEquipItemData == null)
			{
				Debug.LogWarning((object)("Evolve Data is Not Found. : BaseItemID = " + equipData.tableID));
				return false;
			}
			int i = 0;
			for (int num = evolveEquipItemData.Length; i < num; i++)
			{
				if (MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(evolveEquipItemData[i].needMaterial) && MonoBehaviourSingleton<InventoryManager>.I.IsHaveingEquip(evolveEquipItemData[i].needEquip) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money >= (int)evolveEquipItemData[i].needMoney)
				{
					return true;
				}
			}
			return false;
		}
		return MonoBehaviourSingleton<InventoryManager>.I.IsHaveingMaterial(equipData.nextNeedTableData.needMaterial) && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.money >= equipData.nextNeedTableData.needMoney;
	}
}
