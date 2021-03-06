using Network;
using System;
using UnityEngine;

public class GuildMessageDonateListItem
{
	[SerializeField]
	private UISprite m_PinButton;

	[SerializeField]
	private UISprite m_OwnerBackground;

	[SerializeField]
	private UISprite m_TargetBackground;

	[SerializeField]
	private UISprite m_Clock;

	[SerializeField]
	private UILabel m_TimeExpire;

	[SerializeField]
	private UIButton m_ButtonGift;

	[SerializeField]
	private UIButton m_AskForHelp;

	private DonateInfo info;

	private double timeLeft;

	private double minChangeColorTime;

	private double minShowLastTime;

	private bool canPinMsg;

	private float tick = 1f;

	private float counter;

	private float delayShow = 3f;

	private float startPressTime;

	private Vector2 mousePosition;

	private bool checkLongPress;

	public GuildMessageDonateListItem()
		: this()
	{
	}

	private void OnStart()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (m_PinButton != null)
		{
			m_PinButton.get_gameObject().SetActive(false);
		}
	}

	public void SetDonateInfo(DonateInfo _info)
	{
		if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveUpdateStatus += OnReceiveUpdateStatus;
		}
		info = _info;
		timeLeft = (info.expired - MonoBehaviourSingleton<GuildManager>.I.SystemTime) / 1000.0;
		m_TimeExpire.text = SecondToTime(timeLeft);
		minChangeColorTime = 900.0;
		SetUIActive();
		int itemNum = MonoBehaviourSingleton<InventoryManager>.I.GetItemNum((ItemInfo x) => x.tableData.id == info.itemId, 1, false);
		bool flag = itemNum > 0 && info.itemNum < info.quantity;
		m_ButtonGift.SetState(UIButtonColor.State.Normal, flag);
		m_ButtonGift.state = ((!flag) ? UIButtonColor.State.Disabled : UIButtonColor.State.Normal);
		SetupUI();
	}

	private void OnReceiveUpdateStatus(ClanUpdateStatusData clanUpdateStatusData)
	{
		if (clanUpdateStatusData.type == 2 && clanUpdateStatusData.status == 2 && timeLeft <= 0.0)
		{
			SetUIDisable();
		}
	}

	private void SetUIActive()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		m_TimeExpire.color = Color.get_white();
		m_OwnerBackground.color = Color.get_white();
		m_TargetBackground.color = Color.get_white();
		m_Clock.color = Color.get_white();
		if (m_AskForHelp.get_gameObject().get_activeInHierarchy())
		{
			m_AskForHelp.SetState(UIButtonColor.State.Normal, true);
			m_AskForHelp.isEnabled = true;
		}
		if (m_ButtonGift.get_gameObject().get_activeInHierarchy())
		{
			m_ButtonGift.SetState(UIButtonColor.State.Normal, true);
			m_ButtonGift.isEnabled = true;
		}
	}

	public void SetUIDisable()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		m_TimeExpire.text = "Expired!";
		m_TimeExpire.color = Color.get_gray();
		m_OwnerBackground.color = Color.get_gray();
		m_TargetBackground.color = Color.get_gray();
		m_Clock.color = Color.get_gray();
		if (m_AskForHelp.get_gameObject().get_activeInHierarchy())
		{
			m_AskForHelp.SetState(UIButtonColor.State.Disabled, true);
			m_AskForHelp.isEnabled = false;
		}
		if (m_ButtonGift.get_gameObject().get_activeInHierarchy())
		{
			m_ButtonGift.isEnabled = false;
			m_ButtonGift.SetState(UIButtonColor.State.Disabled, true);
		}
	}

	protected void OnDestroy()
	{
		if (MonoBehaviourSingleton<ChatManager>.I.clanChat != null)
		{
			MonoBehaviourSingleton<ChatManager>.I.clanChat.onReceiveUpdateStatus -= OnReceiveUpdateStatus;
		}
	}

	private void Start()
	{
		if (MonoBehaviourSingleton<GuildManager>.I.guildData != null && MonoBehaviourSingleton<GuildManager>.I.guildData.clanMasterId == MonoBehaviourSingleton<UserInfoManager>.I.userInfo.id)
		{
			canPinMsg = true;
		}
	}

	private void Update()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		if (checkLongPress)
		{
			float num = Vector2.Distance(mousePosition, Vector2.op_Implicit(Input.get_mousePosition()));
			if (num > 10f)
			{
				checkLongPress = false;
				return;
			}
			if (Time.get_time() - startPressTime > 1f)
			{
				checkLongPress = false;
				m_PinButton.get_gameObject().SetActive(true);
				m_PinButton.get_gameObject().GetComponent<UIGameSceneEventSender>().eventData = info;
				m_PinButton.get_gameObject().SetActive(true);
			}
		}
		if (counter <= tick)
		{
			counter += Time.get_deltaTime();
		}
		else
		{
			counter = 0f;
			timeLeft -= 1.0;
			SetupUI();
		}
	}

	private void SetupUI()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft <= minChangeColorTime && timeLeft >= 0.0 && m_Clock.color != Color.get_yellow())
		{
			m_Clock.color = Color.get_yellow();
			m_TimeExpire.color = Color.get_yellow();
			m_TimeExpire.text = SecondToTime(timeLeft);
		}
		if (timeLeft < 0.0)
		{
			SetUIDisable();
		}
		else
		{
			m_TimeExpire.text = SecondToTime(timeLeft);
		}
	}

	private void OnPress(bool isDown)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		if (canPinMsg)
		{
			if (isDown)
			{
				checkLongPress = true;
				startPressTime = Time.get_time();
				mousePosition = Vector2.op_Implicit(Input.get_mousePosition());
			}
			else
			{
				checkLongPress = false;
			}
		}
	}

	public void HidePinButton()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		m_PinButton.get_gameObject().SetActive(false);
	}

	private double DateTimeToTimestampSeconds()
	{
		DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow - d).TotalSeconds;
	}

	private string SecondToTime(double time)
	{
		int num = (int)time;
		int num2 = num % 60;
		int num3 = num / 60;
		num3 %= 60;
		int num4 = num / 3600;
		return $"{num4:D2}:{num3:D2}:{num2:D2}";
	}
}
