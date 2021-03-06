using System;
using UnityEngine;

public class SlimeAnimation
{
	private class SlimeParamAnimator<T, T2> where T : SlimeAnimBase<T2>, new()where T2 : new()
	{
		private T anim;

		public SlimeParamAnimator(float time, float start = 0f, float end = 1f)
		{
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			anim = new T();
			anim.SetBlendParam(AnimationCurve.Linear(0f, start, time, end), time);
		}

		public T2 Update()
		{
			return anim.Update();
		}

		public void SetAnimation(AnimationCurve curve, float time, bool is_blend, T2 param, Action cb)
		{
			anim.InitAnim(curve, time, is_blend, param, cb);
		}

		public bool IsPlaying()
		{
			return anim.isPlaying;
		}

		public void Terminate()
		{
			anim.Terminate();
		}

		public T GetAnimData()
		{
			return anim;
		}
	}

	private const float INTERPOLATION_TIME_POS = 0.2f;

	private const float INTERPOLATION_TIME_SCALE = 0.1f;

	private const float INTERPOLATION_TIME_COLOR = 0.5f;

	private const float FADEIN_ALPHA_MIN = 0f;

	private const float FADEIN_ALPHA_MAX = 0.5f;

	private const float FADEOUT_ALPHA_MIN = 0f;

	private const float FADEOUT_ALPHA_MAX = 0.5f;

	private const float CRUSH_ALPHA_MIN = 0f;

	private const float CRUSH_ALPHA_MAX = 0.5f;

	private const float NON_ANIM_TIME = 1f;

	private SlimeParamAnimator<SlimePosAnim, Vector3> posAnimator;

	private SlimeParamAnimator<SlimeScaleAnim, Vector3> scaleAnimator;

	private SlimeParamAnimator<SlimeColorAnim, Color> colorAnimator;

	private bool isFadeOut;

	private SlimeController slime;

	private Transform slimeTransform;

	private Material slimeMaterial;

	private readonly AnimationCurve SLIME_ANIM_CURVE_ZERO = AnimationCurve.Linear(0f, 0f, 0f, 0f);

	private readonly AnimationCurve SLIME_ANIM_CURVE_ONE = AnimationCurve.Linear(0f, 1f, 0f, 1f);

	private readonly AnimationCurve SLIME_ANIM_CURVE_HALF = AnimationCurve.Linear(0f, 0.5f, 0f, 0.5f);

	public SlimeAnimation(SlimeController slime_controller)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		slime = slime_controller;
		slimeTransform = slime.get_transform();
		slimeMaterial = slime.GetComponent<Renderer>().get_material();
		posAnimator = new SlimeParamAnimator<SlimePosAnim, Vector3>(0.2f, 0f, 1f);
		scaleAnimator = new SlimeParamAnimator<SlimeScaleAnim, Vector3>(0.1f, 0f, 1f);
		colorAnimator = new SlimeParamAnimator<SlimeColorAnim, Color>(0.5f, 0f, 1f);
	}

	public void Update()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		if (posAnimator.IsPlaying())
		{
			slimeTransform.set_localPosition(posAnimator.Update());
		}
		if (scaleAnimator.IsPlaying())
		{
			slimeTransform.set_localScale(scaleAnimator.Update());
		}
		if (colorAnimator.IsPlaying())
		{
			Color color = colorAnimator.Update();
			slimeMaterial.set_color(color);
			if (color.a <= 0.01f && isFadeOut)
			{
				slime.SetInvisible();
			}
			else
			{
				slime.SetVisible();
			}
		}
		else if (slime.IsVisible())
		{
			Color color2 = slimeMaterial.get_color();
			if (color2.a > 0f && isFadeOut)
			{
				slimeMaterial.set_color(colorAnimator.Update());
				slime.SetInvisible();
			}
		}
	}

	public bool IsPlaying()
	{
		if (!posAnimator.IsPlaying() && !scaleAnimator.IsPlaying() && !colorAnimator.IsPlaying())
		{
			return false;
		}
		return true;
	}

	public void TouchOn(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, true, slimeTransform.get_localPosition(), CallBackPos);
		scaleAnimator.SetAnimation(slime.animFadeIn, slime.fadeInAnimTime, true, slimeTransform.get_localScale(), CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0f, 1f, 0.5f), slime.fadeInColorAnimTime, false, slimeMaterial.get_color(), CallBackColor);
		isFadeOut = false;
	}

	public void TouchOff(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		posAnimator.SetAnimation(slime.animFadeOut, slime.fadeOutAnimTime, true, slimeTransform.get_localPosition(), CallBackPos);
		scaleAnimator.SetAnimation(SLIME_ANIM_CURVE_ONE, 1f, true, slimeTransform.get_localScale(), CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0.5f, 1f, 0f), slime.fadeOutColorAnimTime, true, slimeMaterial.get_color(), CallBackColor);
		isFadeOut = true;
	}

	public void Crush(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Expected O, but got Unknown
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, false, slimeTransform.get_localPosition(), CallBackPos);
		scaleAnimator.SetAnimation(slime.animCrush, slime.crushAnimTime, true, slimeTransform.get_localScale(), CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0.5f, 1f, 0f), slime.crushColorAnimTime, false, slimeMaterial.get_color(), CallBackColor);
		isFadeOut = true;
	}

	public void ScaleUp(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, true, slimeTransform.get_localPosition(), CallBackPos);
		scaleAnimator.SetAnimation(AnimationCurve.Linear(0f, 1f, slime.scaleupAnimTime, slime.scaleupAnimMaxScale), slime.scaleupAnimTime, true, slimeTransform.get_localScale(), CallBackScale);
		colorAnimator.SetAnimation(SLIME_ANIM_CURVE_HALF, 1f, false, slimeMaterial.get_color(), CallBackColor);
		isFadeOut = false;
	}

	public void ScaleUpDown(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, true, slimeTransform.get_localPosition(), CallBackPos);
		scaleAnimator.SetAnimation(slime.animScaleUpDown, slime.scaleUpDownAnimTime, true, slimeTransform.get_localScale(), CallBackScale);
		colorAnimator.SetAnimation(SLIME_ANIM_CURVE_HALF, 1f, false, slimeMaterial.get_color(), CallBackColor);
		isFadeOut = false;
	}
}
