using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Events;
using GooglePlayGames.Native.PInvoke;
using GooglePlayGames.OurUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GooglePlayGames.Native
{
	internal class NativeEventClient : IEventsClient
	{
		private readonly EventManager mEventManager;

		internal NativeEventClient(EventManager manager)
		{
			mEventManager = Misc.CheckNotNull(manager);
		}

		public void FetchAllEvents(DataSource source, Action<ResponseStatus, List<IEvent>> callback)
		{
			Misc.CheckNotNull<Action<ResponseStatus, List<IEvent>>>(callback);
			callback = CallbackUtils.ToOnGameThread<ResponseStatus, List<IEvent>>(callback);
			mEventManager.FetchAll(ConversionUtils.AsDataSource(source), delegate(EventManager.FetchAllResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, new List<IEvent>());
				}
				else
				{
					callback.Invoke(responseStatus, response.Data().Cast<IEvent>().ToList());
				}
			});
		}

		public void FetchEvent(DataSource source, string eventId, Action<ResponseStatus, IEvent> callback)
		{
			Misc.CheckNotNull(eventId);
			Misc.CheckNotNull<Action<ResponseStatus, IEvent>>(callback);
			mEventManager.Fetch(ConversionUtils.AsDataSource(source), eventId, delegate(EventManager.FetchResponse response)
			{
				ResponseStatus responseStatus = ConversionUtils.ConvertResponseStatus(response.ResponseStatus());
				if (!response.RequestSucceeded())
				{
					callback.Invoke(responseStatus, (IEvent)null);
				}
				else
				{
					callback.Invoke(responseStatus, (IEvent)response.Data());
				}
			});
		}

		public void IncrementEvent(string eventId, uint stepsToIncrement)
		{
			Misc.CheckNotNull(eventId);
			mEventManager.Increment(eventId, stepsToIncrement);
		}
	}
}
