package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.drive.DriveId;

public final class zzblu extends zza {
    public static final Creator<zzblu> CREATOR = new zzblv();
    DriveId zzgfw;

    public zzblu(DriveId driveId) {
        this.zzgfw = driveId;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 2, this.zzgfw, i, false);
        zzd.zzai(parcel, zze);
    }
}
