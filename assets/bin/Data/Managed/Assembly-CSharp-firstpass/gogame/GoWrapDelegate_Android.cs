namespace gogame
{
	public class GoWrapDelegate_Android
	{
		private IGoWrapDelegate goWrapDelegate;

		public GoWrapDelegate_Android(IGoWrapDelegate goWrapDelegate)
			: this("net.gogame.gowrap.sdk.GoWrapDelegateV2")
		{
			this.goWrapDelegate = goWrapDelegate;
		}

		public void didCompleteRewardedAd(string rewardId, int rewardQuantity)
		{
			if (goWrapDelegate != null)
			{
				goWrapDelegate.didCompleteRewardedAd(rewardId, rewardQuantity);
			}
		}

		public void onMenuOpened()
		{
			if (goWrapDelegate != null)
			{
				goWrapDelegate.onMenuOpened();
			}
		}

		public void onMenuClosed()
		{
			if (goWrapDelegate != null)
			{
				goWrapDelegate.onMenuClosed();
			}
		}

		public void onCustomUrl(string url)
		{
			if (goWrapDelegate != null)
			{
				goWrapDelegate.onCustomUrl(url);
			}
		}

		public void onOffersAvailable()
		{
			if (goWrapDelegate != null)
			{
				goWrapDelegate.onOffersAvailable();
			}
		}
	}
}
