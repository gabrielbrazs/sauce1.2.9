package net.gogame.gopay.sdk.iab;

import android.app.Activity;
import android.app.AlertDialog.Builder;
import android.app.Dialog;
import android.content.DialogInterface.OnClickListener;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.Point;
import android.graphics.Typeface;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.LayerDrawable;
import android.os.AsyncTask;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Handler;
import android.text.TextUtils.TruncateAt;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ListAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout.LayoutParams;
import android.widget.Spinner;
import android.widget.TextView;
import com.facebook.appevents.UserDataStore;
import com.facebook.share.internal.MessengerShareContentUtility;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import net.gogame.gopay.sdk.C1359a;
import net.gogame.gopay.sdk.C1360d;
import net.gogame.gopay.sdk.C1361f;
import net.gogame.gopay.sdk.C1362g;
import net.gogame.gopay.sdk.C1406j;
import net.gogame.gopay.sdk.C1408n;
import net.gogame.gopay.sdk.Country;
import net.gogame.gopay.sdk.support.C1414c;
import net.gogame.gopay.sdk.support.C1415m;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.googleUtils.SkuDetails;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

public class PurchaseActivity extends Activity {
    /* access modifiers changed from: private */

    /* renamed from: A */
    public C1360d f1010A;
    /* access modifiers changed from: private */

    /* renamed from: B */
    public C1379b f1011B;
    /* access modifiers changed from: private */

    /* renamed from: C */
    public C1398i f1012C;
    /* access modifiers changed from: private */

    /* renamed from: D */
    public C1394bs f1013D;

    /* renamed from: E */
    private C1365a f1014E = null;
    /* access modifiers changed from: private */

    /* renamed from: F */
    public Spinner f1015F;
    /* access modifiers changed from: private */

    /* renamed from: G */
    public Spinner f1016G;
    /* access modifiers changed from: private */

    /* renamed from: H */
    public C1396g f1017H = new C1396g();
    /* access modifiers changed from: private */

    /* renamed from: I */
    public boolean f1018I = false;
    /* access modifiers changed from: private */

    /* renamed from: J */
    public boolean f1019J;
    /* access modifiers changed from: private */

    /* renamed from: K */
    public boolean f1020K = false;
    /* access modifiers changed from: private */

    /* renamed from: L */
    public boolean f1021L = false;
    /* access modifiers changed from: private */

    /* renamed from: M */
    public int f1022M = 0;
    /* access modifiers changed from: private */

    /* renamed from: N */
    public int f1023N = 3;
    /* access modifiers changed from: private */

    /* renamed from: O */
    public int f1024O;
    /* access modifiers changed from: private */

    /* renamed from: P */
    public int f1025P;
    /* access modifiers changed from: private */

    /* renamed from: Q */
    public int f1026Q;

    /* renamed from: a */
    RelativeLayout f1027a;

    /* renamed from: b */
    C1414c f1028b;
    /* access modifiers changed from: private */

    /* renamed from: c */
    public boolean f1029c = false;
    /* access modifiers changed from: private */

    /* renamed from: d */
    public boolean f1030d = false;
    /* access modifiers changed from: private */

    /* renamed from: e */
    public String f1031e;
    /* access modifiers changed from: private */

    /* renamed from: f */
    public String f1032f;
    /* access modifiers changed from: private */

    /* renamed from: g */
    public String f1033g;
    /* access modifiers changed from: private */

    /* renamed from: h */
    public String f1034h;
    /* access modifiers changed from: private */

    /* renamed from: i */
    public C1393br f1035i;
    /* access modifiers changed from: private */

    /* renamed from: j */
    public String f1036j;
    /* access modifiers changed from: private */

    /* renamed from: k */
    public String f1037k;

    /* renamed from: l */
    private String f1038l;

    /* renamed from: m */
    private String f1039m;

    /* renamed from: n */
    private String f1040n;

    /* renamed from: o */
    private String f1041o;

    /* renamed from: p */
    private Map f1042p;

    /* renamed from: q */
    private SkuDetails f1043q;
    /* access modifiers changed from: private */

    /* renamed from: r */
    public SkuDetails f1044r;
    /* access modifiers changed from: private */

    /* renamed from: s */
    public ProgressBar f1045s;
    /* access modifiers changed from: private */

    /* renamed from: t */
    public SharedPreferences f1046t;
    /* access modifiers changed from: private */

    /* renamed from: u */
    public AsyncTask f1047u;
    /* access modifiers changed from: private */

    /* renamed from: v */
    public Handler f1048v;

    /* renamed from: w */
    private Runnable f1049w;
    /* access modifiers changed from: private */

    /* renamed from: x */
    public Runnable f1050x = new C1399l(this);
    /* access modifiers changed from: private */

    /* renamed from: y */
    public WebView f1051y;
    /* access modifiers changed from: private */

