using System;
using UnityEngine;

public class WebViewManager : MonoBehaviourSingleton<WebViewManager>
{
	private const string COOKIE_KEY_VERSION = "apv";

	private WebViewObject webViewObject;

	private Action<string> onClose;

	[SerializeField]
	private Rect m_Margine;

	public static string Url_News => NetworkManager.APP_HOST + News;

	public static string Url_Help => NetworkManager.APP_HOST + Help;

	public static string News => "news";

	public static string Help => "help";

	public static string Terms => "tos/terms";

	public static string Present => "tos/present";

	public static string Currency => "tos/currency";

	public static string Commercial => "tos/commercial";

	public static string Found => "tos/found";

	public static string GachaQuestList => "questitem/list";

	public static string PointShop => "pointshop/top";

	public static string GachaTicket => "tos/gachaticket";

	public static string lounge => "lounge/top";

	public static string BingoRule => "bingo/rule";

	public static string BingoReward => "bingo/reward";

	public static string Explore => "explore/help";

	public static string GuildRequest => "guild-request/top";

	public static string eula => "tos/eula";

	public static string NewsWithLinkParamFormat => "news/show?link={0}";

	public static string GuildHint => "goclan";

	public static string CreateNewsWithLinkParamUrl(string link_param)
	{
		return string.Format(NewsWithLinkParamFormat, link_param);
	}

