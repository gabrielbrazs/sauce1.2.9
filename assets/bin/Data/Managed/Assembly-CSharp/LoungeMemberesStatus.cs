using System.Collections.Generic;
using System.Linq;

public class LoungeMemberesStatus
{
	private List<LoungeMemberStatus> memberes = new List<LoungeMemberStatus>();

	public LoungeMemberStatus this[int userId]
	{
		get
		{
			return GetMemberData(userId);
		}
	}

	public LoungeMemberesStatus(LoungeModel.Lounge lounge)
	{
		memberes = (from x in lounge.slotInfos
		select new LoungeMemberStatus(x)).ToList();
	}

	public LoungeMemberesStatus(List<Party_Model_RegisterACK.UserInfo> data)
	{
		Set(data);
	}

	public LoungeMemberStatus GetMemberData(int userId)
	{
		LoungeMemberStatus loungeMemberStatus = memberes.FirstOrDefault((LoungeMemberStatus x) => x.userId == userId);
		if (object.ReferenceEquals(null, loungeMemberStatus))
		{
			loungeMemberStatus = new LoungeMemberStatus(userId);
			memberes.Add(loungeMemberStatus);
		}
		return loungeMemberStatus;
	}

	public void Add(LoungeMemberStatus member)
	{
		LoungeMemberStatus memberData = GetMemberData(member.userId);
		if (object.ReferenceEquals(null, memberData))
		{
			memberes.Add(memberData);
		}
		else
		{
			memberData.SetCopy(member);
		}
	}

	public void Remove(int userId)
	{
		LoungeMemberStatus memberData = GetMemberData(userId);
		if (!object.ReferenceEquals(null, memberData))
		{
			memberes.Remove(memberData);
		}
	}

	public void Set(List<Party_Model_RegisterACK.UserInfo> data)
	{
		if (!object.ReferenceEquals(null, data))
		{
			memberes = (from x in data
			select new LoungeMemberStatus(x)).ToList();
		}
	}

	public List<LoungeMemberStatus> GetAll()
	{
		return memberes;
	}

	public void SyncLoungeMember(LoungeModel.Lounge lounge)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		list = (from x in lounge.slotInfos
		where x.userInfo != null && !memberes.Any((LoungeMemberStatus m) => m.userId == x.userInfo.userId)
		select x.userInfo.userId).ToList();
		list2 = (from m in memberes
		where !lounge.slotInfos.Any((LoungeModel.SlotInfo x) => x.userInfo != null && x.userInfo.userId == m.userId)
		select m into x
		select x.userId).ToList();
		for (int i = 0; i < list.Count; i++)
		{
			memberes.Add(new LoungeMemberStatus(list[i]));
		}
		for (int j = 0; j < list2.Count; j++)
		{
			Remove(list2[j]);
		}
	}
}
