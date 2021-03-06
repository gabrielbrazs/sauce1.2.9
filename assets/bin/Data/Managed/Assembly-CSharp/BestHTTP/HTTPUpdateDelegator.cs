using BestHTTP.Caching;
using UnityEngine;

namespace BestHTTP
{
	internal sealed class HTTPUpdateDelegator
	{
		private static HTTPUpdateDelegator instance;

		public HTTPUpdateDelegator()
			: this()
		{
		}

		public static void CheckInstance()
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Expected O, but got Unknown
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Expected O, but got Unknown
			if (!Object.op_Implicit(instance))
			{
				instance = (Object.FindObjectOfType(typeof(HTTPUpdateDelegator)) as HTTPUpdateDelegator);
				if (!Object.op_Implicit(instance))
				{
					GameObject val = new GameObject("HTTP Update Delegator");
					val.set_hideFlags(3);
					Object.DontDestroyOnLoad(val);
					instance = val.AddComponent<HTTPUpdateDelegator>();
				}
			}
		}

		private void Awake()
		{
			HTTPCacheService.SetupCacheFolder();
		}

		private void LateUpdate()
		{
			HTTPManager.OnUpdate();
		}

		private void OnApplicationQuit()
		{
			HTTPManager.OnQuit();
		}
	}
}
