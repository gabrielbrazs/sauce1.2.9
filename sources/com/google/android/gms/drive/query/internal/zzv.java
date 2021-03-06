package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.query.Filter;

public final class zzv extends zza {
    public static final Creator<zzv> CREATOR = new zzw();
    private FilterHolder zzgnw;

    public zzv(Filter filter) {
        this(new FilterHolder(filter));
    }

    zzv(FilterHolder filterHolder) {
        this.zzgnw = filterHolder;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzgnw, i, false);
        zzd.zzai(parcel, zze);
    }

    public final <T> T zza(zzj<T> zzj) {
        return zzj.zzv(this.zzgnw.getFilter().zza(zzj));
    }
}
