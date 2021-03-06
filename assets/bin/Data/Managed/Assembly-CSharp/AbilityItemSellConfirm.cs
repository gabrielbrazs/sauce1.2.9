using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AbilityItemSellConfirm : ItemSellConfirm
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

	private List<string> uniqs = new List<string>();

	public override string overrideBackKeyEvent => "NO";

	protected override bool isShowIconBG()
	{
		return false;
	}

	public override void Initialize()
	{
		List<AbilityItemSortData> source = GameSection.GetEventData() as List<AbilityItemSortData>;
		sellData = (from x in (IEnumerable<AbilityItemSortData>)source
		select (x)).ToList();
		base.isRareConfirm = false;
		sellData.ForEach(delegate(SortCompareData sort_data)
		{
			if (!base.isRareConfirm && GameDefine.IsRare(sort_data.GetRarity()))
			{
				base.isRareConfirm = true;
			}
		});
		base.Initialize();
	}

	protected override void DrawIcon()
	{
		base.DrawIcon();
		SetActive((Enum)UI.STR_NON_REWARD, true);
	}

	private void OnQuery_NO()
	{
		GameSection.SetEventData(sellData);
	}

	private void OnQuery_YES()
	{
		GameSection.SetEventData(null);
		uniqs.Clear();
		sellData.ForEach(delegate(SortCompareData sort_data)
		{
			uniqs.Add(sort_data.GetUniqID().ToString());
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
			GameSection.SetEventData(null);
			SendSell();
		}
	}

	private void SendSell()
	{
		GameSection.StayEvent();
		MonoBehaviourSingleton<ItemExchangeManager>.I.SendInventorySellAbilityItem(uniqs, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		});
	}

	public void OnQuery_AbilityItemSellIncludeRareConfirm_YES()
	{
		GameSection.SetEventData(null);
		SendSell();
	}
}
