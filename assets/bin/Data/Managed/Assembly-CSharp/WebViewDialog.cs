using System.Collections.Generic;

public class WebViewDialog : GameSection
{
	public override string overrideBackKeyEvent
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			if (this.get_gameObject().get_name() == "WebViewDialog")
			{
				return "[BACK]";
			}
			return "CLOSE";
		}
	}

	public override void Initialize()
	{
		string text = GameSceneEvent.current.userData as string;
		if (text == null)
		{
			List<GameSceneTables.TextData> currentSectionTextList = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionTextList();
			if (currentSectionTextList.Count > 0)
			{
				text = currentSectionTextList[0].text;
			}
		}
		if (MonoBehaviourSingleton<WebViewManager>.IsValid())
		{
			MonoBehaviourSingleton<WebViewManager>.I.Open(NetworkManager.APP_HOST + text, delegate
			{
				DispatchEvent("CLOSE", null);
			});
		}
		base.Initialize();
	}
}
