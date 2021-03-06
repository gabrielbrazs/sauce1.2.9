using System;

public class QuestOrderSellSettings : GameSection
{
	private enum UI
	{
		LBL_ITEM_NUM,
		LBL_SALE_NUM,
		BTN_SALE_NUM_MINUS,
		BTN_SALE_NUM_PLUS,
		SLD_SALE_NUM,
		SPR_SALE_FRAME
	}

	private QuestInfoData quest;

	private int haveNum;

	private int sellNum;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		quest = (array[0] as QuestInfoData);
		haveNum = (int)array[1];
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.LBL_ITEM_NUM, string.Format("{0, 8:#,0}", haveNum));
		SetProgressInt((Enum)UI.SLD_SALE_NUM, 1, 1, haveNum, (EventDelegate.Callback)OnChagenSlider);
	}

	private void OnChagenSlider()
	{
		int progressInt = GetProgressInt((Enum)UI.SLD_SALE_NUM);
		SetLabelText((Enum)UI.LBL_SALE_NUM, string.Format("{0,8:#,0}", progressInt));
	}

	private void OnQuery_SALE_NUM_MINUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) - 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SALE_NUM_PLUS()
	{
		SetProgressInt((Enum)UI.SLD_SALE_NUM, GetProgressInt((Enum)UI.SLD_SALE_NUM) + 1, -1, -1, (EventDelegate.Callback)null);
	}

	private void OnQuery_SELL()
	{
		sellNum = GetProgressInt((Enum)UI.SLD_SALE_NUM);
		GameSection.SetEventData(new object[2]
		{
			quest.questData.tableData.questText,
			sellNum.ToString()
		});
	}

	public void OnQuery_QuestSellOrderConfirm_YES()
	{
		GameSection.SetEventData(new object[2]
		{
			quest.questData.tableData.questID,
			sellNum
		});
	}
}
