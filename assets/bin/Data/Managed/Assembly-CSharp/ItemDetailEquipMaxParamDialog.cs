using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetailEquipMaxParamDialog : ItemDetailEquipDialog
{
	private List<List<EquipItemTable.EquipItemData>> allEquipData;

	private int currentEvolveStage;

	private int currentEvolveIndex;

	private Transform root;

	private uint materialId;

	public override void Initialize()
	{
		object[] array = (object[])GameSection.GetEventData();
		EquipItemTable.EquipItemData item = array[1] as EquipItemTable.EquipItemData;
		List<EquipItemTable.EquipItemData> list = new List<EquipItemTable.EquipItemData>();
		list.Add(item);
		allEquipData = new List<List<EquipItemTable.EquipItemData>>();
		allEquipData.Add(list);
		AddNextEvolveDataRecursive(list);
		currentEvolveStage = 0;
		currentEvolveIndex = 0;
		root = SetPrefab(base.collectUI, "ItemDetailEquipMaxParamDialog", true);
		materialId = (uint)array[2];
		base.Initialize();
	}

	protected override void EquipTableParam(EquipItemTable.EquipItemData table_data)
	{
		base.EquipTableParam(table_data);
		GrowEquipItemTable.GrowEquipItemData growEquipItemData = Singleton<GrowEquipItemTable>.I.GetGrowEquipItemData(table_data.growID, (uint)table_data.maxLv);
		int growParamAtk = growEquipItemData.GetGrowParamAtk(table_data.baseAtk);
		int growParamDef = growEquipItemData.GetGrowParamDef(table_data.baseDef);
		int[] growParamElemAtk = growEquipItemData.GetGrowParamElemAtk(table_data.atkElement);
		int num = Mathf.Max(growParamElemAtk);
		int[] growParamElemDef = growEquipItemData.GetGrowParamElemDef(table_data.defElement);
		int num2 = Mathf.Max(growParamElemDef);
		int growParamHp = growEquipItemData.GetGrowParamHp(table_data.baseHp);
		SetActive(detailBase, UI.STR_LV, true);
		SetActive(detailBase, UI.STR_ONLY_VISUAL, false);
		SetLabelText(detailBase, UI.LBL_LV_NOW, table_data.maxLv.ToString());
		SetLabelText(detailBase, UI.LBL_ATK, growParamAtk.ToString());
		SetLabelText(detailBase, UI.LBL_ELEM, num.ToString());
		SetLabelText(detailBase, UI.LBL_DEF, growParamDef.ToString());
		SetLabelText(detailBase, UI.LBL_ELEM_DEF, num2.ToString());
		SetLabelText(detailBase, UI.LBL_HP, growParamHp.ToString());
		SetActive(detailBase, UI.OBJ_FAVORITE_ROOT, false);
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		DisableMagiSlotButton();
		UpdatePaging();
	}

	private void DisableMagiSlotButton()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		Transform ctrl = GetCtrl(UI.OBJ_SKILL_BUTTON_ROOT);
		Transform val = ctrl.FindChild("SkillIconButton");
		base.SetEnabled<UIButton>(val, false);
		val.GetComponent<BoxCollider>().set_enabled(false);
	}

	private void UpdatePaging()
	{
		bool is_visible = allEquipData.Count + allEquipData[currentEvolveStage].Count >= 2;
		SetActive((Enum)UI.OBJ_EVOLVE_SELECT, true);
		SetActive((Enum)UI.OBJ_ARROW_BTN_ROOT, is_visible);
		if (currentEvolveStage == 0)
		{
			SetActive((Enum)UI.LBL_EVOLVE_NORMAL, true);
			SetLabelText((Enum)UI.LBL_EVOLVE_NORMAL, StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 13u));
			SetActive((Enum)UI.LBL_EVOLVE_ATTRIBUTE, false);
			SetActive((Enum)UI.SPR_EVOLVE_ELEM, false);
		}
		else
		{
			EquipItemTable.EquipItemData currentEquipItemData = GetCurrentEquipItemData();
			int targetElementPriorityToTable = (int)currentEquipItemData.GetTargetElementPriorityToTable();
			bool flag = targetElementPriorityToTable == 6;
			SetActive((Enum)UI.LBL_EVOLVE_NORMAL, flag);
			SetActive((Enum)UI.LBL_EVOLVE_ATTRIBUTE, !flag);
			SetActive((Enum)UI.SPR_EVOLVE_ELEM, !flag);
			if (flag)
			{
				SetLabelText((Enum)UI.LBL_EVOLVE_NORMAL, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 14u), currentEvolveStage.ToString()));
			}
			else
			{
				SetLabelText((Enum)UI.LBL_EVOLVE_ATTRIBUTE, string.Format(StringTable.Get(STRING_CATEGORY.TEXT_SCRIPT, 15u), currentEvolveStage.ToString()));
				SetElementSprite((Enum)UI.SPR_EVOLVE_ELEM, targetElementPriorityToTable);
			}
		}
	}

	private void OnQuery_NEXT_EVOLVE()
	{
		currentEvolveIndex++;
		if (currentEvolveIndex >= allEquipData[currentEvolveStage].Count)
		{
			currentEvolveIndex = 0;
			currentEvolveStage++;
		}
		if (currentEvolveStage >= allEquipData.Count)
		{
			currentEvolveStage = 0;
		}
		UpdateDetail(GetCurrentEquipItemData());
	}

	private void OnQuery_PRE_EVOLVE()
	{
		bool flag = false;
		currentEvolveIndex--;
		if (currentEvolveIndex < 0)
		{
			currentEvolveIndex = 0;
			flag = true;
			currentEvolveStage--;
		}
		if (currentEvolveStage < 0)
		{
			currentEvolveStage = allEquipData.Count - 1;
		}
		if (flag)
		{
			currentEvolveIndex = allEquipData[currentEvolveStage].Count - 1;
		}
		UpdateDetail(GetCurrentEquipItemData());
	}

	private void OnQuery_LOTTERY_LIST()
	{
		CreateEquipItemTable.CreateEquipItemData createEquipItemByPart = Singleton<CreateEquipItemTable>.I.GetCreateEquipItemByPart(materialId, GetCurrentEquipItemData().type);
		if (createEquipItemByPart == null)
		{
			GameSection.StopEvent();
		}
		else
		{
			GameSection.SetEventData(createEquipItemByPart);
		}
	}

	private EquipItemTable.EquipItemData GetCurrentEquipItemData()
	{
		return allEquipData[currentEvolveStage][currentEvolveIndex];
	}

	private void UpdateDetail(EquipItemTable.EquipItemData data)
	{
		detailItemData = data;
		RefreshUI();
	}

	private EquipItemTable.EquipItemData[] GetNextEvolveData(EquipItemTable.EquipItemData tableData)
	{
		EvolveEquipItemTable.EvolveEquipItemData[] evolveTable = tableData.GetEvolveTable();
		if (evolveTable == null)
		{
			return null;
		}
		EquipItemTable.EquipItemData[] array = new EquipItemTable.EquipItemData[evolveTable.Length];
		for (int i = 0; i < evolveTable.Length; i++)
		{
			array[i] = Singleton<EquipItemTable>.I.GetEquipItemData(evolveTable[i].equipEvolveItemID);
		}
		return array;
	}

	private void AddNextEvolveDataRecursive(List<EquipItemTable.EquipItemData> tabledata)
	{
		if (tabledata != null && tabledata.Count > 0)
		{
			List<EquipItemTable.EquipItemData> list = new List<EquipItemTable.EquipItemData>();
			HashSet<uint> hashSet = new HashSet<uint>();
			int i = 0;
			for (int count = tabledata.Count; i < count; i++)
			{
				EquipItemTable.EquipItemData[] nextEvolveData = GetNextEvolveData(tabledata[i]);
				if (nextEvolveData != null)
				{
					int j = 0;
					for (int num = nextEvolveData.Length; j < num; j++)
					{
						if (hashSet.Add(nextEvolveData[j].id))
						{
							list.Add(nextEvolveData[j]);
						}
					}
				}
			}
			if (list.Count >= 1)
			{
				allEquipData.Add(list);
			}
			AddNextEvolveDataRecursive(list);
		}
	}
}
