package android.support.customtabs;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.support.annotation.RestrictTo;
import android.support.annotation.RestrictTo.Scope;
import android.support.customtabs.ICustomTabsCallback.Stub;
import android.text.TextUtils;
import java.util.ArrayList;
import java.util.List;

public class CustomTabsClient {
    private final ICustomTabsService mService;
    private final ComponentName mServiceComponentName;

    @RestrictTo({Scope.LIBRARY_GROUP})
    CustomTabsClient(ICustomTabsService iCustomTabsService, ComponentName componentName) {
        this.mService = iCustomTabsService;
        this.mServiceComponentName = componentName;
    }

    public static boolean bindCustomTabsService(Context context, String str, CustomTabsServiceConnection customTabsServiceConnection) {
        Intent intent = new Intent("android.support.customtabs.action.CustomTabsService");
        if (!TextUtils.isEmpty(str)) {
            intent.setPackage(str);
        }
        return context.bindService(intent, customTabsServiceConnection, 33);
    }

    public static boolean connectAndInitialize(Context context, String str) {
        boolean z = false;
        if (str == null) {
            return z;
        }
        final Context applicationContext = context.getApplicationContext();
        try {
            return bindCustomTabsService(applicationContext, str, new CustomTabsServiceConnection() {
                public final void onCustomTabsServiceConnected(ComponentName componentName, CustomTabsClient customTabsClient) {
                    customTabsClient.warmup(0);
                    applicationContext.unbindService(this);
                }

                public final void onServiceDisconnected(ComponentName componentName) {
                }
            });
        } catch (SecurityException e) {
            return z;
        }
    }

    public static String getPackageName(Context context, @Nullable List<String> list) {
        return getPackageName(context, list, false);
    }

    public static String getPackageName(Context context, @Nullable List<String> list, boolean z) {
        PackageManager packageManager = context.getPackageManager();
        List<String> list2 = list == null ? new ArrayList<>() : list;
        Intent intent = new Intent("android.intent.action.VIEW", Uri.parse("http://"));
        if (!z) {
            ResolveInfo resolveActivity = packageManager.resolveActivity(intent, 0);
            if (resolveActivity != null) {
                String str = resolveActivity.activityInfo.packageName;
                ArrayList arrayList = new ArrayList(list2.size() + 1);
                arrayList.add(str);
                if (list != null) {
                    arrayList.addAll(list);
                }
                list2 = arrayList;
            }
        }
        Intent intent2 = new Intent("android.support.customtabs.action.CustomTabsService");
        for (String str2 : list2) {
            intent2.setPackage(str2);
            if (packageManager.resolveService(intent2, 0) != null) {
                return str2;
            }
        }
        return null;
    }

    public Bundle extraCommand(String str, Bundle bundle) {
        try {
            return this.mService.extraCommand(str, bundle);
        } catch (RemoteException e) {
            return null;
        }
    }

    public CustomTabsSession newSession(final CustomTabsCallback customTabsCallback) {
        C00172 r1 = new Stub() {
            private Handler mHandler = new Handler(Looper.getMainLooper());

            public void extraCallback(final String str, final Bundle bundle) throws RemoteException {
                if (customTabsCallback != null) {
                    this.mHandler.post(new Runnable() {
                        public void run() {
                            customTabsCallback.extraCallback(str, bundle);
                        }
                    });
                }
            }

            public void onMessageChannelReady(final Bundle bundle) throws RemoteException {
                if (customTabsCallback != null) {
                    this.mHandler.post(new Runnable() {
                        public void run() {
                            customTabsCallback.onMessageChannelReady(bundle);
                        }
                    });
                }
            }

            public void onNavigationEvent(final int i, final Bundle bundle) {
                if (customTabsCallback != null) {
                    this.mHandler.post(new Runnable() {
                        public void run() {
                            customTabsCallback.onNavigationEvent(i, bundle);
                        }
                    });
                }
            }

            public void onPostMessage(final String str, final Bundle bundle) throws RemoteException {
                if (customTabsCallback != null) {
                    this.mHandler.post(new Runnable() {
                        public void run() {
                            customTabsCallback.onPostMessage(str, bundle);
                        }
                    });
                }
            }
        };
        try {
            if (!this.mService.newSession(r1)) {
                return null;
            }
            return new CustomTabsSession(this.mService, r1, this.mServiceComponentName);
        } catch (RemoteException e) {
            return null;
        }
    }

    public boolean warmup(long j) {
        try {
            return this.mService.warmup(j);
        } catch (RemoteException e) {
            return false;
        }
    }
}