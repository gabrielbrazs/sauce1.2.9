package com.google.android.gms.games.internal;

import android.os.Bundle;
import android.os.IBinder;

public final class zzp {
    public int bottom;
    public int gravity;
    public int left;
    public int right;
    public int top;
    public IBinder zzhgv;
    public int zzhgw;

    private zzp(int i, IBinder iBinder) {
        this.zzhgw = -1;
        this.left = 0;
        this.top = 0;
        this.right = 0;
        this.bottom = 0;
        this.gravity = i;
        this.zzhgv = iBinder;
    }

    public final Bundle zzaqz() {
        Bundle bundle = new Bundle();
        bundle.putInt("popupLocationInfo.gravity", this.gravity);
        bundle.putInt("popupLocationInfo.displayId", this.zzhgw);
        bundle.putInt("popupLocationInfo.left", this.left);
        bundle.putInt("popupLocationInfo.top", this.top);
        bundle.putInt("popupLocationInfo.right", this.right);
        bundle.putInt("popupLocationInfo.bottom", this.bottom);
        return bundle;
    }
}
