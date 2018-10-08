package net.gogame.gopay.sdk;

import net.gogame.gopay.sdk.iab.C1341a;

public class Country extends C1341a {
    public Country(String str, String str2, String str3) {
        super(str, str2, str3);
    }

    public String getCode() {
        return getId();
    }

    public String getName() {
        return getDisplayName();
    }
}
