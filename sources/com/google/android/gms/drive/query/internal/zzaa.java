package com.google.android.gms.drive.query.internal;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzaa implements Creator<zzz> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        int zzd = zzb.zzd(parcel);
        while (parcel.dataPosition() < zzd) {
            zzb.zzb(parcel, parcel.readInt());
        }
        zzb.zzaf(parcel, zzd);
        return new zzz();
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new zzz[i];
    }
}
