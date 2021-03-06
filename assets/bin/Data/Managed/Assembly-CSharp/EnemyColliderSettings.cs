using UnityEngine;

public class EnemyColliderSettings
{
	[Tooltip("対象collider")]
	public Collider targetCollider;

	[Tooltip("攻撃ヒット無視フラグ")]
	public bool ignoreHitAttack = true;

	public EnemyColliderSettings()
		: this()
	{
	}
}
