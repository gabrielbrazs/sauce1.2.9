public class UIScrollablePopupListItem
{
	private UIScrollablePopupList popupRoot;

	public UIScrollablePopupListItem()
		: this()
	{
	}

	private void Awake()
	{
		popupRoot = this.GetComponentInParent<UIScrollablePopupList>();
	}

	private void OnPress(bool isDown)
	{
		popupRoot.ClosePopup();
	}
}
