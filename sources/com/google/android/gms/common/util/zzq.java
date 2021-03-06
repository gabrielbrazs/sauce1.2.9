package com.google.android.gms.common.util;

import android.os.Process;
import android.os.StrictMode;
import android.os.StrictMode.ThreadPolicy;
import java.io.BufferedReader;
import java.io.Closeable;
import java.io.FileReader;
import java.io.IOException;

public final class zzq {
    private static String zzfyz = null;
    private static final int zzfza = Process.myPid();

    public static String zzalk() {
        if (zzfyz == null) {
            zzfyz = zzcg(zzfza);
        }
        return zzfyz;
    }

    private static String zzcg(int i) {
        Closeable bufferedReader;
        Throwable th;
        String str = null;
        if (i > 0) {
            ThreadPolicy allowThreadDiskReads;
            try {
                allowThreadDiskReads = StrictMode.allowThreadDiskReads();
                bufferedReader = new BufferedReader(new FileReader("/proc/" + i + "/cmdline"));
                try {
                    StrictMode.setThreadPolicy(allowThreadDiskReads);
                    str = bufferedReader.readLine().trim();
                    zzm.closeQuietly(bufferedReader);
                } catch (IOException e) {
                    zzm.closeQuietly(bufferedReader);
                    return str;
                } catch (Throwable th2) {
                    th = th2;
                    zzm.closeQuietly(bufferedReader);
                    throw th;
                }
            } catch (IOException e2) {
                bufferedReader = str;
                zzm.closeQuietly(bufferedReader);
                return str;
            } catch (Throwable th3) {
                Throwable th4 = th3;
                bufferedReader = str;
                th = th4;
                zzm.closeQuietly(bufferedReader);
                throw th;
            }
        }
        return str;
    }
}
