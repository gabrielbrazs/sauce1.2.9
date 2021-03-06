using System;

public class ItemIconDetailItemSetupper : ItemIconDetailSetuperBase
{
	public UILabel lblNum;

	public UILabel lblDescription;

	public UILabel lblEndDate;

	public override void Set(object[] data = null)
	{
		base.Set(null);
		ItemTable.ItemData itemData = data[0] as ItemTable.ItemData;
		int num = (int)data[1];
		bool flag = (bool)data[2];
		SetName(itemData.name);
		SetVisibleBG(true);
		if (flag)
		{
			SetActiveInfo(0);
			lblNum.text = num.ToString();
			if (itemData.endDate != default(DateTime))
			{
				lblEndDate.text = "Valid till " + itemData.endDate.ToString("yyyy/MM/dd HH:mm");
			}
			else
			{
				lblEndDate.text = string.Empty;
			}
		}
		else
		{
			SetActiveInfo(1);
			SetDescription(itemData.text);
		}
	}

	public void SetActiveInfo(int activeIndex)
	{
		for (int i = 0; i < infoRootAry.Length; i++)
		{
			infoRootAry[i].SetActive(i == activeIndex);
		}
	}

	public void SetDescription(string text)
	{
		lblDescription.text = text;
	}
}
