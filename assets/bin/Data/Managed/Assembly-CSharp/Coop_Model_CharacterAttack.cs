using UnityEngine;

public class Coop_Model_CharacterAttack : Coop_Model_ObjectSyncPositionBase
{
	public int attack_id;

	public Vector3 act_pos = Vector3.get_zero();

	public bool act_pos_f;

	public bool sync_immediately;

	public int syncRandomSeed;

	public Coop_Model_CharacterAttack()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		base.packetType = PACKET_TYPE.CHARACTER_ATTACK;
	}

	public override bool IsHandleable(StageObject owner)
	{
		Character character = owner as Character;
		if (!sync_immediately && !character.IsChangeableAction(Character.ACTION_ID.ATTACK))
		{
			return false;
		}
		return base.IsHandleable(owner);
	}
}
