package com.google.android.gms.games.internal.api;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.games.snapshot.SnapshotMetadata;
import com.google.android.gms.games.snapshot.Snapshots.CommitSnapshotResult;

final class zzci implements CommitSnapshotResult {
    private /* synthetic */ Status zzeik;

    zzci(zzch zzch, Status status) {
        this.zzeik = status;
    }

    public final SnapshotMetadata getSnapshotMetadata() {
        return null;
    }

    public final Status getStatus() {
        return this.zzeik;
    }
}
