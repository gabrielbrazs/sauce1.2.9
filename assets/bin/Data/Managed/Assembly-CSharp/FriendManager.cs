using Network;
using System;
using System.Collections.Generic;

public class FriendManager : MonoBehaviourSingleton<FriendManager>
{
	public const int DRAW_FOLLOW_MAX = 10;

	private FriendFollowListModel.Param recvFollowList;

	private FriendFollowListModel.Param recvFollowerList;

	private FriendMessageUserListModel.Param recvMessageUserList;

	private string mutualFollowValue = string.Empty;

	private FriendSearchResult recvSearchList;

	public HomeCharaInfoList homeCharas
	{
		get;
		private set;
	}

	public FriendMessageUserListModel.MessageUserInfo talkUser
	{
		get;
		private set;
	}

	public FriendFollowLinkResult followLinkResult
	{
		get;
		private set;
	}

	public FriendMutualFollowResult mutualFollowResult
	{
		get;
		private set;
	}

	public string MutualFollowValue
	{
		get
		{
			return mutualFollowValue;
		}
		set
		{
			mutualFollowValue = value;
		}
	}

	public int messagePageMax
	{
		get;
		private set;
	}

	public List<FriendMessageData> messageDetailList
	{
		get;
		private set;
	}

	public int followNum
	{
		get;
		private set;
	}

	public int followerNum
	{
		get;
		private set;
	}

	public int noReadMessageNum
	{
		get;
		private set;
	}

	public void SetFollowToHomeCharaInfo(int userId, bool follow)
	{
		FriendCharaInfo friendCharaInfo = homeCharas.chara.Find((FriendCharaInfo c) => c.userId == userId);
		if (friendCharaInfo != null)
		{
			friendCharaInfo.following = follow;
		}
	}

	public void SetFollowerToHomeCharaInfo(int userId, bool follower)
	{
		FriendCharaInfo friendCharaInfo = homeCharas.chara.Find((FriendCharaInfo c) => c.userId == userId);
		if (friendCharaInfo != null)
		{
			friendCharaInfo.follower = follower;
		}
	}

	private void AddMessageDetailList(List<FriendMessageData> addMessageList)
	{
		addMessageList.ForEach(delegate(FriendMessageData message)
		{
			FriendManager friendManager = this;
			FriendMessageData friendMessageData = messageDetailList.Find((FriendMessageData m) => m.id == message.id);
			if (friendMessageData == null)
			{
				messageDetailList.Add(message);
			}
		});
		messageDetailList.Sort((FriendMessageData l, FriendMessageData r) => l.lid.CompareTo(r.lid));
	}

	public void SetFollowNum(int num)
	{
		followNum = num;
	}

	public void SetFollowerNum(int num)
	{
		followerNum = num;
	}

	public void SetNoReadMessageNum(int num)
	{
		if (0 > num)
		{
			num = 0;
		}
		noReadMessageNum = num;
	}

	protected override void Awake()
	{
		base.Awake();
		homeCharas = new HomeCharaInfoList();
		messageDetailList = new List<FriendMessageData>();
		noReadMessageNum = 0;
	}

