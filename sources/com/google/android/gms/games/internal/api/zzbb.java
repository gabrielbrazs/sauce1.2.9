package com.google.android.gms.games.internal.api;

import android.os.RemoteException;
import com.google.android.gms.common.api.Api.zzb;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.internal.zzn;
import com.google.android.gms.games.internal.GamesClientImpl;

final class zzbb extends zzbf {
    private /* synthetic */ int zzhhn;

    zzbb(zzax zzax, GoogleApiClient googleApiClient, int i) {
        this.zzhhn = i;
        super(googleApiClient);
    }

    protected final /* synthetic */ void zza(zzb zzb) throws RemoteException {
        ((GamesClientImpl) zzb).zza((zzn) this, this.zzhhn, true, false);
    }
}
