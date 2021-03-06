package com.facebook.internal;

import android.util.Log;
import com.facebook.FacebookSdk;
import com.facebook.LoggingBehavior;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map.Entry;

public class Logger {
    public static final String LOG_TAG_BASE = "FacebookSDK.";
    private static final HashMap<String, String> stringsToReplace = new HashMap();
    private final LoggingBehavior behavior;
    private StringBuilder contents;
    private int priority = 3;
    private final String tag;

    public Logger(LoggingBehavior loggingBehavior, String str) {
        Validate.notNullOrEmpty(str, "tag");
        this.behavior = loggingBehavior;
        this.tag = LOG_TAG_BASE + str;
        this.contents = new StringBuilder();
    }

    public static void log(LoggingBehavior loggingBehavior, int i, String str, String str2) {
        if (FacebookSdk.isLoggingBehaviorEnabled(loggingBehavior)) {
            String replaceStrings = replaceStrings(str2);
            if (!str.startsWith(LOG_TAG_BASE)) {
                str = LOG_TAG_BASE + str;
            }
            Log.println(i, str, replaceStrings);
            if (loggingBehavior == LoggingBehavior.DEVELOPER_ERRORS) {
                new Exception().printStackTrace();
            }
        }
    }

    public static void log(LoggingBehavior loggingBehavior, int i, String str, String str2, Object... objArr) {
        if (FacebookSdk.isLoggingBehaviorEnabled(loggingBehavior)) {
            log(loggingBehavior, i, str, String.format(str2, objArr));
        }
    }

    public static void log(LoggingBehavior loggingBehavior, String str, String str2) {
        log(loggingBehavior, 3, str, str2);
    }

    public static void log(LoggingBehavior loggingBehavior, String str, String str2, Object... objArr) {
        if (FacebookSdk.isLoggingBehaviorEnabled(loggingBehavior)) {
            log(loggingBehavior, 3, str, String.format(str2, objArr));
        }
    }

    public static void registerAccessToken(String str) {
        synchronized (Logger.class) {
            try {
                if (!FacebookSdk.isLoggingBehaviorEnabled(LoggingBehavior.INCLUDE_ACCESS_TOKENS)) {
                    registerStringToReplace(str, "ACCESS_TOKEN_REMOVED");
                }
            } catch (Throwable th) {
                Class cls = Logger.class;
            }
        }
    }

    public static void registerStringToReplace(String str, String str2) {
        synchronized (Logger.class) {
            try {
                Object obj = stringsToReplace;
                obj.put(str, str2);
            } finally {
                Class cls = Logger.class;
            }
        }
    }

    private static String replaceStrings(String str) {
        synchronized (Logger.class) {
            try {
                Iterator it = stringsToReplace.entrySet().iterator();
                while (true) {
                    Object hasNext = it.hasNext();
                    if (hasNext == null) {
                        break;
                    }
                    Entry entry = (Entry) it.next();
                    str = str.replace((CharSequence) entry.getKey(), (CharSequence) entry.getValue());
                }
                return str;
            } finally {
                Class cls = Logger.class;
            }
        }
    }

    private boolean shouldLog() {
        return FacebookSdk.isLoggingBehaviorEnabled(this.behavior);
    }

    public void append(String str) {
        if (shouldLog()) {
            this.contents.append(str);
        }
    }

    public void append(String str, Object... objArr) {
        if (shouldLog()) {
            this.contents.append(String.format(str, objArr));
        }
    }

    public void append(StringBuilder stringBuilder) {
        if (shouldLog()) {
            this.contents.append(stringBuilder);
        }
    }

    public void appendKeyValue(String str, Object obj) {
        append("  %s:\t%s\n", str, obj);
    }

    public String getContents() {
        return replaceStrings(this.contents.toString());
    }

    public int getPriority() {
        return this.priority;
    }

    public void log() {
        logString(this.contents.toString());
        this.contents = new StringBuilder();
    }

    public void logString(String str) {
        log(this.behavior, this.priority, this.tag, str);
    }

    public void setPriority(int i) {
        Validate.oneOf(Integer.valueOf(i), Param.VALUE, Integer.valueOf(7), Integer.valueOf(3), Integer.valueOf(6), Integer.valueOf(4), Integer.valueOf(2), Integer.valueOf(5));
        this.priority = i;
    }
}
