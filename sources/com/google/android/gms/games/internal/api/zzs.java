package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzs extends zzv {
    private /* synthetic */ String zzhhc;
    private /* synthetic */ int zzhhd;

    zzs(zzp zzp, GoogleApiClient googleApiClient, String str, int i) {
        this.zzhhc = str;
        this.zzhhd = i;
        super(googleApiClient);
    }

    public final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zzp(this.zzhhc, this.zzhhd);
    }
}
