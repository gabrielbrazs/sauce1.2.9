using UnityEngine;

public class UICenterOnClickChild
{
	public UICenterOnClickChild()
		: this()
	{
	}

	private void OnClick()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		UICenterOnClick uICenterOnClick = NGUITools.FindInParents<UICenterOnClick>(this.get_gameObject());
		if (!(uICenterOnClick == null))
		{
			Transform val = uICenterOnClick.get_transform();
			UICenterOnChild uICenterOnChild = NGUITools.FindInParents<UICenterOnChild>(this.get_gameObject());
			UIPanel uIPanel = NGUITools.FindInParents<UIPanel>(this.get_gameObject());
			if (uICenterOnChild != null)
			{
				if (uICenterOnChild.get_enabled())
				{
					uICenterOnChild.CenterOn(val);
				}
			}
			else if (uIPanel != null && uIPanel.clipping != 0)
			{
				UIScrollView component = uIPanel.GetComponent<UIScrollView>();
				Vector3 pos = -uIPanel.cachedTransform.InverseTransformPoint(val.get_position());
				if (!component.canMoveHorizontally)
				{
					Vector3 localPosition = uIPanel.cachedTransform.get_localPosition();
					pos.x = localPosition.x;
				}
				if (!component.canMoveVertically)
				{
					Vector3 localPosition2 = uIPanel.cachedTransform.get_localPosition();
					pos.y = localPosition2.y;
				}
				SpringPanel.Begin(uIPanel.cachedGameObject, pos, 6f);
			}
		}
	}
}
