package com.google.firebase.iid;

import android.content.Context;
import android.content.Intent;
import android.os.Build.VERSION;
import android.support.v4.content.WakefulBroadcastReceiver;
import android.util.Base64;
import android.util.Log;
import com.google.firebase.messaging.MessageForwardingService;

public final class FirebaseInstanceIdReceiver extends WakefulBroadcastReceiver {
    public final void onReceive(Context context, Intent intent) {
        String str = null;
        int i = -1;
        intent.setComponent(null);
        intent.setPackage(context.getPackageName());
        if (VERSION.SDK_INT <= 18) {
            intent.removeCategory(context.getPackageName());
        }
        String stringExtra = intent.getStringExtra("gcm.rawData64");
        if (stringExtra != null) {
            intent.putExtra("rawData", Base64.decode(stringExtra, 0));
            intent.removeExtra("gcm.rawData64");
        }
        stringExtra = intent.getStringExtra("from");
        if ("google.com/iid".equals(stringExtra) || "gcm.googleapis.com/refresh".equals(stringExtra)) {
            str = "com.google.firebase.INSTANCE_ID_EVENT";
        } else if (MessageForwardingService.ACTION_REMOTE_INTENT.equals(intent.getAction())) {
            str = "com.google.firebase.MESSAGING_EVENT";
        } else {
            Log.d("FirebaseInstanceId", "Unexpected intent");
        }
        if (str != null) {
            if (FirebaseInstanceIdInternalReceiver.zzek(context)) {
                if (isOrderedBroadcast()) {
                    setResultCode(-1);
                }
                FirebaseInstanceIdInternalReceiver.zzah(context, str).zza(intent, goAsync());
            } else {
                i = zzq.zzbyp().zza(context, str, intent);
            }
        }
        if (isOrderedBroadcast()) {
            setResultCode(i);
        }
    }
}
