public class RegionMapDescriptionDetailDelivery : InGameDeliveryDetailBase
{
	protected new enum UI
	{
		OBJ_BASE_ROOT,
		OBJ_BACK,
		OBJ_COMPLETE_ROOT,
		BTN_COMPLETE,
		CHARA_ALL,
		OBJ_UNLOCK_PORTAL_ROOT,
		LBL_UNLOCK_PORTAL,
		LBL_QUEST_TITLE,
		LBL_CHARA_MESSAGE,
		LBL_PERSON_NAME,
		TEX_NPC,
		BTN_JUMP_QUEST,
		BTN_JUMP_INVALID,
		BTN_JUMP_MAP,
		BTN_JUMP_GACHATOP,
		GRD_REWARD,
		LBL_MONEY,
		LBL_EXP,
		SPR_WINDOW,
		SPR_MESSAGE_BG,
		OBJ_NEED_ITEM_ROOT,
		LBL_NEED_ITEM_NAME,
		LBL_NEED,
		LBL_HAVE,
		LBL_PLACE_NAME,
		LBL_ENEMY_NAME,
		OBJ_DIFFICULTY_ROOT,
		OBJ_ENEMY_NAME_ROOT,
		LBL_GET_PLACE,
		BTN_SUBMISSION,
		STR_BTN_SUBMISSION,
		STR_BTN_SUBMISSION_BACK,
		OBJ_TOP_CROWN_ROOT,
		OBJ_TOP_CROWN_1,
		OBJ_TOP_CROWN_2,
		OBJ_TOP_CROWN_3,
		STR_MISSION_EMPTY,
		SPR_CROWN_1,
		SPR_CROWN_2,
		SPR_CROWN_3,
		OBJ_SUBMISSION_ROOT,
		OBJ_MISSION_INFO,
		OBJ_MISSION_INFO_1,
		OBJ_MISSION_INFO_2,
		OBJ_MISSION_INFO_3,
		LBL_MISSION_INFO_1,
		LBL_MISSION_INFO_2,
		LBL_MISSION_INFO_3,
		SPR_MISSION_INFO_CROWN_1,
		SPR_MISSION_INFO_CROWN_2,
		SPR_MISSION_INFO_CROWN_3,
		STR_MISSION,
		OBJ_BASE_FRAME,
		OBJ_TARGET_FRAME,
		OBJ_SUBMISSION_FRAME,
		OBJ_NORMAL_ROOT,
		OBJ_EVENT_ROOT,
		LBL_POINT_NORMAL,
		TEX_NORMAL_ICON,
		LBL_POINT_EVENT,
		TEX_EVENT_ICON,
		BTN_CREATE,
		BTN_JOIN,
		BTN_MATCHING,
		PORTRAIT_WINDOW,
		PORTRAIT_BACK,
		LANDSCAPE_WINDOW,
		LANDSCAPE_BACK
	}

	private RegionMap.SpotEventData mapData;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		mapData = (array[3] as RegionMap.SpotEventData);
		base.Initialize();
	}

	public void OnQuery_JUMP_QUEST()
	{
		GameSection.StopEvent();
		EventData[] autoEvents = new EventData[2]
		{
			new EventData("TO_REGION_MAP", null),
			new EventData("TO_FIELD_OR_HOME", mapData)
		};
		MonoBehaviourSingleton<GameSceneManager>.I.SetAutoEvents(autoEvents);
	}

	protected override void UpdateUIJumpButton(JumpButtonType type)
	{
		if (base.isComplete)
		{
			SetActive(baseRoot, UI.BTN_COMPLETE, true);
			SetActive(baseRoot, UI.BTN_JUMP_QUEST, false);
		}
		else
		{
			bool is_visible = MonoBehaviourSingleton<FieldManager>.I.currentMapID != mapData.mapId;
			SetActive(baseRoot, UI.BTN_JUMP_QUEST, is_visible);
		}
	}
}
