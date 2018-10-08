package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;

public final class zzarq extends zza {
    public static final Creator<zzarq> CREATOR = new zzarr();
    private String accountType;
    private int zzdxt;

    zzarq(int i, String str) {
        this.zzdxt = 1;
        this.accountType = (String) zzbp.zzu(str);
    }

    public zzarq(String str) {
        this(1, str);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zzc(parcel, 1, this.zzdxt);
        zzd.zza(parcel, 2, this.accountType, false);
        zzd.zzai(parcel, zze);
    }
}
