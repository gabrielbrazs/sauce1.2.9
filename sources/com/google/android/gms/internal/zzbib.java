package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbib extends zza {
    public static final Creator<zzbib> CREATOR = new zzbic();

    public final void writeToParcel(Parcel parcel, int i) {
        zzd.zzai(parcel, zzd.zze(parcel));
    }
}
