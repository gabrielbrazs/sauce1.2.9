package com.google.android.gms.auth.api;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.internal.zzasy;

final class zze extends zza<zzasy, zzf> {
    zze() {
    }

    public final /* synthetic */ com.google.android.gms.common.api.Api.zze zza(Context context, Looper looper, zzq zzq, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        return new zzasy(context, looper, zzq, (zzf) obj, connectionCallbacks, onConnectionFailedListener);
    }
}
