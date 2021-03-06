using UnityEngine;

public class Coop_Model_CharacterMoveVelocity : Coop_Model_ObjectBase
{
	public float time;

	public Vector3 pos = Vector3.get_zero();

	public int motion_id;

	public int target_id;

	public Coop_Model_CharacterMoveVelocity()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_MOVE_VELOCITY;
	}

	public override Vector3 GetObjectPosition()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return pos;
	}

	public override bool IsHaveObjectPosition()
	{
		return true;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		bool flag = false;
		if (character.actionID == Character.ACTION_ID.MOVE && character.moveType == Character.MOVE_TYPE.SYNC_VELOCITY)
		{
			flag = true;
		}
		if (!character.IsChangeableAction(Character.ACTION_ID.MOVE) && !flag)
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
