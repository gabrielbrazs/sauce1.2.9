using System.Collections;
using UnityEngine;

public class Ookushi_AssetBundle
{
	public Ookushi_AssetBundle()
		: this()
	{
	}

	private IEnumerator Start()
	{
		while (!AppMain.isInitialized)
		{
			yield return (object)null;
		}
		LoadingQueue load_queue = new LoadingQueue(this);
		LoadObject lo = load_queue.Load(RESOURCE_CATEGORY.UI, "QuestRequestItem", false);
		yield return (object)load_queue.Wait();
		ResourceUtility.Instantiate<Object>(lo.loadedObject);
	}
}