	public void Open(string url, Action<string> _onClose = null)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		onClose = _onClose;
		webViewObject = this.get_gameObject().AddComponent<WebViewObject>();
		webViewObject.Init(string.Empty, string.Empty, string.Empty);
		webViewObject.EvaluateJS("var appVersion='" + NetworkNative.getNativeVersionName() + "';");
		webViewObject.SetCookie(NetworkManager.APP_HOST, "apv", NetworkNative.getNativeVersionName());
		if (MonoBehaviourSingleton<AccountManager>.I.account.token != string.Empty)
		{
			string[] array = MonoBehaviourSingleton<AccountManager>.I.account.token.Split('=');
			webViewObject.SetCookie(NetworkManager.APP_HOST, array[0], array[1]);
		}
		webViewObject.LoadURL(url);
		webViewObject.SetVisibility(true);
		int num = Screen.get_width();
		int num2 = Screen.get_height();
		if (MonoBehaviourSingleton<AppMain>.IsValid())
		{
			num = MonoBehaviourSingleton<AppMain>.I.defaultScreenWidth;
			num2 = MonoBehaviourSingleton<AppMain>.I.defaultScreenHeight;
		}
		int left = (int)((float)num * m_Margine.get_xMin());
		int top = (int)((float)num2 * m_Margine.get_yMin());
		int right = (int)((float)num * m_Margine.get_width());
		int bottom = (int)((float)num2 * m_Margine.get_height());
		webViewObject.SetMargins(left, top, right, bottom);
	}

	public void OnWebViewEvent(string msg)
	{
		if (!string.IsNullOrEmpty(msg))
		{
			if (msg.StartsWith("mailto:"))
			{
				Debug.Log((object)("[mailto]:" + msg));
				Application.OpenURL(msg);
			}
			if (msg.StartsWith("browser:"))
			{
				string text = msg.Replace("browser:", string.Empty);
				Debug.Log((object)("[browser]:" + text));
				Application.OpenURL(text);
			}
			if (msg.StartsWith("checkPurchase:"))
			{
				bool flag = (int.Parse(msg.Replace("checkPurchase:", string.Empty)) == 1) ? true : false;
				Debug.Log((object)("[checkPurchase]:" + flag));
				Native.RestorePurchasedItem(flag);
			}
			if (msg.StartsWith("openOpinionBox:"))
			{
				Debug.Log((object)"[openOpinionBox]:");
				Close(msg);
				MonoBehaviourSingleton<GameSceneManager>.I.OpinionBox();
			}
			if (msg.StartsWith("close:"))
			{
				Debug.Log((object)"[close]:");
				Close(msg);
			}
			if (msg.StartsWith("movie:"))
			{
				string text2 = msg.Replace("movie:", string.Empty);
				Debug.Log((object)("[movie]:" + text2));
				switch (text2)
				{
				case "tutorial":
					webViewObject.onDestroy = delegate
					{
						Utility.PlayFullScreenMovie("tutorial_move.mp4");
					};
					break;
				case "skill":
					webViewObject.onDestroy = delegate
					{
						Utility.PlayFullScreenMovie("tutorial_skill.mp4");
					};
					break;
				}
				Close(msg);
			}
			if (msg.StartsWith("goto:"))
			{
				Close(msg);
				ProcessGotoEvent(msg);
			}
		}
	}

	public static void ProcessGotoEvent(string msg)
	{
		string text = msg.Replace("goto:", string.Empty);
		Debug.Log((object)("[goto]:" + text));
		string name = "MAIN_MENU_HOME";
		if (LoungeMatchingManager.IsValidInLounge())
		{
			name = "MAIN_MENU_LOUNGE";
		}
		EventData[] array = null;
		switch (text)
		{
		case "gacha":
			array = new EventData[1]
			{
				new EventData("MAIN_MENU_SHOP", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "magi_gacha":
			array = new EventData[2]
			{
				new EventData("MAIN_MENU_SHOP", null),
				new EventData("MAGI_GACHA", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "inn":
			array = new EventData[1]
			{
				new EventData("MAIN_MENU_STUDIO", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "quest":
			array = new EventData[2]
			{
				new EventData(name, null),
				new EventData("QUEST_COUNTER", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "event_quest":
			array = new EventData[3]
			{
				new EventData(name, null),
				new EventData("QUEST_COUNTER", null),
				new EventData("TO_EVENT_LIST", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "gacha_quest":
			array = new EventData[3]
			{
				new EventData(name, null),
				new EventData("GACHA_QUEST_COUNTER", null),
				new EventData("TO_GACHA_QUEST_COUNTER", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "explore_quest":
			array = new EventData[2]
			{
				new EventData(name, null),
				new EventData("EXPLORE", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "point_shop":
			array = new EventData[2]
			{
				new EventData(name, null),
				new EventData("POINT_SHOP", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "bingo":
			array = new EventData[2]
			{
				new EventData(name, null),
				new EventData("BINGO", true)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		case "arena":
		{
			EventData eventData = ((int)MonoBehaviourSingleton<UserInfoManager>.I.userStatus.level >= 50) ? new EventData("SELECT_ARENA", null) : new EventData("SELECT_DISABLE_ARENA", null);
			array = new EventData[3]
			{
				new EventData(name, null),
				new EventData("TO_EVENT", null),
				eventData
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		}
		case "promotion":
			array = new EventData[2]
			{
				new EventData(name, null),
				new EventData("FRIEND_PROMOTION", null)
			};
			MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			break;
		default:
			if (text.StartsWith("login_bonus:"))
			{
				string s = text.Replace("login_bonus:", string.Empty);
				int.TryParse(s, out int result);
				if (result != 0)
				{
					array = new EventData[2]
					{
						new EventData(name, null),
						new EventData("LIMITED_LOGIN_BONUS_VIEW", result)
					};
					MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
				}
			}
			else if (text.StartsWith("gacha_equip:"))
			{
				string text2 = text.Replace("gacha_equip:", string.Empty);
				int[] array2 = new int[3]
				{
					-1,
					-1,
					-1
				};
				string[] array3 = text2.Split(':');
				int i = 0;
				for (int num = array3.Length; i < num; i++)
				{
					int.TryParse(array3[i], out array2[i]);
				}
				array = new EventData[2]
				{
					new EventData("MAIN_MENU_SHOP", null),
					new EventData("GACHA_EQUIP_LIST_FROM_NEWS", new object[3]
					{
						array2[0],
						array2[1],
						array2[2]
					})
				};
				MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(array);
			}
			break;
		}
	}

	public void Close(string result)
	{
		Object.Destroy(webViewObject);
		webViewObject = null;
		try
		{
			if (onClose != null)
			{
				onClose(result);
			}
		}
		catch (Exception ex)
		{
			Debug.LogError((object)ex);
		}
		finally
		{
			onClose = null;
		}
	}
}
