package jp.colopl.libs;

import android.app.IntentService;
import android.content.Intent;

public class AssetService extends IntentService {
    /* renamed from: a */
    private static boolean f943a = false;

    static {
        System.loadLibrary("asset");
    }

    public AssetService() {
        super("AssetService");
    }

    public AssetService(String str) {
        super(str);
    }

    public native void asset();

    protected void onHandleIntent(Intent intent) {
        if (intent.getStringExtra("asset").equals("start") || !f943a) {
            asset();
            f943a = true;
        }
    }
}
