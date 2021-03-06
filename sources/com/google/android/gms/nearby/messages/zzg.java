package com.google.android.gms.nearby.messages;

import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.google.android.gms.common.internal.safeparcel.zzb;

public final class zzg implements Creator<Strategy> {
    public final /* synthetic */ Object createFromParcel(Parcel parcel) {
        boolean z = false;
        int zzd = zzb.zzd(parcel);
        int i = 0;
        int i2 = 0;
        int i3 = 0;
        int i4 = 0;
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        while (parcel.dataPosition() < zzd) {
            int readInt = parcel.readInt();
            switch (65535 & readInt) {
                case 1:
                    i2 = zzb.zzg(parcel, readInt);
                    break;
                case 2:
                    i3 = zzb.zzg(parcel, readInt);
                    break;
                case 3:
                    i4 = zzb.zzg(parcel, readInt);
                    break;
                case 4:
                    z = zzb.zzc(parcel, readInt);
                    break;
                case 5:
                    i5 = zzb.zzg(parcel, readInt);
                    break;
                case 6:
                    i6 = zzb.zzg(parcel, readInt);
                    break;
                case 7:
                    i7 = zzb.zzg(parcel, readInt);
                    break;
                case 1000:
                    i = zzb.zzg(parcel, readInt);
                    break;
                default:
                    zzb.zzb(parcel, readInt);
                    break;
            }
        }
        zzb.zzaf(parcel, zzd);
        return new Strategy(i, i2, i3, i4, z, i5, i6, i7);
    }

    public final /* synthetic */ Object[] newArray(int i) {
        return new Strategy[i];
    }
}
