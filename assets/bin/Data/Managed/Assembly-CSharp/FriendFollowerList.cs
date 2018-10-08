using Network;
using System;
using UnityEngine;

public class FriendFollowerList : FollowListBase
{
	private int m_currentFollowerCount;

	private int m_maxFollowerCount = 1000;

	private int m_chunkSize = 100;

	protected bool IsConnect;

	private FriendCharaInfo[][] m_cachedUserDataList = new FriendCharaInfo[5][];

	public override void Initialize()
	{
		m_currentFollowerCount = MonoBehaviourSingleton<FriendManager>.I.followerNum;
		m_maxFollowerCount = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.FRIEND_MAX_FOLLOWER;
		for (int i = 0; i < m_cachedUserDataList.Length; i++)
		{
			m_cachedUserDataList[i] = new FriendCharaInfo[m_maxFollowerCount];
		}
		int followerListSortType = GameSaveData.instance.FollowerListSortType;
		if (0 <= followerListSortType && followerListSortType < 5)
		{
			m_currentSortType = (USER_SORT_TYPE)followerListSortType;
		}
		titleType = TITLE_TYPE.FOLLOWER;
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetActive((Enum)UI.OBJ_FOLLOW_NUMBER_ROOT, true);
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_NOW, m_currentFollowerCount.ToString());
		SetLabelText((Enum)UI.LBL_FOLLOW_NUMBER_MAX, m_maxFollowerCount.ToString());
		ListUI();
	}

	protected unsafe override void UpdateDynamicList()
	{
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		if (!currentUserArray.IsNullOrEmpty())
		{
			int currentPageItemLength = GetCurrentPageItemLength();
			FriendCharaInfo[] currentList = new FriendCharaInfo[currentPageItemLength];
			for (int i = 0; i < currentPageItemLength; i++)
			{
				currentList[i] = currentUserArray[nowPage * 10 + i];
			}
			if (GameDefine.ACTIVE_DEGREE)
			{
				UIGrid component = GetCtrl(UI.GRD_LIST).GetComponent<UIGrid>();
				component.cellHeight = (float)GameDefine.DEGREE_FRIEND_LIST_HEIGHT;
			}
			CleanItemList();
			_003CUpdateDynamicList_003Ec__AnonStorey31E _003CUpdateDynamicList_003Ec__AnonStorey31E;
			SetDynamicList((Enum)UI.GRD_LIST, GetListItemName, currentPageItemLength, false, null, null, new Action<int, Transform, bool>((object)_003CUpdateDynamicList_003Ec__AnonStorey31E, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	private int GetCurrentPageItemLength()
	{
		return GetPageItemLength(nowPage);
	}

	private int GetPageItemLength(int currentPage)
	{
		return (currentPage + 1 != pageNumMax || m_currentFollowerCount % 10 <= 0) ? 10 : (m_currentFollowerCount % 10);
	}

	private int GetChunkIndex(int pageIndex)
	{
		if (m_chunkSize < 1 || pageIndex < 0)
		{
			return 0;
		}
		return Mathf.FloorToInt((float)(pageIndex * 10 / m_chunkSize));
	}

	protected unsafe override void SendGetList(int page, Action<bool> callback)
	{
		if (!IsConnect)
		{
			int chunkIndex = GetChunkIndex(page);
			IsConnect = true;
			_003CSendGetList_003Ec__AnonStorey31F _003CSendGetList_003Ec__AnonStorey31F;
			MonoBehaviourSingleton<FriendManager>.I.SendGetFollowerList(chunkIndex, (int)m_currentSortType, new Action<bool, FriendFollowerListModel.Param>((object)_003CSendGetList_003Ec__AnonStorey31F, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}
	}

	protected override void PostSendGetListByReopen(int page)
	{
		SetDirtyTable();
		base.PostSendGetListByReopen(page);
	}

	public override void OnNotify(NOTIFY_FLAG flags)
	{
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_PARAM) != (NOTIFY_FLAG)0L)
		{
			isInitializeSendReopen = true;
		}
		if ((flags & NOTIFY_FLAG.UPDATE_FRIEND_LIST) != (NOTIFY_FLAG)0L)
		{
			SetDirtyTable();
		}
		base.OnNotify(flags);
	}

	protected override NOTIFY_FLAG GetUpdateUINotifyFlags()
	{
		return NOTIFY_FLAG.UPDATE_FRIEND_LIST;
	}

	public override void OnQuery_FOLLOW_INFO()
	{
		int num = (int)GameSection.GetEventData();
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		int num2 = num + nowPage * 10;
		if (num2 >= 0 && !currentUserArray.IsNullOrEmpty() && currentUserArray.Length > num2)
		{
			FriendCharaInfo eventData = currentUserArray[num2];
			MonoBehaviourSingleton<StatusManager>.I.otherEquipSetSaveIndex = num + 4;
			GameSection.SetEventData(eventData);
		}
	}

	protected override FriendCharaInfo[] GetCurrentUserArray()
	{
		if (m_currentFollowerCount <= 0)
		{
			return null;
		}
		return m_cachedUserDataList[(int)m_currentSortType];
	}

	private bool HasNullOrEmptyData(int _nextPage)
	{
		FriendCharaInfo[] currentUserArray = GetCurrentUserArray();
		if (currentUserArray == null)
		{
			return true;
		}
		int num = _nextPage * 10;
		int pageItemLength = GetPageItemLength(_nextPage);
		for (int i = 0; i < pageItemLength; i++)
		{
			if (currentUserArray[num + i] == null)
			{
				return true;
			}
		}
		return false;
	}

	protected override void OnQuery_PAGE_PREV()
	{
		if (!IsConnect)
		{
			int num = (nowPage - 1 + pageNumMax) % pageNumMax;
			if (!HasNullOrEmptyData(num))
			{
				nowPage = num;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
			}
			else
			{
				base.OnQuery_PAGE_PREV();
			}
		}
	}

	protected override void OnQuery_PAGE_NEXT()
	{
		if (!IsConnect)
		{
			int num = (nowPage + 1) % pageNumMax;
			if (!HasNullOrEmptyData(num))
			{
				nowPage = num;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
			}
			else
			{
				base.OnQuery_PAGE_NEXT();
			}
		}
	}

	protected override void OnQuery_SORT()
	{
		if (m_currentFollowerCount > 0)
		{
			if (!IsConnect)
			{
				UpdateSortType();
				GameSaveData.instance.SetFollowerListSortType((int)m_currentSortType);
				if (!HasNullOrEmptyData(nowPage))
				{
					MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GetUpdateUINotifyFlags());
				}
				else
				{
					GameSection.StayEvent();
					SendGetList(nowPage, delegate(bool ret)
					{
						GameSection.ResumeEvent(ret, null);
					});
				}
			}
		}
		else
		{
			MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, "You have no Follower Hunters", StringTable.Get(STRING_CATEGORY.COMMON_DIALOG, 100u), null, null, null), delegate
			{
			}, false, 0);
		}
	}
}
