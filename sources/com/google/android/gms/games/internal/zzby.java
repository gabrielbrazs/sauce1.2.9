package com.google.android.gms.games.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.os.IBinder;
import android.view.Display;
import android.view.View;
import android.view.View.OnAttachStateChangeListener;
import android.view.ViewTreeObserver;
import android.view.ViewTreeObserver.OnGlobalLayoutListener;
import com.google.android.gms.common.util.PlatformVersion;
import java.lang.ref.WeakReference;

public final class zzby implements OnAttachStateChangeListener, OnGlobalLayoutListener {
    private boolean zzgt = false;
    private zze zzjv;
    private zzca zzjw;
    private WeakReference<View> zzjx;

    private zzby(zze zze, int i) {
        this.zzjv = zze;
        this.zzjw = new zzca(i);
    }

    public static zzby zza(zze zze, int i) {
        return new zzby(zze, i);
    }

    @TargetApi(17)
    private final void zzc(View view) {
        int i = -1;
        if (PlatformVersion.isAtLeastJellyBeanMR1()) {
            Display display = view.getDisplay();
            if (display != null) {
                i = display.getDisplayId();
            }
        }
        IBinder windowToken = view.getWindowToken();
        int[] iArr = new int[2];
        view.getLocationInWindow(iArr);
        int width = view.getWidth();
        int height = view.getHeight();
        this.zzjw.zzjy = i;
        this.zzjw.zzju = windowToken;
        this.zzjw.left = iArr[0];
        this.zzjw.top = iArr[1];
        this.zzjw.right = iArr[0] + width;
        this.zzjw.bottom = iArr[1] + height;
        if (this.zzgt) {
            zzcr();
        }
    }

    public final void onGlobalLayout() {
        if (this.zzjx != null) {
            View view = (View) this.zzjx.get();
            if (view != null) {
                zzc(view);
            }
        }
    }

    public final void onViewAttachedToWindow(View view) {
        zzc(view);
    }

    public final void onViewDetachedFromWindow(View view) {
        this.zzjv.zzci();
        view.removeOnAttachStateChangeListener(this);
    }

    public final void setGravity(int i) {
        this.zzjw.gravity = i;
    }

    @TargetApi(16)
    public final void zzb(View view) {
        this.zzjv.zzci();
        if (this.zzjx != null) {
            View view2 = (View) this.zzjx.get();
            Context context = this.zzjv.getContext();
            if (view2 == null && (context instanceof Activity)) {
                view2 = ((Activity) context).getWindow().getDecorView();
            }
            if (view2 != null) {
                view2.removeOnAttachStateChangeListener(this);
                ViewTreeObserver viewTreeObserver = view2.getViewTreeObserver();
                if (PlatformVersion.isAtLeastJellyBean()) {
                    viewTreeObserver.removeOnGlobalLayoutListener(this);
                } else {
                    viewTreeObserver.removeGlobalOnLayoutListener(this);
                }
            }
        }
        this.zzjx = null;
        Context context2 = this.zzjv.getContext();
        if (view == null && (context2 instanceof Activity)) {
            View findViewById = ((Activity) context2).findViewById(16908290);
            if (findViewById == null) {
                findViewById = ((Activity) context2).getWindow().getDecorView();
            }
            zzbd.m433w("PopupManager", "You have not specified a View to use as content view for popups. Falling back to the Activity content view. Note that this may not work as expected in multi-screen environments");
            view = findViewById;
        }
        if (view != null) {
            zzc(view);
            this.zzjx = new WeakReference<>(view);
            view.addOnAttachStateChangeListener(this);
            view.getViewTreeObserver().addOnGlobalLayoutListener(this);
            return;
        }
        zzbd.m430e("PopupManager", "No content view usable to display popups. Popups will not be displayed in response to this client's calls. Use setViewForPopups() to set your content view.");
    }

    public final Bundle zzco() {
        return this.zzjw.zzcs();
    }

    public final IBinder zzcp() {
        return this.zzjw.zzju;
    }

    public final zzca zzcq() {
        return this.zzjw;
    }

    public final void zzcr() {
        if (this.zzjw.zzju != null) {
            this.zzjv.zza(this.zzjw.zzju, this.zzjw.zzcs());
            this.zzgt = false;
            return;
        }
        this.zzgt = true;
    }
}