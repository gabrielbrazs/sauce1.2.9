public class QuestRoomUserStatusDetail : EquipSetDetailStatus
{
	private QuestRoomObserver observer;

	public override void Initialize()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		object[] array = GameSection.GetEventData() as object[];
		observer = this.get_gameObject().AddComponent<QuestRoomObserver>().Initialize((bool)array[1], (bool)array[2], delegate(string dispatch_event_name)
		{
			DispatchEvent(dispatch_event_name, null);
		}, delegate(string change_event_name)
		{
			GameSection.ChangeEvent(change_event_name, null);
		}, delegate
		{
			GameSection.StayEvent();
		}, delegate(bool is_success)
		{
			GameSection.ResumeEvent(is_success, null);
		}, null);
		GameSection.SetEventData(array[0]);
		base.Initialize();
	}

	protected void OnQuery_QuestRoomInvalid_UserDetail_OK()
	{
		observer.SetupBackSectionEvent();
	}

	protected override void OnQuery_ABILITY_DATA()
	{
		base.OnQuery_ABILITY_DATA();
		object eventData = GameSection.GetEventData();
		GameSection.SetEventData(new object[3]
		{
			eventData,
			observer.fromSearchSection,
			observer.isEntryPass
		});
	}

	protected override void OnQuery_TO_ABILITY()
	{
		GameSection.SetEventData(new object[3]
		{
			currentEventData,
			false,
			false
		});
	}
}