    /* renamed from: z */
    public Button f1052z;

    /* renamed from: a */
    static /* synthetic */ Dialog m787a(PurchaseActivity purchaseActivity, String str, C1359a aVar, OnClickListener onClickListener) {
        Dialog dialog = new Dialog(purchaseActivity);
        dialog.setOnCancelListener(new C1383bf(purchaseActivity));
        dialog.getWindow().requestFeature(1);
        ImageButton imageButton = new ImageButton(purchaseActivity);
        imageButton.setMinimumWidth(0);
        imageButton.setMinimumHeight(0);
        imageButton.setBackgroundColor(0);
        imageButton.setPadding(0, 0, 0, 0);
        imageButton.setOnClickListener(new C1384bg(purchaseActivity, dialog));
        imageButton.setImageDrawable(purchaseActivity.m789a(C1415m.m933g()));
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.setMargins(0, 0, DisplayUtils.pxFromDp(purchaseActivity, 7.0f), 0);
        layoutParams.addRule(11);
        layoutParams.addRule(15);
        TextView textView = new TextView(purchaseActivity);
        textView.setBackgroundColor(0);
        textView.setPadding(DisplayUtils.pxFromDp(purchaseActivity, 7.0f), 0, 0, 0);
        textView.setText(str);
        textView.setTypeface(null, 1);
        textView.setTextSize(18.0f);
        textView.setTextColor(-1);
        textView.setSingleLine();
        textView.setEllipsize(TruncateAt.END);
        LayoutParams layoutParams2 = new LayoutParams(-2, -2);
        layoutParams2.addRule(15);
        layoutParams2.addRule(9);
        RelativeLayout relativeLayout = new RelativeLayout(purchaseActivity);
        GradientDrawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setCornerRadii(new float[]{(float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), (float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f), 0.0f, 0.0f, 0.0f, 0.0f});
        gradientDrawable.setColor(Color.rgb(92, 176, 59));
        if (VERSION.SDK_INT >= 16) {
            relativeLayout.setBackground(gradientDrawable);
        } else {
            relativeLayout.setBackgroundDrawable(gradientDrawable);
        }
        relativeLayout.addView(textView, layoutParams2);
        relativeLayout.addView(imageButton, layoutParams);
        ListView listView = new ListView(purchaseActivity);
        listView.setBackgroundColor(0);
        listView.setAdapter(aVar);
        listView.setSelection(purchaseActivity.f1012C.f1105e);
        listView.setOnItemClickListener(new C1385bh(purchaseActivity, onClickListener, dialog));
        LinearLayout linearLayout = new LinearLayout(purchaseActivity);
        linearLayout.setOrientation(1);
        LinearLayout.LayoutParams layoutParams3 = new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 40.0f));
        LinearLayout.LayoutParams layoutParams4 = new LinearLayout.LayoutParams(-1, -1);
        linearLayout.addView(relativeLayout, layoutParams3);
        linearLayout.addView(listView, layoutParams4);
        dialog.setContentView(linearLayout);
        GradientDrawable gradientDrawable2 = new GradientDrawable();
        gradientDrawable2.setCornerRadius((float) DisplayUtils.pxFromDp(purchaseActivity, 10.0f));
        gradientDrawable2.setColor(-1);
        if (VERSION.SDK_INT >= 16) {
            linearLayout.setBackground(gradientDrawable2);
        } else {
            linearLayout.setBackgroundDrawable(gradientDrawable2);
        }
        dialog.getWindow().setBackgroundDrawable(gradientDrawable2);
        WindowManager.LayoutParams attributes = dialog.getWindow().getAttributes();
        int i = (int) (((double) DisplayUtils.getScreenSize(purchaseActivity).y) / 1.5d);
        if (DisplayUtils.pxFromDp(purchaseActivity, 60.0f) * aVar.getCount() >= i) {
            attributes.height = i;
        }
        return dialog;
    }

    /* renamed from: a */
    private static Intent m788a(int i, C1393br brVar, String str) {
        Intent intent = new Intent();
        intent.putExtra("RESPONSE_CODE", i);
        if (brVar != null) {
            intent.putExtra("INAPP_PURCHASE_DATA", brVar.toString());
            intent.putExtra("INAPP_DATA_SIGNATURE", brVar.f1093b);
        }
        if (str != null) {
            intent.putExtra("MESSAGE", str);
        }
        return intent;
    }

    /* renamed from: a */
    private BitmapDrawable m789a(Bitmap bitmap) {
        if (bitmap == null) {
            return null;
        }
        return new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, DisplayUtils.pxFromDp(this, ((float) bitmap.getWidth()) * 0.5f), DisplayUtils.pxFromDp(this, ((float) bitmap.getHeight()) * 0.5f), false));
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public String m790a(String str) {
        if (this.f1042p.containsKey(str)) {
            return (String) this.f1042p.get(str);
        }
        return null;
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m792a() {
        if (this.f1045s == null || this.f1045s.isShown()) {
            m814c();
            Handler handler = this.f1048v;
            C1377ax axVar = new C1377ax(this);
            this.f1049w = axVar;
            handler.postDelayed(axVar, 10000);
            return;
        }
        this.f1048v.post(new C1388bm(this));
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m793a(int i, String str) {
        this.f1029c = true;
        m810b();
        this.f1048v.post(new C1378ay(this, i, str));
    }

    /* renamed from: a */
    static /* synthetic */ void m795a(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f1025P = i;
        purchaseActivity.f1018I = false;
        purchaseActivity.f1015F.setEnabled(false);
        purchaseActivity.f1016G.setEnabled(false);
        if (purchaseActivity.f1052z != null) {
            purchaseActivity.f1052z.setEnabled(false);
        }
        purchaseActivity.m792a();
        C1406j.m868a(((Country) purchaseActivity.f1010A.getItem(i)).getCode());
        purchaseActivity.m804a((C1392bq) new C1375as(purchaseActivity), true);
    }

    /* renamed from: a */
    static /* synthetic */ void m798a(PurchaseActivity purchaseActivity, String str, List list) {
        int i;
        if (!purchaseActivity.f1018I) {
            purchaseActivity.f1018I = true;
            purchaseActivity.f1012C.mo21499a(str, list);
            purchaseActivity.f1012C.f1106f = null;
            if (purchaseActivity.f1013D != null) {
                Point screenSize = DisplayUtils.getScreenSize(purchaseActivity);
                int count = purchaseActivity.f1012C.getCount();
                int pxFromDp = screenSize.x / DisplayUtils.pxFromDp(purchaseActivity, 80.0f);
                if (count != 0) {
                    if ((count - pxFromDp) + 1 > 0) {
                        purchaseActivity.f1052z.setVisibility(0);
                        int floor = (int) Math.floor((double) (((float) purchaseActivity.f1052z.getLeft()) / ((float) DisplayUtils.pxFromDp(purchaseActivity, 80.0f))));
                        LinearLayout.LayoutParams layoutParams = (LinearLayout.LayoutParams) purchaseActivity.f1028b.getLayoutParams();
                        layoutParams.width = purchaseActivity.f1052z.getLeft();
                        purchaseActivity.f1028b.setLayoutParams(layoutParams);
                        i = floor - 1;
                    } else {
                        purchaseActivity.f1052z.setVisibility(8);
                        LinearLayout.LayoutParams layoutParams2 = (LinearLayout.LayoutParams) purchaseActivity.f1028b.getLayoutParams();
                        layoutParams2.width = -1;
                        purchaseActivity.f1028b.setLayoutParams(layoutParams2);
                        i = count;
                    }
                    List list2 = purchaseActivity.f1013D.f989c;
                    if (list2 == null) {
                        list2 = new ArrayList();
                    }
                    list2.clear();
                    for (int i2 = 0; i2 < i; i2++) {
                        list2.add(new Integer(i2));
                    }
                    C1394bs bsVar = purchaseActivity.f1013D;
                    bsVar.f1096e = 0;
                    bsVar.f1097f = null;
                    if (!purchaseActivity.f1019J || purchaseActivity.f1014E == null) {
                        purchaseActivity.f1013D.mo21548a((C1365a) purchaseActivity.f1012C.getItem(0));
                    } else {
                        purchaseActivity.f1013D.mo21548a(purchaseActivity.f1014E);
                    }
                    purchaseActivity.f1013D.mo21499a(purchaseActivity.f1012C.mo21498a(), list2);
                }
            }
            if (purchaseActivity.f1019J) {
                String a = purchaseActivity.m790a("welcomePage");
                String url = purchaseActivity.f1051y.getUrl();
                if (url == null || !url.equals(a)) {
                    purchaseActivity.f1051y.stopLoading();
                    purchaseActivity.f1051y.clearCache(true);
                    purchaseActivity.f1051y.loadUrl(a);
                    return;
                }
                purchaseActivity.m810b();
                purchaseActivity.f1029c = true;
                purchaseActivity.f1018I = false;
                return;
            }
            purchaseActivity.f1012C.f1105e = 0;
            purchaseActivity.m803a(purchaseActivity.f1013D != null ? purchaseActivity.f1013D.f1097f : (C1365a) purchaseActivity.f1012C.getItem(0));
        }
    }

    /* renamed from: a */
    static /* synthetic */ void m799a(PurchaseActivity purchaseActivity, C1362g gVar, boolean z) {
        int i;
        LinearLayout linearLayout;
        purchaseActivity.f1042p = gVar.f997d;
        if (purchaseActivity.m790a("infoPage") != null) {
            purchaseActivity.f1014E = new C1395f("Info", purchaseActivity.m790a("infoPage"));
        }
        purchaseActivity.f1043q = gVar.f995b;
        boolean z2 = purchaseActivity.f1014E != null && ((C1395f) purchaseActivity.f1014E).f1099b;
        int pxFromDp = DisplayUtils.pxFromDp(purchaseActivity, z ? 40.0f : 120.0f);
        int i2 = z2 ? DisplayUtils.pxFromDp(purchaseActivity, 40.0f) : 0;
        int i3 = DisplayUtils.getScreenSize(purchaseActivity).x;
        LinearLayout linearLayout2 = new LinearLayout(purchaseActivity);
        linearLayout2.setBackgroundColor(-3355444);
        linearLayout2.setOrientation(1);
        LayoutParams layoutParams = new LayoutParams(-1, -1);
        layoutParams.addRule(13);
        purchaseActivity.f1027a.addView(linearLayout2, layoutParams);
        LinearLayout linearLayout3 = new LinearLayout(purchaseActivity);
        linearLayout3.setBackgroundColor(-1);
        linearLayout3.setOrientation(0);
        linearLayout3.setHorizontalGravity(3);
        linearLayout3.setVerticalGravity(17);
        linearLayout2.addView(linearLayout3, new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 40.0f)));
        GradientDrawable gradientDrawable = new GradientDrawable();
        gradientDrawable.setStroke(1, -3355444);
        gradientDrawable.setColor(-1);
        gradientDrawable.setGradientType(2);
        LayerDrawable layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
        layerDrawable.setLayerInset(0, -2, -2, -2, 0);
        if (VERSION.SDK_INT >= 16) {
            linearLayout3.setBackground(layerDrawable);
        } else {
            linearLayout3.setBackgroundDrawable(layerDrawable);
        }
        ImageButton imageButton = new ImageButton(purchaseActivity);
        imageButton.setMinimumWidth(0);
        imageButton.setMinimumHeight(0);
        imageButton.setBackgroundColor(Color.rgb(92, 176, 59));
        imageButton.setPadding(0, 0, 0, 0);
        imageButton.setOnClickListener(new C1390bo(purchaseActivity));
        imageButton.setImageDrawable(purchaseActivity.m789a(C1415m.m933g()));
        linearLayout3.addView(imageButton, new LinearLayout.LayoutParams(pxFromDp, -1));
        LinearLayout linearLayout4 = new LinearLayout(purchaseActivity);
        linearLayout4.setWeightSum(4.0f);
        linearLayout3.addView(linearLayout4, new LinearLayout.LayoutParams((i3 - pxFromDp) - i2, -1));
        purchaseActivity.f1010A = new C1360d(purchaseActivity);
        purchaseActivity.f1010A.mo21499a(purchaseActivity.m790a(UserDataStore.COUNTRY), gVar.f998e);
        int i4 = 0;
        while (true) {
            i = i4;
            if (i >= purchaseActivity.f1010A.getCount()) {
                i = 0;
                break;
            } else if (((Country) purchaseActivity.f1010A.getItem(i)).getCode().equals(C1406j.m858a())) {
                break;
            } else {
                i4 = i + 1;
            }
        }
        LinearLayout.LayoutParams layoutParams2 = new LinearLayout.LayoutParams(0, -1);
        layoutParams2.weight = 2.0f;
        layoutParams2.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 5.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f1015F = new Spinner(purchaseActivity, 1);
        purchaseActivity.f1015F.setBackgroundColor(0);
        purchaseActivity.f1015F.setAdapter(purchaseActivity.f1010A);
        purchaseActivity.f1015F.setSelection(i);
        purchaseActivity.f1015F.setPadding(0, 0, 0, 0);
        purchaseActivity.f1015F.setEnabled(purchaseActivity.f1010A.getCount() > 1);
        purchaseActivity.f1015F.setOnTouchListener(new C1391bp(purchaseActivity));
        purchaseActivity.f1025P = i;
        int i5 = VERSION.SDK_INT;
        purchaseActivity.f1015F.setOnItemSelectedListener(new C1400m(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f1015F, layoutParams2);
        purchaseActivity.f1011B = new C1379b(purchaseActivity);
        purchaseActivity.f1011B.mo21499a(purchaseActivity.m790a("paymentType"), gVar.f996c);
        ImageView imageView = new ImageView(purchaseActivity);
        imageView.setImageBitmap(C1415m.m932f());
        imageView.setPadding(0, 0, 0, 0);
        LinearLayout.LayoutParams layoutParams3 = new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 9.0f), -1);
        layoutParams3.weight = 0.0f;
        linearLayout4.addView(imageView, layoutParams3);
        LinearLayout.LayoutParams layoutParams4 = new LinearLayout.LayoutParams(0, -1);
        layoutParams4.weight = 2.0f;
        layoutParams4.setMargins(DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f), DisplayUtils.pxFromDp(purchaseActivity, 0.0f));
        purchaseActivity.f1026Q = 0;
        purchaseActivity.f1016G = new Spinner(purchaseActivity, 1);
        purchaseActivity.f1016G.setBackgroundColor(0);
        purchaseActivity.f1016G.setAdapter(purchaseActivity.f1011B);
        purchaseActivity.f1016G.setSelection(0);
        purchaseActivity.f1016G.setPadding(0, 0, 0, 0);
        purchaseActivity.f1016G.setEnabled(purchaseActivity.f1011B.getCount() > 1);
        int i6 = VERSION.SDK_INT;
        purchaseActivity.f1016G.setOnTouchListener(new C1401p(purchaseActivity));
        purchaseActivity.f1016G.setOnItemSelectedListener(new C1402q(purchaseActivity));
        linearLayout4.addView(purchaseActivity.f1016G, layoutParams4);
        if (z2) {
            ImageButton imageButton2 = new ImageButton(purchaseActivity);
            imageButton2.setMinimumWidth(0);
            imageButton2.setMinimumHeight(0);
            imageButton2.setBackgroundColor(Color.rgb(92, 176, 59));
            imageButton2.setPadding(0, 0, 0, 0);
            imageButton2.setImageDrawable(purchaseActivity.m789a(C1415m.m934h()));
            imageButton2.setOnClickListener(new C1403u(purchaseActivity));
            linearLayout3.addView(imageButton2, new LinearLayout.LayoutParams(i2, -1));
        }
        purchaseActivity.f1012C = new C1398i(purchaseActivity);
        if (z) {
            RelativeLayout relativeLayout = new RelativeLayout(purchaseActivity);
            relativeLayout.setBackgroundColor(Color.rgb(241, 241, 241));
            linearLayout2.addView(relativeLayout, new LinearLayout.LayoutParams(-1, -2));
            int pxFromDp2 = DisplayUtils.pxFromDp(purchaseActivity, 2.0f);
            GradientDrawable gradientDrawable2 = new GradientDrawable();
            gradientDrawable2.setStroke(pxFromDp2, Color.rgb(213, 213, 213));
            gradientDrawable2.setColor(Color.rgb(241, 241, 241));
            gradientDrawable2.setGradientType(2);
            LayerDrawable layerDrawable2 = new LayerDrawable(new Drawable[]{gradientDrawable2});
            layerDrawable2.setLayerInset(0, -pxFromDp2, -pxFromDp2, -pxFromDp2, 0);
            if (VERSION.SDK_INT >= 16) {
                relativeLayout.setBackground(layerDrawable2);
            } else {
                relativeLayout.setBackgroundDrawable(layerDrawable2);
            }
            LayoutParams layoutParams5 = new LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 70.0f), DisplayUtils.pxFromDp(purchaseActivity, 40.0f));
            layoutParams5.addRule(11);
            layoutParams5.addRule(15);
            purchaseActivity.f1052z = new Button(purchaseActivity);
            purchaseActivity.f1052z.setText("MORE");
            purchaseActivity.f1052z.setTextSize(16.0f);
            purchaseActivity.f1052z.setMinimumWidth(0);
            purchaseActivity.f1052z.setMinWidth(0);
            purchaseActivity.f1052z.setMaxWidth(DisplayUtils.pxFromDp(purchaseActivity, 70.0f));
            purchaseActivity.f1052z.setTypeface(Typeface.DEFAULT_BOLD);
            purchaseActivity.f1052z.setTextColor(Color.rgb(92, 176, 59));
            Bitmap d = C1415m.m930d();
            if (d != null) {
                purchaseActivity.f1052z.setCompoundDrawablesWithIntrinsicBounds(null, null, purchaseActivity.m789a(d), null);
            }
            purchaseActivity.f1052z.setCompoundDrawablePadding(DisplayUtils.pxFromDp(purchaseActivity, 4.0f));
            purchaseActivity.f1052z.setBackgroundColor(0);
            purchaseActivity.f1052z.setPadding(0, 0, DisplayUtils.pxFromDp(purchaseActivity, 4.0f), 0);
            purchaseActivity.f1052z.setOnTouchListener(new C1404x(purchaseActivity));
            purchaseActivity.f1052z.setOnClickListener(new C1405y(purchaseActivity));
            relativeLayout.addView(purchaseActivity.f1052z, layoutParams5);
            LinearLayout linearLayout5 = new LinearLayout(purchaseActivity);
            linearLayout5.setBackgroundColor(0);
            linearLayout5.setOrientation(0);
            LayoutParams layoutParams6 = new LayoutParams(-2, -2);
            layoutParams6.addRule(9);
            layoutParams6.addRule(15);
            relativeLayout.addView(linearLayout5, layoutParams6);
            purchaseActivity.f1028b = new C1414c(purchaseActivity);
            purchaseActivity.f1028b.setBackgroundColor(0);
            purchaseActivity.f1028b.setScrollingEnabled(false);
            purchaseActivity.f1028b.setHorizontalFadingEdgeEnabled(false);
            C1414c cVar = purchaseActivity.f1028b;
            C1394bs bsVar = new C1394bs(purchaseActivity, purchaseActivity.f1012C);
            purchaseActivity.f1013D = bsVar;
            cVar.setAdapter((ListAdapter) bsVar);
            purchaseActivity.f1028b.setOnItemClickListener(new C1366ac(purchaseActivity));
            linearLayout5.addView(purchaseActivity.f1028b, new LinearLayout.LayoutParams(-1, DisplayUtils.pxFromDp(purchaseActivity, 60.0f)));
            linearLayout = new LinearLayout(purchaseActivity);
            linearLayout.setBackgroundColor(Color.rgb(100, 100, 100));
            linearLayout.setOrientation(1);
            linearLayout2.addView(linearLayout, new LinearLayout.LayoutParams(-1, -1));
        } else {
            purchaseActivity.f1012C.f1104d = 2;
            LinearLayout linearLayout6 = new LinearLayout(purchaseActivity);
            linearLayout6.setBackgroundColor(0);
            linearLayout6.setOrientation(0);
            linearLayout2.addView(linearLayout6, new LinearLayout.LayoutParams(-1, -1));
            GradientDrawable gradientDrawable3 = new GradientDrawable();
            gradientDrawable3.setStroke(1, Color.rgb(230, 230, 230));
            gradientDrawable3.setColor(Color.rgb(241, 241, 241));
            gradientDrawable3.setGradientType(2);
            LayerDrawable layerDrawable3 = new LayerDrawable(new Drawable[]{gradientDrawable3});
            layerDrawable3.setLayerInset(0, -2, -2, 0, -2);
            ListView listView = new ListView(purchaseActivity);
            if (VERSION.SDK_INT >= 16) {
                listView.setBackground(layerDrawable3);
            } else {
                listView.setBackgroundDrawable(layerDrawable3);
            }
            listView.setAdapter(purchaseActivity.f1012C);
            listView.setOnTouchListener(new C1367af(purchaseActivity));
            listView.setOnItemClickListener(new C1368ag(purchaseActivity));
            listView.setOnItemLongClickListener(new C1369aj(purchaseActivity));
            linearLayout6.addView(listView, new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(purchaseActivity, 120.0f), -1));
            linearLayout = linearLayout6;
        }
        purchaseActivity.f1017H.setCallback(new C1371an(purchaseActivity));
        purchaseActivity.f1051y = new WebView(purchaseActivity);
        purchaseActivity.f1051y.addJavascriptInterface(purchaseActivity.f1017H, SettingsJsonConstants.APP_KEY);
        purchaseActivity.f1051y.setHorizontalScrollBarEnabled(true);
        purchaseActivity.f1051y.setVerticalScrollBarEnabled(true);
        purchaseActivity.f1051y.setHapticFeedbackEnabled(false);
        purchaseActivity.f1051y.setOnLongClickListener(new C1372ap(purchaseActivity));
        purchaseActivity.f1051y.setLongClickable(false);
        purchaseActivity.f1051y.getSettings().setAllowContentAccess(true);
        purchaseActivity.f1051y.getSettings().setJavaScriptCanOpenWindowsAutomatically(true);
        purchaseActivity.f1051y.getSettings().setJavaScriptEnabled(true);
        purchaseActivity.f1051y.getSettings().setSupportZoom(true);
        purchaseActivity.f1051y.getSettings().setDomStorageEnabled(true);
        purchaseActivity.f1051y.getSettings().setUseWideViewPort(true);
        purchaseActivity.f1051y.getSettings().setLoadWithOverviewMode(true);
        purchaseActivity.f1051y.getSettings().setAppCacheEnabled(false);
        purchaseActivity.f1051y.getSettings().setCacheMode(2);
        purchaseActivity.f1051y.setWebChromeClient(new WebChromeClient());
        purchaseActivity.f1051y.setWebViewClient(new C1373aq(purchaseActivity));
        purchaseActivity.f1051y.setOnTouchListener(new C1374ar(purchaseActivity));
        linearLayout.addView(purchaseActivity.f1051y, new LinearLayout.LayoutParams(-1, -1));
        if (purchaseActivity.f1045s != null) {
            purchaseActivity.f1045s.bringToFront();
        }
        purchaseActivity.f1019J = true;
    }

    /* renamed from: a */
    static /* synthetic */ void m802a(PurchaseActivity purchaseActivity, C1408n nVar) {
        if (!purchaseActivity.f1019J && !purchaseActivity.f1030d) {
            if (nVar == null || !purchaseActivity.m805a(nVar.f1127c)) {
                JSONObject jSONObject = nVar.f1126b;
                try {
                    purchaseActivity.f1035i = new C1393br(purchaseActivity.f1031e, purchaseActivity.getPackageName(), jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has("order_id") ? jSONObject.getString("order_id") : "xxx", jSONObject.has("timestamp") ? jSONObject.getLong("timestamp") : 0, jSONObject.has(MessengerShareContentUtility.ATTACHMENT_PAYLOAD) ? jSONObject.getString(MessengerShareContentUtility.ATTACHMENT_PAYLOAD) : purchaseActivity.f1034h, jSONObject.has("gp_status") ? jSONObject.getInt("gp_status") : 1);
                    purchaseActivity.f1024O = (jSONObject.has("timer") ? jSONObject.getInt("timer") : 5) * 1000;
                    purchaseActivity.f1041o = jSONObject.has("header_text") ? jSONObject.getString("header_text") : "Confirmation";
                    purchaseActivity.f1040n = jSONObject.has("info_text") ? jSONObject.getString("info_text") : "Cancel current Purchase?";
                    purchaseActivity.f1038l = jSONObject.has("yes_btn_text") ? jSONObject.getString("yes_btn_text") : "Yes";
                    purchaseActivity.f1039m = jSONObject.has("no_btn_text") ? jSONObject.getString("no_btn_text") : "No";
                } catch (JSONException e) {
                }
                purchaseActivity.f1020K = false;
                purchaseActivity.f1021L = false;
                purchaseActivity.f1051y.stopLoading();
                purchaseActivity.f1051y.clearCache(true);
                purchaseActivity.f1051y.loadUrl(nVar.f1125a);
            }
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m803a(C1365a aVar) {
        if (aVar != null) {
            if (this.f1047u != null) {
                this.f1047u.cancel(true);
                this.f1047u = null;
            }
            m792a();
            this.f1051y.stopLoading();
            if (aVar instanceof C1395f) {
                this.f1012C.f1105e = -1;
                if (this.f1012C.f1106f != null) {
                    this.f1012C.f1106f.setBackgroundColor(Color.rgb(241, 241, 241));
                }
                this.f1012C.f1106f = null;
                this.f1051y.loadUrl(((C1395f) aVar).f1098a);
                return;
            }
            this.f1047u = new C1376at(this, aVar).execute(new Void[0]);
        }
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m804a(C1392bq bqVar, boolean z) {
        new C1387bk(this, z, bqVar).execute(new Void[0]);
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public boolean m805a(C1361f fVar) {
        if (fVar == null || fVar.f992b) {
            return false;
        }
        m810b();
        new Builder(this).setTitle("Error(" + String.valueOf(fVar.f991a) + ")").setMessage(fVar.f993c).setNegativeButton("Dismiss", new C1381bc(this, fVar)).setOnCancelListener(new C1380bb(this, fVar)).show();
        return true;
    }

    /* access modifiers changed from: private */
    /* renamed from: b */
    public void m810b() {
        m814c();
        if (this.f1045s != null && this.f1045s.isShown()) {
            this.f1048v.post(new C1389bn(this));
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m811b(PurchaseActivity purchaseActivity, int i, String str) {
        purchaseActivity.setResult(-1, m788a(i, (C1393br) null, str));
        purchaseActivity.finish();
    }

    /* renamed from: b */
    static /* synthetic */ void m812b(PurchaseActivity purchaseActivity, String str) {
        int i = 1;
        try {
            C1393br brVar = new C1393br(purchaseActivity.f1031e, str.substring(9), purchaseActivity.getPackageName());
            if (brVar.f1092a == 0) {
                i = -1;
            }
            purchaseActivity.setResult(i, m788a(brVar.f1092a, brVar, (String) null));
            purchaseActivity.finish();
        } catch (Exception e) {
            purchaseActivity.m793a(-1002, "Something went wrong!\nException: " + e.getLocalizedMessage());
        }
    }

    /* renamed from: b */
    static /* synthetic */ void m813b(PurchaseActivity purchaseActivity, boolean z) {
        purchaseActivity.f1030d = z;
        if (z) {
            if (purchaseActivity.f1015F != null) {
                purchaseActivity.f1015F.setAlpha(0.4f);
            }
            if (purchaseActivity.f1016G != null) {
                purchaseActivity.f1016G.setAlpha(0.4f);
            }
            if (purchaseActivity.f1052z != null) {
                purchaseActivity.f1052z.setAlpha(0.4f);
            }
            if (purchaseActivity.f1013D != null) {
                purchaseActivity.f1013D.mo21548a(purchaseActivity.f1014E);
            }
            purchaseActivity.m803a(purchaseActivity.f1014E);
            return;
        }
        if (purchaseActivity.f1019J) {
            String a = purchaseActivity.m790a("welcomePage");
            String url = purchaseActivity.f1051y.getUrl();
            if (url == null || !url.equals(a)) {
                purchaseActivity.f1051y.stopLoading();
                purchaseActivity.f1051y.clearCache(true);
                purchaseActivity.f1051y.loadUrl(a);
            } else {
                purchaseActivity.m810b();
                purchaseActivity.f1029c = true;
                purchaseActivity.f1018I = false;
            }
        }
        if (purchaseActivity.f1015F != null) {
            purchaseActivity.f1015F.setAlpha(1.0f);
        }
        if (purchaseActivity.f1016G != null) {
            purchaseActivity.f1016G.setAlpha(1.0f);
        }
        if (purchaseActivity.f1052z != null) {
            purchaseActivity.f1052z.setAlpha(1.0f);
        }
    }

    /* renamed from: c */
    private void m814c() {
        this.f1048v.removeCallbacks(this.f1049w);
        this.f1049w = null;
    }

    /* renamed from: c */
    static /* synthetic */ void m816c(PurchaseActivity purchaseActivity, int i) {
        purchaseActivity.f1013D.mo21548a((C1365a) purchaseActivity.f1012C.getItem(i));
        purchaseActivity.f1012C.f1105e = i;
        purchaseActivity.m803a((C1365a) purchaseActivity.f1012C.getItem(i));
    }

    /* renamed from: m */
    static /* synthetic */ void m829m(PurchaseActivity purchaseActivity) {
        if (((purchaseActivity.f1019J || purchaseActivity.f1030d) && purchaseActivity.f1035i == null) || purchaseActivity.f1035i == null || !purchaseActivity.f1020K) {
            purchaseActivity.setResult(0, m788a(-1005, (C1393br) null, "User cancelled"));
        } else {
            try {
                purchaseActivity.setResult(-1, m788a(purchaseActivity.f1035i.f1092a, purchaseActivity.f1035i, (String) null));
            } catch (Exception e) {
                purchaseActivity.setResult(0, m788a(-1005, (C1393br) null, "User cancelled"));
            }
        }
        purchaseActivity.finish();
    }

    public void finish() {
        m810b();
        this.f1048v.removeCallbacksAndMessages(null);
        if (this.f1047u != null) {
            this.f1047u.cancel(true);
        }
        super.finish();
    }

    /* access modifiers changed from: protected */
    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
    }

    public void onBackPressed() {
        if (this.f1029c) {
            if (((this.f1019J || this.f1030d) && this.f1035i == null) || this.f1035i == null || !this.f1020K) {
                setResult(0, m788a(-1005, (C1393br) null, "User cancelled"));
            } else {
                try {
                    setResult(-1, m788a(this.f1035i.f1092a, this.f1035i, (String) null));
                } catch (Exception e) {
                    setResult(0, m788a(-1005, (C1393br) null, "User cancelled"));
                }
            }
            this.f1048v.removeCallbacksAndMessages(null);
            if (this.f1047u != null) {
                this.f1047u.cancel(true);
                this.f1047u = null;
            }
            super.onBackPressed();
        }
    }

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestWindowFeature(1);
        C1415m.m920a(getFilesDir().getPath());
        this.f1046t = getSharedPreferences("_config_", 0);
        this.f1048v = new Handler();
        this.f1033g = getIntent().getExtras().getString("gid");
        this.f1032f = getIntent().getExtras().getString("guid");
        this.f1031e = getIntent().getExtras().getString("sku");
        this.f1034h = getIntent().getExtras().getString(MessengerShareContentUtility.ATTACHMENT_PAYLOAD);
        String string = getIntent().getExtras().getString("json");
        if (string != null) {
            try {
                this.f1044r = new SkuDetails(string);
                JSONObject jSONObject = new JSONObject(string);
                this.f1036j = jSONObject.getString("price_amount_micros");
                this.f1037k = jSONObject.getString("price_currency_code");
            } catch (JSONException e) {
            }
        }
        FrameLayout frameLayout = new FrameLayout(this);
        frameLayout.setBackgroundColor(0);
        setContentView(frameLayout, new ViewGroup.LayoutParams(-1, -1));
        this.f1027a = new RelativeLayout(this);
        this.f1027a.setBackgroundColor(0);
        frameLayout.addView(this.f1027a, new FrameLayout.LayoutParams(-1, -1));
        this.f1045s = new ProgressBar(this);
        this.f1045s.setIndeterminate(true);
        this.f1045s.setVisibility(4);
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.addRule(13);
        this.f1027a.addView(this.f1045s, layoutParams);
        m792a();
        this.f1048v.post(new C1370am(this));
    }

    /* access modifiers changed from: protected */
    public void onResume() {
        super.onResume();
        DisplayUtils.lockOrientation(this);
    }
}
