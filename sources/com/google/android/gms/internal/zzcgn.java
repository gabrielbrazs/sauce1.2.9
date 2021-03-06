package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzcgn extends zza {
    public static final Creator<zzcgn> CREATOR = new zzcgo();

    public final boolean equals(Object obj) {
        return this == obj || (obj instanceof zzcgn);
    }

    public final int hashCode() {
        return 0;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        zzd.zzai(parcel, zzd.zze(parcel));
    }
}
