package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.support.annotation.NonNull;
import com.google.android.gms.auth.api.phone.SmsRetrieverClient;
import com.google.android.gms.tasks.Task;

public final class zzati extends SmsRetrieverClient {
    public zzati(@NonNull Activity activity) {
        super(activity);
    }

    public zzati(@NonNull Context context) {
        super(context);
    }

    public final Task<Void> startSmsRetriever() {
        return zzb(new zzatj(this));
    }
}
