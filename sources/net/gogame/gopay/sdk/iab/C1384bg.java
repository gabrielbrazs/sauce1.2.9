package net.gogame.gopay.sdk.iab;

import android.app.Dialog;
import android.view.View;
import android.view.View.OnClickListener;

/* renamed from: net.gogame.gopay.sdk.iab.bg */
final class C1384bg implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ Dialog f1078a;

    /* renamed from: b */
    final /* synthetic */ PurchaseActivity f1079b;

    C1384bg(PurchaseActivity purchaseActivity, Dialog dialog) {
        this.f1079b = purchaseActivity;
        this.f1078a = dialog;
    }

    public final void onClick(View view) {
        this.f1078a.dismiss();
        this.f1079b.f1052z.setEnabled(true);
    }
}
