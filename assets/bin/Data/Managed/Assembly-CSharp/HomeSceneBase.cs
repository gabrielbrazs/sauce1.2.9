using System.Collections;

public class HomeSceneBase : GameSection
{
	public override void Initialize()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		UILabel.OutlineLimit = false;
		this.StartCoroutine(DoInitialize());
	}

	private IEnumerator DoInitialize()
	{
		bool wait = true;
		MonoBehaviourSingleton<OnceManager>.I.SendGetOnce(delegate
		{
			((_003CDoInitialize_003Ec__Iterator61)/*Error near IL_002d: stateMachine*/)._003Cwait_003E__0 = false;
		});
		while (wait)
		{
			yield return (object)null;
		}
		MonoBehaviourSingleton<QuestManager>.I.SetClearStatus();
		MonoBehaviourSingleton<DeliveryManager>.I.SetList();
		MonoBehaviourSingleton<WorldMapManager>.I.SetWorldMapTraveledList();
		MonoBehaviourSingleton<BlackListManager>.I.SetAllList();
		MonoBehaviourSingleton<AchievementManager>.I.SetAchievement();
		MonoBehaviourSingleton<GuildRequestManager>.I.SetList();
		MonoBehaviourSingleton<WorldMapManager>.I.SetReleasedRegion();
		MonoBehaviourSingleton<NativeGameService>.I.FixAchievement();
		base.Initialize();
	}

	public override void Exit()
	{
		MonoBehaviourSingleton<StatusManager>.I.InitStatusEquipData();
		base.Exit();
	}
}
