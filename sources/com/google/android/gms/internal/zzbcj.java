package com.google.android.gms.internal;

import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.internal.zzm;

abstract class zzbcj<R extends Result> extends zzm<R, zzbcl> {
    public zzbcj(GoogleApiClient googleApiClient) {
        super(zzbcd.API, googleApiClient);
    }
}
