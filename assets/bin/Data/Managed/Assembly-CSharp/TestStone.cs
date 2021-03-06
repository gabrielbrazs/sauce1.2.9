using UnityEngine;

public class TestStone : BreakObject
{
	protected override void Awake()
	{
		base.Awake();
		if (string.IsNullOrEmpty(breakEffectName))
		{
			breakEffectName = "ef_btl_bg_rockbreak_01";
		}
	}

	protected override void Initialize()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		Renderer componentInChildren = this.get_gameObject().GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			SphereCollider val = componentInChildren.get_gameObject().AddComponent<SphereCollider>();
			val.set_radius(2.2f);
		}
	}
}
