package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzdt extends zzdz {
    zzdt(zzdr zzdr, GoogleApiClient googleApiClient) {
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzi(this);
    }
}
