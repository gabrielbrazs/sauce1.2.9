using System;
using UnityEngine;

public class ItemDetailEquipAttachSkill : SkillInfoBase
{
	public enum UI
	{
		TGL_WINDOW_TITLE,
		TBL_SKILL_LIST,
		BTN_DETACH,
		BTN_ATTACH,
		BTN_GROW,
		OBJ_ANCHOR_BOTTOM,
		SPR_EQUIP_INDEX_ICON,
		OBJ_ICON_ROOT,
		LBL_EQUIP_NAME,
		LBL_EQUIP_NOW_LV,
		LBL_EQUIP_MAX_LV,
		GRD_ATTACH_SKILL,
		TBL_SPACE,
		OBJ_SPACE,
		OBJ_ITEM_ANCHOR_D,
		OBJ_SPACE_COLLISION,
		TGL_ACTIVE_OBJ,
		LBL_NAME,
		LBL_NAME_NOT_ENABLE_TYPE,
		LBL_NOW_LV,
		LBL_MAX_LV,
		LBL_EX_LV,
		TEX_SKILL_ICON,
		SPR_ENABLE_WEAPON_TYPE
	}

	private ItemDetailEquip.CURRENT_SECTION callSection;

	private object eventData;

	private object equipData;

	private bool isSkillUniqItem;

	private SkillSlotUIData[] slotData;

	private int selectIndex;

	private bool lookOnly = true;

	private int sex;

	public override void Initialize()
	{
		selectIndex = -1;
		object[] array = GameSection.GetEventData() as object[];
		callSection = (ItemDetailEquip.CURRENT_SECTION)(int)array[0];
		eventData = array[1];
		sex = -1;
		if (array.Length > 2)
		{
			sex = (int)array[2];
		}
		if (sex == -1)
		{
			sex = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.sex;
		}
		base.Initialize();
	}

