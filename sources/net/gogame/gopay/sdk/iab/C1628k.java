package net.gogame.gopay.sdk.iab;

import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import net.gogame.gopay.sdk.support.C1652q;

/* renamed from: net.gogame.gopay.sdk.iab.k */
final class C1628k implements C1652q {

    /* renamed from: a */
    C1623bv f1287a = this.f1288b;

    /* renamed from: b */
    final /* synthetic */ C1623bv f1288b;

    /* renamed from: c */
    final /* synthetic */ int f1289c;

    /* renamed from: d */
    final /* synthetic */ C1398i f1290d;

    C1628k(C1398i iVar, C1623bv bvVar, int i) {
        this.f1290d = iVar;
        this.f1288b = bvVar;
        this.f1289c = i;
    }

    /* renamed from: a */
    public final void mo22645a(Bitmap bitmap) {
        if (this.f1287a.f1274c != this.f1289c) {
            return;
        }
        if (bitmap != null) {
            int width = bitmap.getWidth();
            int height = bitmap.getHeight();
            BitmapDrawable bitmapDrawable = new BitmapDrawable(this.f1290d.f993a.getResources(), bitmap);
            bitmapDrawable.setBounds(0, 0, width, height);
            this.f1287a.f1272a.setImageDrawable(bitmapDrawable);
            this.f1287a.f1272a.setMinimumWidth(this.f1290d.mo21497a(width));
            this.f1287a.f1272a.setMinimumHeight(this.f1290d.mo21497a(height));
            this.f1287a.f1273b.setVisibility(4);
            this.f1287a.f1272a.setVisibility(0);
            return;
        }
        this.f1287a.f1272a.setVisibility(4);
        this.f1287a.f1273b.setVisibility(0);
    }
}