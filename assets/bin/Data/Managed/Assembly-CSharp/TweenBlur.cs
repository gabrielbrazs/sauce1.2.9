using UnityEngine;

public class TweenBlur
{
	[Range(0f, 5f)]
	public float from = 1f;

	[Range(0f, 5f)]
	public float to = 1f;

	[SerializeField]
	private float duration = 1f;

	[SerializeField]
	private float delay;

	private float timer;

	[SerializeField]
	private Material sourceMaterial;

	public float _lod;

	public float lod
	{
		get
		{
			foreach (UIDrawCall active in UIDrawCall.activeList)
			{
				if (sourceMaterial == active.baseMaterial)
				{
					return active.dynamicMaterial.GetFloat("_Lod");
				}
			}
			return 1f;
		}
		set
		{
			foreach (UIDrawCall active in UIDrawCall.activeList)
			{
				if (sourceMaterial == active.baseMaterial)
				{
					sourceMaterial.SetFloat("_Lod", value);
					active.dynamicMaterial.SetFloat("_Lod", value);
					_lod = value;
				}
			}
		}
	}

	public TweenBlur()
		: this()
	{
	}

	private void LateUpdate()
	{
		timer += Time.get_deltaTime();
		if (timer < delay)
		{
			lod = from;
		}
		else if (timer < duration + duration)
		{
			lod = Mathf.Lerp(from, to, (timer - duration) / duration);
		}
		else
		{
			lod = to;
			this.set_enabled(false);
		}
	}
}
