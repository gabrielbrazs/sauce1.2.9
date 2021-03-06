using System;
using UnityEngine;

public class GoGameTimeManager : MonoBehaviourSingleton<GoGameTimeManager>
{
	[Flags]
	public enum STOP_FLAG
	{
		DEBUG_MANAGER = 0x1,
		DEBUG_FUNC = 0x2
	}

	private DateTime? currentTime;

	private float elapsedTime;

	public STOP_FLAG stopFlags
	{
		get;
		private set;
	}

	protected override void Awake()
	{
		base.Awake();
		Time.set_timeScale(1f);
	}

	public void SetStop(STOP_FLAG flag, bool is_stop)
	{
		if (is_stop)
		{
			stopFlags |= flag;
		}
		else
		{
			stopFlags &= ~flag;
		}
		Time.set_timeScale((!IsStop()) ? 1f : 0f);
	}

	public bool IsStop()
	{
		return stopFlags != (STOP_FLAG)0;
	}

	public static DateTime GetNow()
	{
		if (!MonoBehaviourSingleton<GoGameTimeManager>.IsValid() || !MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime.HasValue)
		{
			return DateTime.Now;
		}
		return MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime.Value.AddSeconds((double)MonoBehaviourSingleton<GoGameTimeManager>.I.elapsedTime);
	}

	public static void SetServerTime(string time)
	{
		if (DateTime.TryParse(time, out DateTime result))
		{
			MonoBehaviourSingleton<GoGameTimeManager>.I.currentTime = result;
			MonoBehaviourSingleton<GoGameTimeManager>.I.elapsedTime = 0f;
		}
	}

	public static string GetRemainTimeToText(TimeSpan span, int digitNum = 3)
	{
		string text = string.Empty;
		if (span.Seconds > 0)
		{
			span = span.Add(TimeSpan.FromMinutes(1.0));
		}
		int num = 0;
		if (span.Days > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0u), span.Days);
			num++;
		}
		if (span.Hours > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1u), span.Hours);
			num++;
		}
		if (span.Minutes > 0 && num < digitNum)
		{
			text += string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), span.Minutes);
			num++;
		}
		if (text == string.Empty)
		{
			return string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2u), 0);
		}
		return text;
	}

	public static string GetRemainTimeToText(string targetDateTime, int digitNum = 3)
	{
		return GetRemainTimeToText(GetRemainTime(targetDateTime), digitNum);
	}

	public static TimeSpan GetRemainTime(string targetDateTime)
	{
		if (DateTime.TryParse(targetDateTime, out DateTime result))
		{
			return GetRemainTime(result);
		}
		return TimeSpan.FromDays(99.0);
	}

	private static TimeSpan GetRemainTime(DateTime targetDateTime)
	{
		return targetDateTime - GetNow();
	}

	private void Update()
	{
		elapsedTime += Time.get_unscaledDeltaTime();
	}

	public static DateTime CombineDateAndTime(DateTime date, DateTime time)
	{
		return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
	}
}
