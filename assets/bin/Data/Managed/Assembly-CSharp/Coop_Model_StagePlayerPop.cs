using Network;

public class Coop_Model_StagePlayerPop : Coop_Model_Base
{
	public int sid;

	public bool isSelf;

	public CharaInfo charaInfo;

	public StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo;

	public StageObjectManager.PlayerTransferInfo transferInfo;

	public Coop_Model_StagePlayerPop()
	{
		base.packetType = PACKET_TYPE.STAGE_PLAYER_POP;
	}

	public override string ToString()
	{
		string empty = string.Empty;
		empty = empty + ",sid=" + sid;
		empty = empty + ",isSelf=" + isSelf;
		empty = empty + ",charaInfo=" + charaInfo;
		empty = empty + ",extentionInfo=" + extentionInfo;
		empty = empty + ",transferInfo=" + transferInfo;
		return base.ToString() + empty;
	}
}
