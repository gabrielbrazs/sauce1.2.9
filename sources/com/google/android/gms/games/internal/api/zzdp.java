package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.Games.zza;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.UpdateMatchResult;

abstract class zzdp extends zza<UpdateMatchResult> {
    private zzdp(GoogleApiClient googleApiClient) {
        super(googleApiClient);
    }

    public final /* synthetic */ Result zzb(Status status) {
        return new zzdq(this, status);
    }
}
