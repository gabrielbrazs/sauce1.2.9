public class SmithEvolvePerformance : SmithPerformanceBase
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected override void OnOpen()
	{
		director.Reset();
		director.StartEvolve(OnEndDirection);
		base.OnOpen();
	}

	public override void UpdateUI()
	{
		base.UpdateUI();
	}

	protected override void OnClose()
	{
		base.OnClose();
	}
}
