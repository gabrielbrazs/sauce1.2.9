package com.google.android.gms.common.internal;

import android.os.IInterface;
import android.os.RemoteException;

public interface zzap extends IInterface {
    void cancel() throws RemoteException;
}
