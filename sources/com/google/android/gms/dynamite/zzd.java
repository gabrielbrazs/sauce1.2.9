package com.google.android.gms.dynamite;

import android.content.Context;
import com.google.android.gms.dynamite.DynamiteModule.zzc;

final class zzd implements com.google.android.gms.dynamite.DynamiteModule.zzd {
    zzd() {
    }

    public final zzj zza(Context context, String str, zzi zzi) throws zzc {
        zzj zzj = new zzj();
        zzj.zzgpp = zzi.zzae(context, str);
        zzj.zzgpq = zzi.zzb(context, str, true);
        if (zzj.zzgpp == 0 && zzj.zzgpq == 0) {
            zzj.zzgpr = 0;
        } else if (zzj.zzgpp >= zzj.zzgpq) {
            zzj.zzgpr = -1;
        } else {
            zzj.zzgpr = 1;
        }
        return zzj;
    }
}
