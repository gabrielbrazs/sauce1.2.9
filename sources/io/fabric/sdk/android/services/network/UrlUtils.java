package io.fabric.sdk.android.services.network;

import android.text.TextUtils;
import java.net.URI;
import java.net.URLDecoder;
import java.net.URLEncoder;
import java.util.TreeMap;

public final class UrlUtils {
    public static final String UTF8 = "UTF8";

    private UrlUtils() {
    }

    public static TreeMap<String, String> getQueryParams(String str, boolean z) {
        TreeMap<String, String> treeMap = new TreeMap();
        if (str != null) {
            for (String split : str.split("&")) {
                String[] split2 = split.split("=");
                if (split2.length == 2) {
                    if (z) {
                        treeMap.put(urlDecode(split2[0]), urlDecode(split2[1]));
                    } else {
                        treeMap.put(split2[0], split2[1]);
                    }
                } else if (!TextUtils.isEmpty(split2[0])) {
                    if (z) {
                        treeMap.put(urlDecode(split2[0]), "");
                    } else {
                        treeMap.put(split2[0], "");
                    }
                }
            }
        }
        return treeMap;
    }

    public static TreeMap<String, String> getQueryParams(URI uri, boolean z) {
        return getQueryParams(uri.getRawQuery(), z);
    }

    public static String percentEncode(String str) {
        if (str == null) {
            return "";
        }
        StringBuilder stringBuilder = new StringBuilder();
        String urlEncode = urlEncode(str);
        int length = urlEncode.length();
        int i = 0;
        while (i < length) {
            char charAt = urlEncode.charAt(i);
            if (charAt == '*') {
                stringBuilder.append("%2A");
            } else if (charAt == '+') {
                stringBuilder.append("%20");
            } else if (charAt == '%' && i + 2 < length && urlEncode.charAt(i + 1) == '7' && urlEncode.charAt(i + 2) == 'E') {
                stringBuilder.append('~');
                i += 2;
            } else {
                stringBuilder.append(charAt);
            }
            i++;
        }
        return stringBuilder.toString();
    }

    public static String urlDecode(String str) {
        if (str == null) {
            return "";
        }
        try {
            return URLDecoder.decode(str, UTF8);
        } catch (Throwable e) {
            throw new RuntimeException(e.getMessage(), e);
        }
    }

    public static String urlEncode(String str) {
        if (str == null) {
            return "";
        }
        try {
            return URLEncoder.encode(str, UTF8);
        } catch (Throwable e) {
            throw new RuntimeException(e.getMessage(), e);
        }
    }
}