	public override void UpdateUI()
	{
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		if (callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE || callSection == ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)
		{
			equipData = (eventData as EquipItemTable.EquipItemData);
			slotData = GetSkillSlotData(eventData as EquipItemTable.EquipItemData, 0);
			SetActive((Enum)UI.BTN_ATTACH, false);
			SetActive((Enum)UI.BTN_DETACH, false);
			SetActive((Enum)UI.BTN_GROW, false);
		}
		else if (callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE)
		{
			EquipItemAndSkillData equipItemAndSkillData = eventData as EquipItemAndSkillData;
			equipData = equipItemAndSkillData.equipItemInfo.tableData;
			slotData = equipItemAndSkillData.skillSlotUIData;
			isSkillUniqItem = true;
			SetActive((Enum)UI.BTN_ATTACH, false);
			SetActive((Enum)UI.BTN_DETACH, false);
			SetActive((Enum)UI.BTN_GROW, false);
		}
		else if (callSection == ItemDetailEquip.CURRENT_SECTION.QUEST_ROOM)
		{
			equipData = (eventData as EquipItemInfo);
			slotData = GetSkillSlotData(eventData as EquipItemInfo);
			isSkillUniqItem = true;
			SetActive((Enum)UI.BTN_ATTACH, false);
			SetActive((Enum)UI.BTN_DETACH, false);
			SetActive((Enum)UI.BTN_GROW, false);
		}
		else if (callSection == ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT)
		{
			EquipItemAndSkillData equipItemAndSkillData2 = eventData as EquipItemAndSkillData;
			equipData = equipItemAndSkillData2.equipItemInfo;
			slotData = equipItemAndSkillData2.skillSlotUIData;
			isSkillUniqItem = true;
			SetActive((Enum)UI.BTN_ATTACH, false);
			SetActive((Enum)UI.BTN_DETACH, false);
			SetActive((Enum)UI.BTN_GROW, false);
		}
		else
		{
			equipData = (eventData as EquipItemInfo);
			slotData = GetSkillSlotData(eventData as EquipItemInfo);
			isSkillUniqItem = true;
			SetActive((Enum)UI.BTN_ATTACH, true);
			SetActive((Enum)UI.BTN_DETACH, true);
			SetActive((Enum)UI.BTN_GROW, true);
			lookOnly = false;
		}
		SetToggle((Enum)UI.TGL_WINDOW_TITLE, lookOnly);
		if (slotData != null)
		{
			Transform table_item = null;
			SetTable(UI.TBL_SKILL_LIST, "EquipSetDetailTopItem", 1, false, delegate(int i, Transform t, bool is_recycle)
			{
				//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
				//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
				//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
				//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
				//IL_020c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0211: Unknown result type (might be due to invalid IL or missing references)
				//IL_022f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0234: Unknown result type (might be due to invalid IL or missing references)
				//IL_025a: Unknown result type (might be due to invalid IL or missing references)
				table_item = t;
				EquipItemTable.EquipItemData table = null;
				int num = 1;
				int num2 = 0;
				if (callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_CREATE || callSection == ItemDetailEquip.CURRENT_SECTION.SMITH_EVOLVE || callSection == ItemDetailEquip.CURRENT_SECTION.EQUIP_LIST)
				{
					table = (equipData as EquipItemTable.EquipItemData);
				}
				else
				{
					EquipItemInfo equipItemInfo = equipData as EquipItemInfo;
					table = equipItemInfo.tableData;
					num = equipItemInfo.level;
					num2 = equipItemInfo.exceed;
				}
				ItemIcon itemIcon = ItemIcon.CreateEquipItemIconByEquipItemTable(table, sex, FindCtrl(t, UI.OBJ_ICON_ROOT), null, -1, null, 0, false, -1, false, null, false, false);
				itemIcon.SetEnableCollider(false);
				SetActive(t, UI.SPR_EQUIP_INDEX_ICON, false);
				string name = table.name;
				name = Utility.TrimText(name, GetCtrl(UI.LBL_EQUIP_NAME).GetComponent<UILabel>());
				SetLabelText(t, UI.LBL_EQUIP_NAME, name);
				SetLabelText(t, UI.LBL_EQUIP_NOW_LV, num.ToString());
				SetLabelText(t, UI.LBL_EQUIP_MAX_LV, table.maxLv.ToString());
				SetGrid(t, UI.GRD_ATTACH_SKILL, "EquipSetDetailTopItem2", slotData.Length, true, delegate(int i2, Transform t2, bool is_recycle2)
				{
					SkillItemInfo skillItemInfo = slotData[i2].itemData;
					bool flag = skillItemInfo != null && skillItemInfo.tableData.type == slotData[i2].slotData.slotType;
					SetSkillIcon(t2, UI.TEX_SKILL_ICON, slotData[i2].slotData.slotType, flag, false);
					SetEvent(t2, (!flag) ? "SLOT" : "SLOT_DETAIL", i2);
					SetLongTouch(t2, "SLOT_DETAIL", i2);
					if (!flag)
					{
						skillItemInfo = null;
					}
					SetToggle(t2, UI.TGL_ACTIVE_OBJ, skillItemInfo != null);
					SetActive(t2, UI.LBL_NAME, true);
					SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, false);
					if (skillItemInfo == null)
					{
						SetLabelText(t2, UI.LBL_NAME, base.sectionData.GetText("EMPTY_SLOT"));
						SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, false);
					}
					else
					{
						SkillItemTable.SkillItemData tableData = skillItemInfo.tableData;
						SetLabelText(t2, UI.LBL_NAME, tableData.name);
						SetLabelText(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, tableData.name);
						SetLabelText(t2, UI.LBL_NOW_LV, skillItemInfo.level.ToString());
						SetLabelText(t2, UI.LBL_MAX_LV, skillItemInfo.tableData.GetMaxLv(skillItemInfo.exceedCnt).ToString());
						SetActive(t2, UI.LBL_EX_LV, skillItemInfo.IsExceeded());
						SetLabelText(t2, UI.LBL_EX_LV, StringTable.Format(STRING_CATEGORY.SMITH, 9u, skillItemInfo.exceedCnt));
						EQUIPMENT_TYPE? enableEquipType = skillItemInfo.tableData.GetEnableEquipType();
						SetActive(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.HasValue);
						if (enableEquipType.HasValue)
						{
							bool flag2 = enableEquipType.Value == table.type;
							SetSkillEquipIconKind(t2, UI.SPR_ENABLE_WEAPON_TYPE, enableEquipType.Value, flag2);
							SetActive(t2, UI.LBL_NAME, flag2);
							SetActive(t2, UI.LBL_NAME_NOT_ENABLE_TYPE, !flag2);
						}
					}
				});
				base.GetComponent<UITable>(t, (Enum)UI.TBL_SPACE).Reposition();
				Vector3 localPosition = t.get_localPosition();
				float y = localPosition.y;
				Vector3 localPosition2 = FindCtrl(t, UI.OBJ_SPACE).get_localPosition();
				float y2 = localPosition2.y;
				Vector3 localPosition3 = FindCtrl(t, UI.TBL_SPACE).get_localPosition();
				float num3 = y2 + localPosition3.y;
				Vector3 localPosition4 = FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).get_localPosition();
				localPosition4.y = (num3 - y) * 0.5f;
				FindCtrl(t, UI.SPR_EQUIP_INDEX_ICON).set_localPosition(localPosition4);
			});
			Transform val = FindCtrl(table_item, UI.OBJ_SPACE_COLLISION);
			Transform ctrl = GetCtrl(UI.OBJ_ANCHOR_BOTTOM);
			if (val != null && ctrl != null)
			{
				UpdateAnchors();
				ctrl.set_position(val.get_position());
				UpdateAnchors();
			}
		}
	}

	private void EmptySlot(Transform t, int slot_type)
	{
		SetLabelText(t, UI.LBL_NAME, base.sectionData.GetText("EMPTY_SLOT"));
		SetLabelText(t, UI.LBL_NOW_LV, string.Empty);
		SetLabelText(t, UI.LBL_MAX_LV, string.Empty);
		SetActive(t, UI.LBL_EX_LV, false);
		SetLabelText(t, UI.LBL_EX_LV, string.Empty);
	}

	private void OnQuery_SLOT()
	{
		selectIndex = (int)GameSection.GetEventData();
		if (lookOnly || selectIndex < 0)
		{
			GameSection.StopEvent();
		}
		else
		{
			EquipItemInfo equipItemInfo = eventData as EquipItemInfo;
			GameSection.ChangeEvent("ATTACH", new object[4]
			{
				callSection,
				slotData[selectIndex].itemData,
				equipItemInfo,
				selectIndex
			});
		}
	}

	private void OnQuery_SLOT_DETAIL()
	{
		selectIndex = (int)GameSection.GetEventData();
		ItemDetailEquip.CURRENT_SECTION cURRENT_SECTION = callSection;
		if (cURRENT_SECTION != ItemDetailEquip.CURRENT_SECTION.QUEST_RESULT)
		{
			cURRENT_SECTION = ItemDetailEquip.CURRENT_SECTION.STATUS_SKILL_LIST;
		}
		bool flag = false;
		if (isSkillUniqItem)
		{
			SkillItemInfo itemData = slotData[selectIndex].itemData;
			if (itemData != null)
			{
				SkillItemSortData skillItemSortData = new SkillItemSortData();
				skillItemSortData.SetItem(itemData);
				EquipItemInfo equipItemInfo = eventData as EquipItemInfo;
				GameSection.SetEventData(new object[4]
				{
					cURRENT_SECTION,
					skillItemSortData,
					equipItemInfo,
					selectIndex
				});
			}
			else
			{
				flag = true;
			}
		}
		else if (slotData[selectIndex].slotData.skill_id != 0)
		{
			SkillItemTable.SkillItemData skillItemData = Singleton<SkillItemTable>.I.GetSkillItemData(slotData[selectIndex].slotData.skill_id);
			GameSection.SetEventData(new object[2]
			{
				cURRENT_SECTION,
				skillItemData
			});
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			GameSection.StopEvent();
		}
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & (NOTIFY_FLAG.UPDATE_SKILL_CHANGE | NOTIFY_FLAG.UPDATE_SKILL_FAVORITE)) != (NOTIFY_FLAG)0L)
		{
			EquipItemInfo equipItemInfo = eventData as EquipItemInfo;
			if (equipItemInfo != null)
			{
				eventData = MonoBehaviourSingleton<InventoryManager>.I.equipItemInventory.Find(equipItemInfo.uniqueID);
			}
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_SKILL_CHANGE;
	}
}
