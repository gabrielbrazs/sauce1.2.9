package com.google.android.gms.common.util;

import android.support.annotation.Nullable;
import java.util.regex.Pattern;

public final class zzs {
    private static final Pattern zzfzb = Pattern.compile("\\$\\{(.*?)\\}");

    public static boolean zzgl(@Nullable String str) {
        return str == null || str.trim().isEmpty();
    }
}
