package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzdu extends zzdv {
    private /* synthetic */ int zzhin;

    zzdu(zzdr zzdr, GoogleApiClient googleApiClient, int i) {
        this.zzhin = i;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzb((zzn) this, this.zzhin);
    }
}
