using UnityEngine;

public class RegionRoot
{
	[Tooltip("部位ID")]
	public int regionID = 1;

	[Tooltip("サブ部位ID（タ\u30fcゲット時しかヒットしない")]
	public int[] subRegionIDs;

	[Tooltip("RegionRootとして登録した後に非表示にする")]
	public bool isDeactive;

	public int[] regionIDArray
	{
		get;
		protected set;
	}

	public RegionRoot()
		: this()
	{
	}

	private void Awake()
	{
		if (subRegionIDs == null)
		{
			regionIDArray = new int[1];
			regionIDArray[0] = regionID;
		}
		else
		{
			regionIDArray = new int[subRegionIDs.Length + 1];
			regionIDArray[0] = regionID;
			int i = 0;
			for (int num = subRegionIDs.Length; i < num; i++)
			{
				regionIDArray[i + 1] = subRegionIDs[i];
			}
		}
	}
}
