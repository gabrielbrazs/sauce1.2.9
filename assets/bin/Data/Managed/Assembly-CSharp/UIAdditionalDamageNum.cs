using System.Collections;
using UnityEngine;

public class UIAdditionalDamageNum : UIDamageNum
{
	[SerializeField]
	private TweenPosition animPos;

	[SerializeField]
	private TweenScale animScale;

	[SerializeField]
	private TweenPosition animPosNormal;

	[SerializeField]
	private TweenScale animScaleNormal;

	[SerializeField]
	private TweenPosition animPosGood;

	[SerializeField]
	private TweenScale animScaleGood;

	[SerializeField]
	private TweenPosition animPosBad;

	[SerializeField]
	private TweenScale animScaleBad;

	[SerializeField]
	private GameObject damageNormal;

	[SerializeField]
	private GameObject damageGood;

	[SerializeField]
	private GameObject damageBad;

	[SerializeField]
	private UILabel damageNumNormal;

	[SerializeField]
	private UILabel damageNumGood;

	[SerializeField]
	private UILabel damageNumBad;

	public bool Initialize(Vector3 pos, int damage, DAMAGE_COLOR color, int groupOffset, UIDamageNum originalDamage, int effective)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		worldPos = pos;
		worldPos.y += offsetY;
		float num = (float)Screen.get_height() / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		float num2 = (float)Screen.get_width() / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
		higthOffset_f = (float)(damadeNum.height * groupOffset) * heightOffsetRatio * num;
		widthOffset = (float)damadeNum.width * 0.2f * (float)groupOffset * num2;
		if (null != damageNormal && null != damageGood && null != damageBad)
		{
			float num3 = 1f;
			if (effective == 0)
			{
				damageNormal.SetActive(true);
				damageGood.SetActive(false);
				damageBad.SetActive(false);
				animPos = animPosNormal;
				animScale = animScaleNormal;
				damadeNum = damageNumNormal;
			}
			else if (0 < effective)
			{
				damageNormal.SetActive(false);
				damageGood.SetActive(true);
				damageBad.SetActive(false);
				animPos = animPosGood;
				animScale = animScaleGood;
				damadeNum = damageNumGood;
				color = DAMAGE_COLOR.GOOD;
				num3 = 1.2f;
			}
			else
			{
				damageNormal.SetActive(false);
				damageGood.SetActive(false);
				damageBad.SetActive(true);
				animPos = animPosBad;
				animScale = animScaleBad;
				damadeNum = damageNumBad;
				color = DAMAGE_COLOR.BAD;
			}
			higthOffset_f *= num3;
			widthOffset *= num3;
		}
		if (groupOffset == 0 && 0 >= effective)
		{
			animScale.from = new Vector3(1f, 1f, 1f);
			animScale.to = new Vector3(1f, 1f, 1f);
		}
		if (!SetPosFromWorld(worldPos))
		{
			return false;
		}
		enable = true;
		damadeNum.text = damage.ToString();
		ChangeColor(color, damadeNum);
		if (animPos != null)
		{
			animPos.ResetToBeginning();
		}
		if (animScale != null)
		{
			animScale.ResetToBeginning();
		}
		this.StartCoroutine(DirectionNumber());
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		if (animPos != null)
		{
			animPos.PlayForward();
		}
		if (animScale != null)
		{
			animScale.PlayForward();
		}
		if (animPos != null)
		{
			while (animPos.get_enabled())
			{
				yield return (object)null;
			}
		}
		if (animScale != null)
		{
			while (animScale.get_enabled())
			{
				yield return (object)null;
			}
		}
		enable = false;
		damadeNum.alpha = 0.01f;
	}
}
