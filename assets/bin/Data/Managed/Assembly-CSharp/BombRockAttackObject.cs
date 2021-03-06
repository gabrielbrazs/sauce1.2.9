using UnityEngine;

public class BombRockAttackObject : AttackColliderObject
{
	protected override void OnTriggerEnter(Collider collider)
	{
		base.OnTriggerEnter(collider);
		Destroy();
	}

	public override bool CheckHitAttack(AttackHitInfo info, Collider to_collider, StageObject to_object)
	{
		if (info.attackType == AttackHitInfo.ATTACK_TYPE.BOMBROCK && to_object is Player && !(to_object is Self))
		{
			return false;
		}
		return base.CheckHitAttack(info, to_collider, to_object);
	}
}
