using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ItemStorageSellConfirm : ItemSellConfirm
{
	public new enum UI
	{
		STR_INCLUDE_RARE,
		STR_MAIN_TEXT,
		STR_TITLE_R,
		GRD_ICON,
		LBL_TOTAL,
		OBJ_GOLD,
		BTN_0,
		BTN_1,
		BTN_CENTER,
		SCR_ICON,
		GRD_REWARD_ICON,
		STR_NON_REWARD
	}

	public enum GO_BACK
	{
		TOP,
		SELL
	}

	private ItemStorageTop.TAB_MODE tab;

	private List<string> uniqs = new List<string>();

	private List<int> nums = new List<int>();

	private GO_BACK goBackTo;

	public override IEnumerable<string> requireDataTable
	{
		get
		{
			yield return "EquipItemExceedTable";
			foreach (string item in base.requireDataTable)
			{
				yield return item;
			}
		}
	}

	public override string overrideBackKeyEvent => "NO";

	protected override bool isShowIconBG()
	{
		return false;
	}

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		tab = (ItemStorageTop.TAB_MODE)(int)array[0];
		sellData = (array[1] as List<SortCompareData>);
		if (array.Length > 2)
		{
			goBackTo = (GO_BACK)(int)array[2];
		}
		base.isRareConfirm = false;
		base.isEquipConfirm = false;
		base.isExceedConfirm = false;
		base.isExceedEquipmentConfirm = false;
		sellData.ForEach(delegate(SortCompareData sort_data)
		{
			if (!base.isRareConfirm || !base.isEquipConfirm || (!base.isExceedConfirm && !base.isExceedEquipmentConfirm))
			{
				if (!base.isRareConfirm && GameDefine.IsRare(sort_data.GetRarity()))
				{
					base.isRareConfirm = true;
				}
				if (!base.isEquipConfirm && sort_data.IsEquipping())
				{
					base.isEquipConfirm = true;
				}
				if (!base.isExceedConfirm && !base.isExceedEquipmentConfirm && sort_data.IsExceeded())
				{
					if (sort_data.GetMaterialType() == REWARD_TYPE.EQUIP_ITEM)
					{
						base.isExceedEquipmentConfirm = true;
					}
					else
					{
						base.isExceedConfirm = true;
					}
				}
			}
		});
		base.Initialize();
	}

	protected override void DrawIcon()
	{
		base.DrawIcon();
		NeedMaterial[] reward_ary = CreateNeedMaterialAry();
		int sELL_SELECT_MAX = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.SELL_SELECT_MAX;
		SetGrid(UI.GRD_REWARD_ICON, null, sELL_SELECT_MAX, false, delegate(int i, Transform t, bool is_recycle)
		{
			if (i < reward_ary.Length)
			{
				NeedMaterial needMaterial = reward_ary[i];
				ItemIcon itemIcon = ItemIcon.CreateRewardItemIcon(REWARD_TYPE.ITEM, needMaterial.itemID, t, needMaterial.num, "NONE", 0, false, -1, false, null, false, false, ItemIcon.QUEST_ICON_SIZE_TYPE.DEFAULT);
				itemIcon.SetRewardBG(true);
				Transform ctrl = GetCtrl(UI.GRD_REWARD_ICON);
				SetMaterialInfo(itemIcon.transform, REWARD_TYPE.ITEM, needMaterial.itemID, ctrl);
			}
			else
			{
				SetActive(t, false);
			}
		});
		SetActive((Enum)UI.STR_NON_REWARD, reward_ary.Length == 0);
	}

	protected virtual NeedMaterial[] CreateNeedMaterialAry()
	{
		SortCompareData[] array = sellData.ToArray();
		List<NeedMaterial> reward = new List<NeedMaterial>();
		Array.ForEach(array, delegate(SortCompareData _data)
		{
			EquipItemInfo equipItemInfo = _data.GetItemData() as EquipItemInfo;
			if (equipItemInfo != null)
			{
				uint lapis_id = 0u;
				EquipItemExceedTable.EquipItemExceedData equipItemExceedData = Singleton<EquipItemExceedTable>.I.GetEquipItemExceedData(equipItemInfo.tableData.rarity, equipItemInfo.tableData.getType, equipItemInfo.tableData.eventId);
				if (equipItemExceedData != null)
				{
					lapis_id = equipItemExceedData.exchangeItemId;
				}
				if (lapis_id != 0)
				{
					ItemTable.ItemData itemData = Singleton<ItemTable>.I.GetItemData(lapis_id);
					if (itemData != null)
					{
						NeedMaterial needMaterial = reward.Find((NeedMaterial regist_lapis) => regist_lapis.itemID == lapis_id);
						if (needMaterial == null)
						{
							reward.Add(new NeedMaterial(lapis_id, 1));
						}
						else
						{
							needMaterial.num++;
						}
					}
				}
			}
		});
		return reward.ToArray();
	}

	private void OnQuery_NO()
	{
		ChangeEventForGoBack();
		GameSection.SetEventData(sellData);
	}

	private void OnQuery_YES()
	{
		GameSection.SetEventData(null);
		uniqs.Clear();
		sellData.ForEach(delegate(SortCompareData sort_data)
		{
			uniqs.Add(sort_data.GetUniqID().ToString());
			nums.Add(1);
		});
		if (base.isRareConfirm || base.isEquipConfirm || base.isExceedConfirm || base.isExceedEquipmentConfirm)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.sectionData.GetText("TEXT_CONFIRM"));
			if (base.isRareConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_RARE"));
			}
			if (base.isEquipConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_EQUIP"));
			}
			if (base.isExceedConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_EXCEED"));
			}
			if (base.isExceedEquipmentConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_EXCEED_EQUIP"));
			}
			stringBuilder.AppendLine(string.Empty);
			stringBuilder.Append(base.sectionData.GetText("TEXT_GROW"));
			GameSection.ChangeEvent("INCLUDE_RARE_CONFIRM", stringBuilder.ToString());
		}
		else
		{
			ChangeEventForGoBack();
			GameSection.SetEventData(null);
			SendSell();
		}
	}

	private void SendSell()
	{
		if (tab == ItemStorageTop.TAB_MODE.EQUIP)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellEquipItem(uniqs, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
		else if (tab == ItemStorageTop.TAB_MODE.MATERIAL)
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellItem(uniqs, nums, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
		else
		{
			GameSection.StayEvent();
			MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellSkillItem(uniqs, delegate(bool is_success)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	public void OnQuery_ItemStorageSellIncludeRareConfirm_YES()
	{
		ChangeEventForGoBack();
		GameSection.SetEventData(null);
		SendSell();
	}

	public void OnQuery_ItemStorageSellIncludeRareConfirm_NO()
	{
		ChangeEventForGoBack();
	}

	protected virtual void ChangeEventForGoBack()
	{
		GameSection.ChangeEvent(GameSceneEvent.current.eventName + "_" + goBackTo, null);
	}
}
