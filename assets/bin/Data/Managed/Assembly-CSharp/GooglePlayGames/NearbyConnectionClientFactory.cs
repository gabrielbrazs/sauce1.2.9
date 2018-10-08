using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using UnityEngine;

namespace GooglePlayGames
{
	public static class NearbyConnectionClientFactory
	{
		public static void Create(Action<INearbyConnectionClient> callback)
		{
			if (Application.get_isEditor())
			{
				Logger.d("Creating INearbyConnection in editor, using DummyClient.");
				callback(new DummyNearbyConnectionClient());
			}
			Logger.d("Creating real INearbyConnectionClient");
			NativeNearbyConnectionClientFactory.Create(callback);
		}

		private static InitializationStatus ToStatus(NearbyConnectionsStatus.InitializationStatus status)
		{
			switch (status)
			{
			case NearbyConnectionsStatus.InitializationStatus.VALID:
				return InitializationStatus.Success;
			case NearbyConnectionsStatus.InitializationStatus.ERROR_INTERNAL:
				return InitializationStatus.InternalError;
			case NearbyConnectionsStatus.InitializationStatus.ERROR_VERSION_UPDATE_REQUIRED:
				return InitializationStatus.VersionUpdateRequired;
			default:
				Logger.w("Unknown initialization status: " + status);
				return InitializationStatus.InternalError;
			}
		}
	}
}
