package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.drive.DrivePreferencesApi.FileUploadPreferencesResult;

abstract class zzbkb extends zzbiu<FileUploadPreferencesResult> {
    private /* synthetic */ zzbjw zzgic;

    public zzbkb(zzbjw zzbjw, GoogleApiClient googleApiClient) {
        this.zzgic = zzbjw;
        super(googleApiClient);
    }

    protected final /* synthetic */ Result zzb(Status status) {
        return new zzbka(this.zzgic, status, null, null);
    }
}
