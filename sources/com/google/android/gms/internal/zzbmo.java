package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;

public final class zzbmo extends zza {
    public static final Creator<zzbmo> CREATOR = new zzbmp();
    private boolean zzggs;

    public zzbmo(boolean z) {
        this.zzggs = z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzggs);
        zzd.zzai(parcel, zze);
    }
}
