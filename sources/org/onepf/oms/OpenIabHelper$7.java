package org.onepf.oms;

import org.jetbrains.annotations.NotNull;
import org.onepf.oms.appstore.SkubitTestAppstore;

class OpenIabHelper$7 implements OpenIabHelper$AppstoreFactory {
    final /* synthetic */ OpenIabHelper this$0;

    OpenIabHelper$7(OpenIabHelper openIabHelper) {
        this.this$0 = openIabHelper;
    }

    @NotNull
    public Appstore get() {
        return new SkubitTestAppstore(OpenIabHelper.access$000(this.this$0));
    }
}
