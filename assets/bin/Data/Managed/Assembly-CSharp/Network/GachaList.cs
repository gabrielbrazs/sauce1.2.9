using System;
using System.Collections.Generic;
using UnityEngine;

namespace Network
{
	[Serializable]
	public class GachaList
	{
		public class GachaType
		{
			public int type;

			public List<GachaGroup> groups = new List<GachaGroup>();

			public string url;

			public GACHA_TYPE Type => (GACHA_TYPE)type;

			public GACHA_TYPE ViewType
			{
				get
				{
					GACHA_TYPE gACHA_TYPE = (GACHA_TYPE)type;
					if (gACHA_TYPE == GACHA_TYPE.TUTORIAL1)
					{
						gACHA_TYPE = GACHA_TYPE.QUEST;
					}
					if (gACHA_TYPE == GACHA_TYPE.TUTORIAL2)
					{
						gACHA_TYPE = GACHA_TYPE.SKILL;
					}
					return gACHA_TYPE;
				}
			}
		}

		public class GachaGroup
		{
			public int group;

			public int priority;

			public string note;

			public string bannerImg;

			public string url;

			public List<Gacha> gachas = new List<Gacha>();

			public List<GachaGuaranteeCampaignInfo> gachaGuaranteeCampaignInfo = new List<GachaGuaranteeCampaignInfo>();

			public List<GachaFriendPromotionInfo> friendPromotionInfo = new List<GachaFriendPromotionInfo>();

			public List<GachaLineup> pickupLineups = new List<GachaLineup>();

			public int counter = -1;

			public string expireAt;
		}

		public class Gacha
		{
			public int gachaId;

			public int subGroup;

			public string productId;

			public int priority;

			public string name;

			public int requiredItemId;

			public int needItemNum;

			public int crystalNum;

			public int num;

			public int yen;

			public int yenIncludeTax;

			public string buttonImg;

			public string eventTitleImg;

			public int remainCount = -1;

			public string endDate;

			public bool IsEnd
			{
				get
				{
					if (string.IsNullOrEmpty(endDate))
					{
						return false;
					}
					if (!DateTime.TryParse(endDate, out DateTime result))
					{
						return true;
					}
					return result < TimeManager.GetNow();
				}
			}

			public bool IsOncePurchase()
			{
				return subGroup == 0;
			}

			public void SetCrystalNum(int num)
			{
				crystalNum = num;
			}
		}

		public class GachaLineup
		{
			public int rewardType;

			public int itemId;

			public int orderNo;

			public GachaPickupAnim anim;

			public List<QuestItem.SellItem> sellItems;
		}

		public class GachaPickupAnim
		{
			public class TextStyle
			{
				public string text;

				public int size;

				public int italic;

				public string color;

				public string outColor;

				public Color toColor()
				{
					//IL_000e: Unknown result type (might be due to invalid IL or missing references)
					Color result = default(Color);
					ColorUtility.TryParseHtmlString(color, ref result);
					return result;
				}

				public Color toOutColor()
				{
					//IL_000e: Unknown result type (might be due to invalid IL or missing references)
					Color result = default(Color);
					ColorUtility.TryParseHtmlString(outColor, ref result);
					return result;
				}
			}

			public string pattern = string.Empty;

			public TextStyle name = new TextStyle();

			public TextStyle description = new TextStyle();

			public TextStyle sub = new TextStyle();

			public TextStyle adda = new TextStyle();

			public TextStyle addb = new TextStyle();
		}

		public List<GachaType> types = new List<GachaType>();

		public string note = string.Empty;
	}
}
