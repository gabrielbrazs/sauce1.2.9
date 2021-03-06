package com.google.android.gms.internal;

import android.annotation.SuppressLint;
import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Status;

@SuppressLint({"MissingRemoteException"})
final class zzbit extends zzbiv {
    zzbit(GoogleApiClient googleApiClient, Status status) {
        super(googleApiClient);
        setResult(status);
    }

    protected final /* bridge */ /* synthetic */ void zza(zzb zzb) throws RemoteException {
    }
}