	public void SendHomeCharaList(Action<bool> callback)
	{
		Protocol.Send(HomeCharaListModel.URL, delegate(HomeCharaListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				homeCharas = ret.result;
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendGetChara(int[] userIds, Action<bool, List<FriendCharaInfo>> callback)
	{
		HomeGetCharaModel.RequestSendForm requestSendForm = new HomeGetCharaModel.RequestSendForm();
		int i = 0;
		for (int num = userIds.Length; i < num; i++)
		{
			requestSendForm.ids.Add(userIds[i]);
		}
		Protocol.Send(HomeGetCharaModel.URL, requestSendForm, delegate(HomeGetCharaModel ret)
		{
			bool arg = ErrorCodeChecker.IsSuccess(ret.Error);
			callback(arg, ret.result);
		}, string.Empty);
	}

	public void SendGetFollowList(int page, Action<bool, FriendFollowListModel.Param> callback)
	{
		FriendFollowListModel.RequestSendForm requestSendForm = new FriendFollowListModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(FriendFollowListModel.URL, requestSendForm, delegate(FriendFollowListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvFollowList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvFollowList);
		}, string.Empty);
	}

	public void SendGetFollowerList(int page, Action<bool, FriendFollowListModel.Param> callback)
	{
		FriendFollowerListModel.RequestSendForm requestSendForm = new FriendFollowerListModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(FriendFollowerListModel.URL, requestSendForm, delegate(FriendFollowerListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvFollowerList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvFollowerList);
		}, string.Empty);
	}

	public void SendGetFollowLink(Action<bool> callback)
	{
		Protocol.Send(FriendFollowLinkModel.URL, delegate(FriendFollowLinkModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			if (ret.Error == Error.None)
			{
				followLinkResult = ret.result;
				obj = true;
			}
			callback(obj);
		}, string.Empty);
	}

	public void SendFollowUser(List<int> id_list, Action<Error, List<int>> callback)
	{
		FriendFollowModel.RequestSendForm requestSendForm = new FriendFollowModel.RequestSendForm();
		requestSendForm.ids = id_list;
		Protocol.Send(FriendFollowModel.URL, requestSendForm, delegate(FriendFollowModel ret)
		{
			List<int> arg = new List<int>();
			if (ErrorCodeChecker.IsSuccess(ret.Error))
			{
				arg = ret.result.success;
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultFollowInfo(ret.result);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
			}
			callback(ret.Error, arg);
		}, string.Empty);
		MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("Friend_request", "Social");
	}

	public void SendMutualFollow(string targetCode, Action<bool> callback)
	{
		FriendMutualFollowModel.RequestSendForm requestSendForm = new FriendMutualFollowModel.RequestSendForm();
		requestSendForm.targetCode = targetCode;
		Protocol.Send(FriendMutualFollowModel.URL, requestSendForm, delegate(FriendMutualFollowModel ret)
		{
			bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
			if (ret.Error == Error.None)
			{
				mutualFollowResult = ret.result;
				obj = true;
			}
			callback(obj);
		}, string.Empty);
	}

	public void SendUnfollowUser(int user_id, Action<bool> callback)
	{
		FriendUnfollowModel.RequestSendForm requestSendForm = new FriendUnfollowModel.RequestSendForm();
		requestSendForm.followUserId = user_id;
		Protocol.Send(FriendUnfollowModel.URL, requestSendForm, delegate(FriendUnfollowModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				flag = (ret.result.success == 1);
				if (MonoBehaviourSingleton<QuestManager>.IsValid())
				{
					MonoBehaviourSingleton<QuestManager>.I.resultUserCollection.SetResultUnfollowInfo(ret.result, user_id);
				}
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendDeleteFollower(int user_id, Action<bool> callback)
	{
		FriendDeleteFollowerModel.RequestSendForm requestSendForm = new FriendDeleteFollowerModel.RequestSendForm();
		requestSendForm.followerUserId = user_id;
		Protocol.Send(FriendDeleteFollowerModel.URL, requestSendForm, delegate(FriendDeleteFollowerModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				flag = (ret.result.success == 1);
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_PARAM);
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendGetMessageUserList(int page, Action<bool, FriendMessageUserListModel.Param> callback)
	{
		FriendMessageUserListModel.RequestSendForm requestSendForm = new FriendMessageUserListModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(FriendMessageUserListModel.URL, requestSendForm, delegate(FriendMessageUserListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvMessageUserList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvMessageUserList);
		}, string.Empty);
	}

	public void SendGetMessageDetailList(int user_id, int page, Action<bool> callback)
	{
		FriendMessageDetailListModel.RequestSendForm requestSendForm = new FriendMessageDetailListModel.RequestSendForm();
		requestSendForm.userId = user_id;
		requestSendForm.page = page;
		if (talkUser == null || talkUser.userId != user_id)
		{
			talkUser = null;
			messageDetailList.Clear();
		}
		Protocol.Send(FriendMessageDetailListModel.URL, requestSendForm, delegate(FriendMessageDetailListModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				messagePageMax = ret.result.pageNumMax;
				AddMessageDetailList(ret.result.message);
				FriendMessageUserListModel.MessageUserInfo messageUserInfo = recvMessageUserList.messageUser.Find((FriendMessageUserListModel.MessageUserInfo user) => user.userId == user_id);
				if (messageUserInfo != null)
				{
					talkUser = messageUserInfo;
				}
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendFriendMessage(int user_id, string message, Action<bool> callback)
	{
		FriendSendMessageModel.RequestSendForm requestSendForm = new FriendSendMessageModel.RequestSendForm();
		requestSendForm.toUserId = user_id;
		requestSendForm.message = message;
		Protocol.Send(FriendSendMessageModel.URL, requestSendForm, delegate(FriendSendMessageModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				flag = (ret.result.success == 1);
			}
			callback(flag);
		}, string.Empty);
	}

	public void SendGetNoreadMessage(Action<bool> callback)
	{
		if (talkUser == null)
		{
			callback(false);
		}
		else
		{
			FriendGetNoReadMessageModel.RequestSendForm requestSendForm = new FriendGetNoReadMessageModel.RequestSendForm();
			requestSendForm.userId = talkUser.userId;
			Protocol.Send(FriendGetNoReadMessageModel.URL, requestSendForm, delegate(FriendGetNoReadMessageModel ret)
			{
				bool obj = ErrorCodeChecker.IsSuccess(ret.Error);
				callback(obj);
			}, string.Empty);
		}
	}

	public void SendSearchName(string name, int page, Action<bool, FriendSearchResult> callback)
	{
		FriendSearchByNameModel.RequestSendForm requestSendForm = new FriendSearchByNameModel.RequestSendForm();
		requestSendForm.name = name;
		requestSendForm.page = page;
		Protocol.Send(FriendSearchByNameModel.URL, requestSendForm, delegate(FriendSearchByNameModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvSearchList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvSearchList);
		}, string.Empty);
	}

	public void SendSearchLevel(int page, Action<bool, FriendSearchResult> callback)
	{
		FriendSearchByLevelModel.RequestSendForm requestSendForm = new FriendSearchByLevelModel.RequestSendForm();
		requestSendForm.page = page;
		Protocol.Send(FriendSearchByLevelModel.URL, requestSendForm, delegate(FriendSearchByLevelModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvSearchList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvSearchList);
		}, string.Empty);
	}

	public void SendSearchID(string code, Action<bool, FriendSearchResult> callback)
	{
		FriendSearchByCodeModel.RequestSendForm requestSendForm = new FriendSearchByCodeModel.RequestSendForm();
		requestSendForm.code = code;
		Protocol.Send(FriendSearchByCodeModel.URL, requestSendForm, delegate(FriendSearchByCodeModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				recvSearchList = ret.result;
				MonoBehaviourSingleton<GameSceneManager>.I.SetNotify(GameSection.NOTIFY_FLAG.UPDATE_FRIEND_LIST);
			}
			callback(flag, recvSearchList);
		}, string.Empty);
	}

	public void SendGetArenaRanking(int group, int isContaionSelf, Action<bool, List<ArenaRankingData>> callback)
	{
		ArenaRankingModel.RequestSendForm requestSendForm = new ArenaRankingModel.RequestSendForm();
		requestSendForm.groupId = group;
		requestSendForm.isContainSelf = isContaionSelf;
		List<ArenaRankingData> rankingDataList = null;
		Protocol.Send(ArenaRankingModel.URL, requestSendForm, delegate(ArenaRankingModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				rankingDataList = ret.result;
			}
			callback(flag, rankingDataList);
		}, string.Empty);
	}

	public void SendGetLastRanking(int group, int isContaionSelf, Action<bool, ArenaLastRankingModel.Param> callback)
	{
		ArenaLastRankingModel.RequestSendForm requestSendForm = new ArenaLastRankingModel.RequestSendForm();
		requestSendForm.groupId = group;
		requestSendForm.isContainSelf = isContaionSelf;
		ArenaLastRankingModel.Param result = null;
		Protocol.Send(ArenaLastRankingModel.URL, requestSendForm, delegate(ArenaLastRankingModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				result = ret.result;
			}
			callback(flag, result);
		}, string.Empty);
	}

	public void SendGetFriendRanking(int group, int isContaionSelf, Action<bool, List<ArenaRankingData>> callback)
	{
		ArenaFriendRankingModel.RequestSendForm requestSendForm = new ArenaFriendRankingModel.RequestSendForm();
		requestSendForm.groupId = group;
		requestSendForm.isContainSelf = isContaionSelf;
		List<ArenaRankingData> rankingDataList = null;
		Protocol.Send(ArenaFriendRankingModel.URL, requestSendForm, delegate(ArenaFriendRankingModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				rankingDataList = ret.result;
			}
			callback(flag, rankingDataList);
		}, string.Empty);
	}

	public void SendGetLegendRanking(Action<bool, List<ArenaLegendRankingModel.Param>> callback)
	{
		List<ArenaLegendRankingModel.Param> result = new List<ArenaLegendRankingModel.Param>();
		Protocol.Send(ArenaLegendRankingModel.URL, null, delegate(ArenaLegendRankingModel ret)
		{
			bool flag = ErrorCodeChecker.IsSuccess(ret.Error);
			if (flag)
			{
				result = ret.result;
			}
			callback(flag, result);
		}, string.Empty);
	}

	public void Dirty()
	{
	}

	public void OnDiff(BaseModelDiff.DiffFriend diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.follow))
		{
			followNum = diff.follow[0];
			flag = true;
		}
		if (Utility.IsExist(diff.follower))
		{
			followerNum = diff.follower[0];
			flag = true;
		}
		if (flag)
		{
			Dirty();
		}
	}

	public void DirtyMessage()
	{
	}

	public void OnDiff(BaseModelDiff.DiffMessage diff)
	{
		bool flag = false;
		if (Utility.IsExist(diff.add))
		{
			AddMessageDetailList(diff.add);
			flag = true;
		}
		if (flag)
		{
			DirtyMessage();
		}
	}

	public void ResetUser()
	{
		talkUser = null;
		if (messageDetailList != null)
		{
			messageDetailList.Clear();
		}
	}
}
