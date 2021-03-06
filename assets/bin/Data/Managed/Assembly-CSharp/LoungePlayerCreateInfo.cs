using UnityEngine;

public class LoungePlayerCreateInfo
{
	public Vector3 lookPoint;

	public SphereCollider createArea;

	public LoungePlayerCreateInfo()
		: this()
	{
	}

	public Vector3 GetCreatePosition()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (createArea == null)
		{
			return Vector3.get_zero();
		}
		Transform val = createArea.get_transform();
		Quaternion val2 = Quaternion.Euler(0f, (float)Random.Range(0, 360), 0f);
		float num = Random.Range(0f, createArea.get_radius());
		return val.get_position() + val2 * Vector3.get_forward() * num;
	}
}
