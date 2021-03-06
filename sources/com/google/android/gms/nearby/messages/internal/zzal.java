package com.google.android.gms.nearby.messages.internal;

import android.content.Context;
import android.os.Looper;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks;
import com.google.android.gms.common.api.GoogleApiClient.OnConnectionFailedListener;
import com.google.android.gms.common.internal.zzq;
import com.google.android.gms.nearby.messages.MessagesOptions;
import com.google.android.gms.nearby.messages.Strategy;

final class zzal extends zza<zzah, MessagesOptions> {
    zzal() {
    }

    public final int getPriority() {
        return Strategy.TTL_SECONDS_INFINITE;
    }

    public final /* synthetic */ zze zza(Context context, Looper looper, zzq zzq, Object obj, ConnectionCallbacks connectionCallbacks, OnConnectionFailedListener onConnectionFailedListener) {
        return new zzah(context, looper, connectionCallbacks, onConnectionFailedListener, zzq, (MessagesOptions) obj);
    }
}
