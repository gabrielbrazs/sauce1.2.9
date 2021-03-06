package com.google.android.gms.common.api.internal;

import android.support.annotation.WorkerThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api.zze;
import com.google.android.gms.common.internal.zzj;
import java.util.Map;

final class zzau extends zzbb {
    final /* synthetic */ zzar zzflr;
    private final Map<zze, zzat> zzflt;

    public zzau(zzar zzar, Map<zze, zzat> map) {
        this.zzflr = zzar;
        super(zzar);
        this.zzflt = map;
    }

    @WorkerThread
    public final void zzagy() {
        int i;
        int i2 = 1;
        int i3 = 0;
        int i4 = 1;
        int i5 = 0;
        for (zze zze : this.zzflt.keySet()) {
            if (!zze.zzafe()) {
                i4 = i5;
                i = 0;
            } else if (!((zzat) this.zzflt.get(zze)).zzfjm) {
                i = 1;
                break;
            } else {
                i = i4;
                i4 = 1;
            }
            i5 = i4;
            i4 = i;
        }
        i2 = i5;
        i = 0;
        if (i2 != 0) {
            i3 = this.zzflr.zzfki.isGooglePlayServicesAvailable(this.zzflr.mContext);
        }
        if (i3 == 0 || (r0 == 0 && i4 == 0)) {
            if (this.zzflr.zzfll) {
                this.zzflr.zzflj.connect();
            }
            for (zze zze2 : this.zzflt.keySet()) {
                zzj zzj = (zzj) this.zzflt.get(zze2);
                if (!zze2.zzafe() || i3 == 0) {
                    zze2.zza(zzj);
                } else {
                    this.zzflr.zzflb.zza(new zzaw(this, this.zzflr, zzj));
                }
            }
            return;
        }
        this.zzflr.zzflb.zza(new zzav(this, this.zzflr, new ConnectionResult(i3, null)));
    }
}
