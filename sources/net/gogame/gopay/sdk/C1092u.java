package net.gogame.gopay.sdk;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.GradientDrawable.Orientation;
import android.os.Build.VERSION;
import android.support.v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.BaseAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

/* renamed from: net.gogame.gopay.sdk.u */
public final class C1092u extends BaseAdapter {
    /* renamed from: a */
    C1034h f1253a;
    /* renamed from: b */
    private final Activity f1254b;
    /* renamed from: c */
    private final C1065m f1255c;

    public C1092u(Activity activity, C1065m c1065m) {
        this.f1254b = activity;
        this.f1255c = c1065m;
    }

    public final int getCount() {
        return this.f1253a == null ? 0 : this.f1253a.f984b.size();
    }

    public final Object getItem(int i) {
        return this.f1253a.f984b.get(i);
    }

    public final long getItemId(int i) {
        return (long) i;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        SkuDetails skuDetails = (SkuDetails) getItem(i);
        Context context = viewGroup.getContext();
        View linearLayout = new LinearLayout(context);
        linearLayout.setOrientation(0);
        linearLayout.setBackgroundColor(-1);
        linearLayout.setPadding(DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f));
        View textView = new TextView(context);
        textView.setTextSize(3, 8.0f);
        textView.setTypeface(Typeface.DEFAULT_BOLD);
        textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
        textView.setText(skuDetails.getTitle());
        if (VERSION.SDK_INT >= 17) {
            textView.setTextAlignment(5);
        }
        LayoutParams layoutParams = new LinearLayout.LayoutParams(-2, -2);
        layoutParams.weight = 1.0f;
        layoutParams.setMargins(DisplayUtils.pxFromDp(context, 6.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f));
        textView.setLayoutParams(layoutParams);
        linearLayout.addView(textView);
        textView = new TextView(context);
        textView.setTextSize(3, 8.0f);
        textView.setTypeface(Typeface.DEFAULT);
        textView.setTextColor(Color.parseColor("#818181"));
        textView.setText(skuDetails.getPrice());
        if (VERSION.SDK_INT >= 17) {
            textView.setTextAlignment(6);
        }
        layoutParams = new LinearLayout.LayoutParams(-2, -2);
        layoutParams.setMargins(DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 6.0f), DisplayUtils.pxFromDp(context, 0.0f));
        textView.setLayoutParams(layoutParams);
        linearLayout.addView(textView);
        Drawable gradientDrawable = new GradientDrawable(Orientation.TOP_BOTTOM, new int[]{-1, -1});
        gradientDrawable.setStroke(DisplayUtils.pxFromDp(context, 1.5f), Color.parseColor("#87b856"));
        gradientDrawable.setCornerRadius((float) DisplayUtils.pxFromDp(context, 50.0f));
        if (VERSION.SDK_INT >= 16) {
            linearLayout.setBackground(gradientDrawable);
        }
        linearLayout.setOnClickListener(new C1093v(this, skuDetails));
        return linearLayout;
    }
}
