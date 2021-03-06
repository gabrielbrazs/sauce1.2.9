using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MutualFollowFBInviteDialog : GameSection
{
	private enum UI
	{
		WRP_LIST,
		BTN_SELECTALL,
		BTN_INVITE,
		IPT_NAME,
		LBL_SELECTED,
		LBL_SELECTED_NUM,
		LBL_NAME,
		TEX_AVATAR,
		OBJ_SELECT
	}

	private List<FBManager.FriendData> selectedList = new List<FBManager.FriendData>();

	public override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool getFriendFinish = false;
		bool getInvitableFriendFinish = false;
		MonoBehaviourSingleton<FBManager>.I.GetInvitableFriends(delegate
		{
			((_003CDoInitialize_003Ec__IteratorE9)/*Error near IL_0034: stateMachine*/)._003CgetInvitableFriendFinish_003E__1 = true;
			MonoBehaviourSingleton<FBManager>.I.GetFriends(delegate
			{
				((_003CDoInitialize_003Ec__IteratorE9)/*Error near IL_0034: stateMachine*/)._003CgetFriendFinish_003E__0 = true;
			});
		});
		while (!getInvitableFriendFinish || !getFriendFinish)
		{
			yield return (object)null;
		}
		SetInput((Enum)UI.IPT_NAME, string.Empty, 0, (EventDelegate.Callback)delegate
		{
			string inputValue = ((_003CDoInitialize_003Ec__IteratorE9)/*Error near IL_0085: stateMachine*/)._003C_003Ef__this.GetInputValue((Enum)UI.IPT_NAME);
			((_003CDoInitialize_003Ec__IteratorE9)/*Error near IL_0085: stateMachine*/)._003C_003Ef__this.SetWrapContentFilterText((Enum)UI.WRP_LIST, inputValue);
		});
		base.Initialize();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
		_UpdateListFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo);
		_UpdateNumFriend(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo, MonoBehaviourSingleton<FBManager>.I.friendInfo);
		_UpdateSelected();
	}

	private void _UpdateListFriend(FBManager.InvitableFriendInfo invitable_friend_info)
	{
		List<FBManager.FriendData> friendList = invitable_friend_info.data;
		int j = 0;
		SetWrapContentFilter(UI.WRP_LIST, "MutualFollowFBInviteListItem", friendList.Count, false, delegate(int i, Transform t, bool b)
		{
			FBManager.FriendData friendData2 = friendList[++j];
			SetLabelText(t, UI.LBL_NAME, friendData2.name);
			SetDownloadTexture(t, UI.TEX_AVATAR, friendData2.picture.data.url);
			SetEvent(t, "SELECT", i);
		}, delegate(int i, string s)
		{
			FBManager.FriendData friendData = friendList[i];
			if (friendData.name.ContainIgnoreCase(s) || s.ContainIgnoreCase(friendData.name))
			{
				return true;
			}
			return false;
		});
	}

	private void _UpdateSelected()
	{
		SetLabelText((Enum)UI.LBL_SELECTED, string.Format(base.sectionData.GetText("FRIEND_SELECTED"), selectedList.Count));
	}

	private void _UpdateNumFriend(FBManager.InvitableFriendInfo invitable_friend_info, FBManager.FriendInfo friend_info)
	{
		List<FBManager.FriendData> data = friend_info.data;
		SetLabelText((Enum)UI.LBL_SELECTED_NUM, string.Format(base.sectionData.GetText("FRIEND_NUM"), friend_info.data.Count, invitable_friend_info.data.Count));
	}

	private void OnQuery_SELECT()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		int num = (int)GameSection.GetEventData();
		Transform ctrl = GetCtrl(UI.WRP_LIST);
		Transform root = ctrl.GetChild(num);
		SetActive(root, UI.OBJ_SELECT, true);
		FBManager.FriendData item = MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data[num];
		if (selectedList.Contains(item))
		{
			selectedList.Remove(item);
			SetActive(root, UI.OBJ_SELECT, false);
		}
		else
		{
			selectedList.Add(item);
			SetActive(root, UI.OBJ_SELECT, true);
		}
		_UpdateSelected();
	}

	private void OnQuery_SELECTALL()
	{
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Expected O, but got Unknown
		int count = selectedList.Count;
		selectedList.Clear();
		bool is_visible = false;
		if (count < MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data.Count)
		{
			selectedList.AddRange(MonoBehaviourSingleton<FBManager>.I.invitableFriendInfo.data);
			is_visible = true;
		}
		Transform ctrl = GetCtrl(UI.WRP_LIST);
		foreach (Transform item in ctrl)
		{
			Transform root = item;
			SetActive(root, UI.OBJ_SELECT, is_visible);
		}
		_UpdateSelected();
	}

	private void OnQuery_INVITE()
	{
		List<string> list = (from o in selectedList
		select o.id).ToList();
		if (list.Count > 0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("amount", list.Count);
			MonoBehaviourSingleton<GoWrapManager>.I.trackEvent("invite_friend", "Social", dictionary);
			GameSection.ChangeEvent("INVITE_SUCCESS", null);
			GameSection.StayEvent();
			MonoBehaviourSingleton<FBManager>.I.AppRequest(base.sectionData.GetText("INVITE_MESSAGE"), list, string.Empty, string.Empty, delegate(bool req_success, FBManager.AppRequestResult ret)
			{
				if (req_success)
				{
					List<string> list2 = ret.to.Split(',').ToList();
					MonoBehaviourSingleton<AccountManager>.I.SendTrackInviteFacebook(MonoBehaviourSingleton<FBManager>.I.accessToken, list2, delegate(bool success)
					{
						if (!success)
						{
							GameSection.ChangeStayEvent("TRACK_FAIL", null);
						}
						GameSection.ResumeEvent(true, null);
					});
				}
				else
				{
					GameSection.ChangeStayEvent("INVITE_FAIL", null);
					GameSection.ResumeEvent(true, null);
				}
			});
		}
		else
		{
			GameSection.ChangeEvent("INVITE_NONE", null);
		}
	}
}
