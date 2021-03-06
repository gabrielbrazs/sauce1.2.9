package com.github.droidfu.support;

import android.text.TextUtils;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.util.ArrayList;

public class StringSupport {
    private static String[] splitByCharacterType(String str, boolean z) {
        int i = 0;
        if (str == null) {
            return null;
        }
        if (str.length() == 0) {
            return new String[0];
        }
        char[] toCharArray = str.toCharArray();
        ArrayList arrayList = new ArrayList();
        int type = Character.getType(toCharArray[0]);
        for (int i2 = 1; i2 < toCharArray.length; i2++) {
            int type2 = Character.getType(toCharArray[i2]);
            if (type2 != type) {
                if (z && type2 == 2 && type == 1) {
                    type = i2 - 1;
                    if (type != i) {
                        arrayList.add(new String(toCharArray, i, type - i));
                        i = type;
                    }
                } else {
                    arrayList.add(new String(toCharArray, i, i2 - i));
                    i = i2;
                }
                type = type2;
            }
        }
        arrayList.add(new String(toCharArray, i, toCharArray.length - i));
        return (String[]) arrayList.toArray(new String[arrayList.size()]);
    }

    public static String[] splitByCharacterTypeCamelCase(String str) {
        return splitByCharacterType(str, true);
    }

    public static String underscore(String str) {
        return TextUtils.join(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR, splitByCharacterTypeCamelCase(str)).toLowerCase();
    }
}
