package com.google.android.gms.games.internal.api;

import android.os.Bundle;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.multiplayer.turnbased.LoadMatchesResponse;
import com.google.android.gms.games.multiplayer.turnbased.TurnBasedMultiplayer.LoadMatchesResult;

final class zzdo implements LoadMatchesResult {
    private /* synthetic */ Status zzeik;

    zzdo(zzdn zzdn, Status status) {
        this.zzeik = status;
    }

    public final LoadMatchesResponse getMatches() {
        return new LoadMatchesResponse(new Bundle());
    }

    public final Status getStatus() {
        return this.zzeik;
    }

    public final void release() {
    }
}
