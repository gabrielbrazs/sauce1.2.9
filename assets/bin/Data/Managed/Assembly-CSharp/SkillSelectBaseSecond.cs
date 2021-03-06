using System;
using UnityEngine;

public abstract class SkillSelectBaseSecond : SkillSelectBase
{
	protected new enum UI
	{
		OBJ_DETAIL_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL,
		LBL_NAME,
		LBL_LV_NOW,
		LBL_LV_MAX,
		LBL_ATK,
		LBL_DEF,
		LBL_HP,
		LBL_SELL,
		LBL_DESCRIPTION,
		OBJ_FAVORITE_ROOT,
		TWN_FAVORITE,
		TWN_UNFAVORITE,
		OBJ_SUB_STATUS,
		SPR_SKILL_TYPE_ICON,
		SPR_SKILL_TYPE_ICON_BG,
		SPR_SKILL_TYPE_ICON_RARITY,
		STR_TITLE_ITEM_INFO,
		STR_TITLE_DESCRIPTION,
		STR_TITLE_STATUS,
		STR_TITLE_SELL,
		PRG_EXP_BAR,
		OBJ_NEXT_EXP_ROOT,
		BTN_DECISION,
		STR_DECISION_R,
		BTN_SKILL_DECISION,
		STR_SKILL_DECISION,
		STR_SKILL_DECISION_R,
		OBJ_SKILL_INFO_ROOT,
		LBL_EQUIP_ITEM_NAME,
		SCR_INVENTORY,
		GRD_INVENTORY,
		GRD_INVENTORY_SMALL,
		LBL_SORT,
		BTN_BACK,
		TGL_CHANGE_INVENTORY,
		TGL_ICON_ASC,
		OBJ_EMPTY_SKILL_ROOT,
		SPR_EMPTY_SKILL,
		LBL_EMPTY_SKILL_TYPE,
		OBJ_CAPTION_3,
		LBL_CAPTION
	}

	protected bool isVisibleEmptySkill;

	public override void Initialize()
	{
		InitializeCaption();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected void SetVisibleEmptySkillType(bool is_visible, int index = 0)
	{
		isVisibleEmptySkill = is_visible;
		SetActive((Enum)UI.OBJ_EMPTY_SKILL_ROOT, is_visible);
		if (is_visible)
		{
			SKILL_SLOT_TYPE sKILL_SLOT_TYPE = SKILL_SLOT_TYPE.NONE;
			if (equipItem != null)
			{
				SkillItemTable.SkillSlotData[] skillSlot = equipItem.tableData.GetSkillSlot(equipItem.exceed);
				if (skillSlot == null || skillSlot.Length <= index)
				{
					SetActive((Enum)UI.OBJ_EMPTY_SKILL_ROOT, false);
					return;
				}
				sKILL_SLOT_TYPE = skillSlot[index].slotType;
			}
			SetLabelText((Enum)UI.LBL_EMPTY_SKILL_TYPE, MonoBehaviourSingleton<StatusManager>.I.GetSkillItemGroupString(sKILL_SLOT_TYPE));
			SetSprite((Enum)UI.SPR_EMPTY_SKILL, UIBehaviour.GetSkillIconSpriteName(sKILL_SLOT_TYPE, true, true));
		}
	}

	protected override void UpdateParam()
	{
	}

	protected override void OnQuery_SELECT()
	{
		int num = (int)GameSection.GetEventData();
		if (num >= 0)
		{
			selectIndex = num;
			selectSkillItem = (inventory.datas[selectIndex].GetItemData() as SkillItemInfo);
		}
		else
		{
			selectIndex = -1;
			selectSkillItem = null;
		}
		if (!CheckApplicationVersion())
		{
			GameSection.StopEvent();
		}
		else
		{
			OnDecision();
		}
	}

	protected virtual bool CheckApplicationVersion()
	{
		return true;
	}

	private void InitializeCaption()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_CAPTION_3);
		string text = base.sectionData.GetText("CAPTION");
		SetLabelText(ctrl, UI.LBL_CAPTION, text);
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
}
