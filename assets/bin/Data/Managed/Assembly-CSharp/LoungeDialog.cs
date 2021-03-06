using Network;

public class LoungeDialog : GameSection
{
	public void OnQuery_INVITED_LOUNGE()
	{
		string inviteValue = MonoBehaviourSingleton<LoungeMatchingManager>.I.InviteValue;
		if (!string.IsNullOrEmpty(inviteValue))
		{
			string[] array = inviteValue.Split('_');
			GameSection.StayEvent();
			MonoBehaviourSingleton<LoungeMatchingManager>.I.SendApply(array[0], delegate(bool is_success, Error ret_code)
			{
				GameSection.ResumeEvent(is_success, null);
			});
		}
	}

	public void OnQuery_FRIEND_INVITED_LOUNGE()
	{
		string id = (string)GameSection.GetEventData();
		GameSection.StayEvent();
		MonoBehaviourSingleton<LoungeMatchingManager>.I.SendEntry(id, delegate(bool isSuccess)
		{
			GameSection.ResumeEvent(isSuccess, null);
		});
	}

	private void OnQuery_HOW_TO()
	{
		GameSection.SetEventData(WebViewManager.lounge);
	}
}
