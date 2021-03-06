using UnityEngine;

public class MaterialInfo
{
	public UILabel lbl;

	public UIWidget widget;

	public string nowSectionName;

	private bool isEnableParentScroll;

	public MaterialInfo()
		: this()
	{
	}

	public void Initialize(string section_name)
	{
		nowSectionName = section_name;
		SetEnableInfo(false);
	}

	public void SetText(string text)
	{
		lbl.text = text;
	}

	public void SetEnableInfo(bool is_enable)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		lbl.get_gameObject().SetActive(is_enable);
		widget.get_gameObject().SetActive(is_enable);
	}

	public void Send(bool is_touch, Transform button, string item_name, Transform parent_scroll)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Expected O, but got Unknown
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		if (!(lbl == null))
		{
			SetEnableInfo(is_touch);
			if (is_touch)
			{
				SetText(item_name);
				Transform val = this.get_transform();
				val.set_parent(button.get_parent());
				val.set_localScale(Vector3.get_one());
				UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
				BoxCollider component = button.GetComponent<BoxCollider>();
				UIWidget uIWidget = widget;
				Transform val2 = this.get_transform();
				Vector3 position = val2.get_position();
				UIRoot componentInParent2 = this.get_gameObject().GetComponentInParent<UIRoot>();
				Vector3 size = component.get_size();
				int num = (int)(size.y * 0.5f) + uIWidget.height / 2;
				float num2 = (float)num;
				Vector3 localScale = componentInParent2.get_transform().get_localScale();
				float num3 = num2 * localScale.y;
				Vector3 position2 = component.get_transform().get_position();
				float num4 = num3 + position2.y;
				Vector3 position3 = val2.get_position();
				position._002Ector(position3.x, num4);
				if (componentInParent != null)
				{
					UIPanel component2 = componentInParent.GetComponent<UIPanel>();
					Vector3 val3 = position;
					float num5 = (float)(uIWidget.height / 2);
					Vector3 localScale2 = componentInParent2.get_transform().get_localScale();
					Vector3 worldPos = val3 + new Vector3(0f, num5 * localScale2.y);
					if (!component2.IsVisible(worldPos))
					{
						Vector3 position4 = componentInParent.get_transform().get_position();
						float y = position4.y;
						Vector2 viewSize = component2.GetViewSize();
						float num6 = (viewSize.y - (float)uIWidget.height) / 2f;
						Vector2 clipOffset = component2.clipOffset;
						float num7 = num6 + clipOffset.y;
						Vector3 localScale3 = componentInParent2.get_transform().get_localScale();
						position.y = y + num7 * localScale3.y;
					}
				}
				Vector3 position5 = component.get_transform().get_position();
				float num8 = position5.x;
				float num9 = num8;
				Vector3 localScale4 = componentInParent2.get_transform().get_localScale();
				float num10 = num9 * (1f / localScale4.x);
				float num11 = UIVirtualScreen.screenWidth * 0.5f;
				int num12 = uIWidget.width / 2;
				float num13 = num10 - (float)num12;
				float num14 = num10 + (float)num12;
				if (0f - num11 > num13)
				{
					float num15 = 0f - num11 + (float)num12;
					Vector3 localScale5 = componentInParent2.get_transform().get_localScale();
					num8 = num15 * localScale5.x;
				}
				else if (num11 < num14)
				{
					float num16 = num11 - (float)num12;
					Vector3 localScale6 = componentInParent2.get_transform().get_localScale();
					num8 = num16 * localScale6.x;
				}
				position._002Ector(num8, position.y);
				val.get_transform().set_position(position);
				isEnableParentScroll = (componentInParent != null);
				if (componentInParent != null)
				{
					val.set_parent(((object)parent_scroll) ?? ((object)componentInParent.get_transform().get_parent()));
					val.set_localScale(Vector3.get_one());
				}
				val.get_gameObject().SetActive(false);
				val.get_gameObject().SetActive(true);
			}
		}
	}

	private void adjustPosY(Transform button)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		Vector3 zero = Vector3.get_zero();
		UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
		BoxCollider component = button.GetComponent<BoxCollider>();
		UIWidget component2 = this.GetComponent<UIWidget>();
		Transform val = this.get_transform();
		UIRoot componentInParent2 = this.get_gameObject().GetComponentInParent<UIRoot>();
		Vector3 size = component.get_size();
		int num = (int)(size.y * 0.5f) + component2.height / 2;
		float num2 = (float)num;
		Vector3 localScale = componentInParent2.get_transform().get_localScale();
		float num3 = num2 * localScale.y;
		Vector3 position = component.get_transform().get_position();
		float num4 = num3 + position.y;
		Vector3 position2 = val.get_position();
		zero._002Ector(position2.x, num4);
		if (componentInParent != null)
		{
			UIPanel component3 = componentInParent.GetComponent<UIPanel>();
			Vector3 val2 = zero;
			float num5 = (float)(component2.height / 2);
			Vector3 localScale2 = componentInParent2.get_transform().get_localScale();
			Vector3 worldPos = val2 + new Vector3(0f, num5 * localScale2.y);
			if (!component3.IsVisible(worldPos))
			{
				Vector3 position3 = componentInParent.get_transform().get_position();
				float y = position3.y;
				Vector2 viewSize = component3.GetViewSize();
				float num6 = viewSize.y / 2f;
				Vector2 clipOffset = component3.clipOffset;
				float num7 = num6 + clipOffset.y;
				Vector3 localScale3 = componentInParent2.get_transform().get_localScale();
				zero.y = y + num7 * localScale3.y;
			}
		}
		val.set_position(zero);
	}

	public void UpdatePosision(Transform button)
	{
		if (isEnableParentScroll)
		{
			UIScrollView componentInParent = button.GetComponentInParent<UIScrollView>();
			if (componentInParent != null && componentInParent.isDragging)
			{
				isEnableParentScroll = false;
				SetEnableInfo(false);
			}
		}
	}
}
