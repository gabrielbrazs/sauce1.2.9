using Network;
using System;
using System.Collections.Generic;

public class HomeInfoModel : BaseModel
{
	public class SendForm
	{
		public string appStr;
	}

	[Serializable]
	public class Param
	{
		public string productId;

		public int notice;

		public int message;

		public bool loginBonus;

		public int mysteryTime;

		public string nextDonationTime;

		public string askUpdate;

		public bool party;

		public bool isPointShopOpen;

		public bool isLoungeOpen;

		public int pointShopBanner;

		public bool isOneTimesOfferActive;

		public ClanAdvisaryData advisory;

		public List<string> alertMessages = new List<string>();

		public List<EventBanner> banner = new List<EventBanner>();

		public List<GachaDeco> gachaDeco = new List<GachaDeco>();

		public ChatChannelInfo chat;

		public List<Network.EventData> events = new List<Network.EventData>();

		public List<Network.EventData> bingoEvents = new List<Network.EventData>();

		public int task;

		public int newsId = 1;

		public float dailyRemainTime = -1f;

		public float weeklyRemainTime = -1f;

		public int isDisplayReview = -1;

		public int isShadowChallengeFirst;

		public bool isArenaOpen;

		public bool isJoinedArenaRanking;

		public List<TimeSlotEvent> timeSlotEvents = new List<TimeSlotEvent>();

		public int crystalChangeName;

		public bool isGuildRequestOpen;

		public string blackShopEndDate;
	}

	public static string URL = "ajax/home/info";

	public Param result = new Param();
}
