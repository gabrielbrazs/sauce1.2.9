using System.Collections.Generic;
using UnityEngine;

public class UIExplorePlayerStatusList
{
	[SerializeField]
	private UIExplorePlayerStatus[] statuses = new UIExplorePlayerStatus[3];

	private ExploreStatus exploreStatus;

	public UIExplorePlayerStatusList()
		: this()
	{
	}

	public void Initialize(ExploreStatus exploreStatus)
	{
		if (this.exploreStatus != exploreStatus)
		{
			Clear();
			this.exploreStatus = exploreStatus;
			exploreStatus.onChangeExploreMemberList += OnChangeExploreMemberList;
		}
		OnChangeExploreMemberList();
	}

	private void Clear()
	{
		if (exploreStatus != null)
		{
			exploreStatus.onChangeExploreMemberList -= OnChangeExploreMemberList;
			exploreStatus = null;
		}
	}

	private void OnDestroy()
	{
		Clear();
	}

	private void OnChangeExploreMemberList()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < statuses.Length; i++)
		{
			statuses[i].get_gameObject().SetActive(false);
		}
		List<ExplorePlayerStatus> enabledPlayerStatusList = exploreStatus.GetEnabledPlayerStatusList();
		bool flag = false;
		for (int j = 0; j < enabledPlayerStatusList.Count; j++)
		{
			ExplorePlayerStatus explorePlayerStatus = enabledPlayerStatusList[j];
			if (explorePlayerStatus.isSelf)
			{
				flag = true;
			}
			else
			{
				int num = explorePlayerStatus.coopClient.slotIndex;
				if (num >= 0)
				{
					if (flag)
					{
						num--;
					}
					statuses[num].Initialize(explorePlayerStatus);
				}
			}
		}
	}
}
