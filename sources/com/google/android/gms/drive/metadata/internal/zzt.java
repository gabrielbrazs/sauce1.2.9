package com.google.android.gms.drive.metadata.internal;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.metadata.zza;

public class zzt extends zza<String> {
    public zzt(String str, int i) {
        super(str, i);
    }

    protected final /* synthetic */ void zza(Bundle bundle, Object obj) {
        bundle.putString(getName(), (String) obj);
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        return dataHolder.zzd(getName(), i, i2);
    }

    protected final /* synthetic */ Object zzm(Bundle bundle) {
        return bundle.getString(getName());
    }
}
