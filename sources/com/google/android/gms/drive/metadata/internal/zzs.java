package com.google.android.gms.drive.metadata.internal;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.metadata.zzb;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import org.json.JSONArray;

public final class zzs extends zzb<String> {
    public zzs(String str, int i) {
        super(str, Collections.singleton(str), Collections.emptySet(), 4300000);
    }

    protected final /* synthetic */ void zza(Bundle bundle, Object obj) {
        bundle.putStringArrayList(getName(), new ArrayList((Collection) obj));
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        return zzd(dataHolder, i, i2);
    }

    protected final Collection<String> zzd(DataHolder dataHolder, int i, int i2) {
        try {
            String zzd = dataHolder.zzd(getName(), i, i2);
            if (zzd == null) {
                return null;
            }
            Collection arrayList = new ArrayList();
            JSONArray jSONArray = new JSONArray(zzd);
            for (int i3 = 0; i3 < jSONArray.length(); i3++) {
                arrayList.add(jSONArray.getString(i3));
            }
            return Collections.unmodifiableCollection(arrayList);
        } catch (Throwable e) {
            throw new IllegalStateException("DataHolder supplied invalid JSON", e);
        }
    }

    protected final /* synthetic */ Object zzm(Bundle bundle) {
        return bundle.getStringArrayList(getName());
    }
}
