package com.google.android.gms.common.stats;

import android.content.Context;
import android.content.Intent;
import android.os.PowerManager.WakeLock;
import android.os.Process;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.common.annotation.KeepForSdk;
import java.util.List;

@KeepForSdk
public class StatsUtils {
    @KeepForSdk
    public static String getEventKey(Context context, Intent intent) {
        return String.valueOf((((long) System.identityHashCode(context)) << 32) | ((long) System.identityHashCode(intent)));
    }

    @KeepForSdk
    public static String getEventKey(WakeLock wakeLock, String str) {
        String valueOf = String.valueOf(String.valueOf((((long) Process.myPid()) << 32) | ((long) System.identityHashCode(wakeLock))));
        if (TextUtils.isEmpty(str)) {
            str = "";
        }
        String valueOf2 = String.valueOf(str);
        return valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf);
    }

    @Nullable
    static List<String> zza(@Nullable List<String> list) {
        if (list == null || list.size() != 1 || !"com.google.android.gms".equals(list.get(0))) {
            return list;
        }
        return null;
    }

    @Nullable
    static String zzi(String str) {
        if ("com.google.android.gms".equals(str)) {
            return null;
        }
        return str;
    }
}