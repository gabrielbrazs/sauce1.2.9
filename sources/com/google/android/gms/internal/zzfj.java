package com.google.android.gms.internal;

import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;

public final class zzfj extends zzee implements zzfh {
    zzfj(IBinder iBinder) {
        super(iBinder, AdvertisingInterface.ADVERTISING_ID_SERVICE_INTERFACE_TOKEN);
    }

    public final String getId() throws RemoteException {
        Parcel zza = zza(1, zzax());
        String readString = zza.readString();
        zza.recycle();
        return readString;
    }

    public final boolean zzb(boolean z) throws RemoteException {
        Parcel zzax = zzax();
        zzeg.zza(zzax, true);
        zzax = zza(2, zzax);
        boolean zza = zzeg.zza(zzax);
        zzax.recycle();
        return zza;
    }
}
