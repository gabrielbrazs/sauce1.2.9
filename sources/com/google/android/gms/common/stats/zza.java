package com.google.android.gms.common.stats;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.util.Log;
import com.google.android.gms.common.util.zzc;
import java.util.Collections;
import java.util.List;

public final class zza {
    private static final Object zzfun = new Object();
    private static volatile zza zzfxd;
    private static boolean zzfxe = false;
    private final List<String> zzfxf = Collections.EMPTY_LIST;
    private final List<String> zzfxg = Collections.EMPTY_LIST;
    private final List<String> zzfxh = Collections.EMPTY_LIST;
    private final List<String> zzfxi = Collections.EMPTY_LIST;

    private zza() {
    }

    public static zza zzaky() {
        if (zzfxd == null) {
            synchronized (zzfun) {
                if (zzfxd == null) {
                    zzfxd = new zza();
                }
            }
        }
        return zzfxd;
    }

    public final boolean zza(Context context, Intent intent, ServiceConnection serviceConnection, int i) {
        return zza(context, context.getClass().getName(), intent, serviceConnection, i);
    }

    public final boolean zza(Context context, String str, Intent intent, ServiceConnection serviceConnection, int i) {
        ComponentName component = intent.getComponent();
        if (!(component == null ? false : zzc.zzac(context, component.getPackageName()))) {
            return context.bindService(intent, serviceConnection, i);
        }
        Log.w("ConnectionTracker", "Attempted to bind to a service in a STOPPED package.");
        return false;
    }
}
