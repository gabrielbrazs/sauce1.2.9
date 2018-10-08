using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageUserUIController
{
	public class InitParam
	{
		public UIWidget RootWidget;

		public Transform ItemListParent;

		public int ItemVisibleCount;

		public Action OnClickItem;
	}

	private static readonly string MESSAGE_USER_ITEM_PREFAB_PATH = "InternalUI/UI_Friend/FollowListBaseItem";

	private GameObject m_userItemPrefab;

	private bool m_isConnecting;

	private UIWidget m_rootWidget;

	private Transform m_itemListParent;

	private int m_visibleItemCount;

	private List<FriendMessageUserListModel.MessageUserInfo> m_apiResponce;

	private List<HomeMutualFollowerListItem> m_currentItemList = new List<HomeMutualFollowerListItem>();

	private Queue<HomeMutualFollowerListItem> m_itemPool = new Queue<HomeMutualFollowerListItem>();

	private Action m_OnClickItem;

	public bool IsConnecting => m_isConnecting;

	public ChatMessageUserUIController()
	{
	}

	public ChatMessageUserUIController(InitParam _param)
	{
		if (_param != null)
		{
			m_rootWidget = _param.RootWidget;
			m_itemListParent = _param.ItemListParent;
			m_visibleItemCount = _param.ItemVisibleCount;
			m_OnClickItem = _param.OnClickItem;
		}
	}

	private void SetStartConnecting()
	{
		m_isConnecting = true;
	}

	private void SetEndConnecting()
	{
		m_isConnecting = false;
	}

	public IEnumerator LoadInternalResources(MonoBehaviour _coroutineExecutor)
	{
		if (!(m_userItemPrefab != null))
		{
			LoadingQueue load_queue = new LoadingQueue(_coroutineExecutor);
			LoadObject loadObject_ListItem = load_queue.Load(RESOURCE_CATEGORY.UI, "FollowListBaseItem", false);
			if (load_queue.IsLoading())
			{
				yield return (object)load_queue.Wait();
			}
			m_userItemPrefab = (loadObject_ListItem.loadedObject as GameObject);
		}
	}

	public unsafe IEnumerator SendRequestMessagingPersonList(MonoBehaviour _coroutineExecutor)
	{
		if (MonoBehaviourSingleton<FriendManager>.IsValid())
		{
			if (m_userItemPrefab == null)
			{
				yield return (object)LoadInternalResources(_coroutineExecutor);
			}
			SetStartConnecting();
			bool isCalledByOther = true;
			MonoBehaviourSingleton<FriendManager>.I.SendGetUserListMessagedOnce(isCalledByOther, new Action<bool, FriendMessagedMutualFollowerListModel.Param>((object)/*Error near IL_008a: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			while (IsConnecting)
			{
				yield return (object)null;
			}
		}
	}

	public void HideAll()
	{
	}

	public void ShowAll()
	{
	}

	protected unsafe void GenerateMessageUserList(List<FriendMessageUserListModel.MessageUserInfo> recv_data)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Expected O, but got Unknown
		SetRootAlpha(0f);
		RecycleItem();
		int len = (recv_data != null) ? recv_data.Count : 0;
		m_currentItemList = GetItemObjects(len, m_itemListParent);
		int loadCompleteCount = 0;
		_003CGenerateMessageUserList_003Ec__AnonStorey2C0 _003CGenerateMessageUserList_003Ec__AnonStorey2C;
		for (int i = 0; i < len; i++)
		{
			HomeMutualFollowerListItem homeMutualFollowerListItem = m_currentItemList[i];
			homeMutualFollowerListItem.get_transform().set_localPosition(Vector3.get_down() * ((float)i * 130f));
			homeMutualFollowerListItem.get_transform().set_localScale(Vector3.get_one() * 0.98f);
			HomeMutualFollowerListItem.InitParam initParam = new HomeMutualFollowerListItem.InitParam();
			initParam.CharacterInfo = recv_data[i];
			initParam.Index = i;
			initParam.IsFollower = recv_data[i].follower;
			initParam.IsFollowing = recv_data[i].following;
			initParam.NoReadMsgNum = recv_data[i].noReadNum;
			initParam.IsPermittedMessage = recv_data[i].isPermitted;
			initParam.IsUseRenderTextureCharaModel = (!FieldManager.IsValidInField() && !FieldManager.IsValidInGame() && !FieldManager.IsValidInTutorial());
			initParam.OnClickItem = OnClickItem;
			initParam.OnCompleteLoading = new Action((object)_003CGenerateMessageUserList_003Ec__AnonStorey2C, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			homeMutualFollowerListItem.Initialize(initParam);
		}
	}

	private void RecycleItem()
	{
		int i = 0;
		for (int count = m_currentItemList.Count; i < count; i++)
		{
			m_currentItemList[i].HideAll();
			m_itemPool.Enqueue(m_currentItemList[i]);
		}
		m_currentItemList.Clear();
	}

	private List<HomeMutualFollowerListItem> GetItemObjects(int _requestCount, Transform _parentObj)
	{
		List<HomeMutualFollowerListItem> list = new List<HomeMutualFollowerListItem>();
		if (_parentObj == null)
		{
			return list;
		}
		if (_requestCount < m_itemPool.Count)
		{
			for (int i = 0; i < _requestCount; i++)
			{
				list.Add(m_itemPool.Dequeue());
			}
			return list;
		}
		while (m_itemPool.Count > 0)
		{
			list.Add(m_itemPool.Dequeue());
		}
		if (m_userItemPrefab == null)
		{
			return list;
		}
		int num = _requestCount - list.Count;
		for (int j = 0; j < num; j++)
		{
			Transform val = ResourceUtility.Realizes(m_userItemPrefab, _parentObj, 5);
			if (!(val == null))
			{
				HomeMutualFollowerListItem component = val.GetComponent<HomeMutualFollowerListItem>();
				if (!(component == null))
				{
					list.Add(component);
				}
			}
		}
		return list;
	}

	protected void OnClickItem(int _itemIndex)
	{
		if (m_apiResponce != null && _itemIndex >= 0 && m_apiResponce.Count > _itemIndex)
		{
			FriendMessageUserListModel.MessageUserInfo messageUserInfo = m_apiResponce[_itemIndex];
			if (messageUserInfo != null)
			{
				if (!messageUserInfo.isPermitted)
				{
					string errorMessage = StringTable.GetErrorMessage(13022u);
					MonoBehaviourSingleton<GameSceneManager>.I.OpenCommonDialog(new CommonDialog.Desc(CommonDialog.TYPE.OK, errorMessage, null, null, null, null), delegate
					{
					}, true, 13022);
				}
				else if (MonoBehaviourSingleton<FriendManager>.IsValid())
				{
					MonoBehaviourSingleton<FriendManager>.I.SendGetMessageDetailList(messageUserInfo.userId, 0, true, delegate
					{
						if (m_OnClickItem != null)
						{
							m_OnClickItem.Invoke();
						}
					});
				}
			}
		}
	}

	private void SetRootAlpha(float _value)
	{
		if (!(m_rootWidget == null))
		{
			float alpha = Mathf.Clamp01(_value);
			m_rootWidget.alpha = alpha;
		}
	}

	public void ClearList()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		int i = 0;
		for (int count = m_currentItemList.Count; i < count; i++)
		{
			m_currentItemList[i].CleanRenderTexture();
			Object.Destroy(m_currentItemList[i].get_gameObject());
			m_currentItemList[i] = null;
		}
		m_currentItemList.Clear();
	}
}