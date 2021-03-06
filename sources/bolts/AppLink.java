package bolts;

import android.net.Uri;
import java.util.Collections;
import java.util.List;

public class AppLink {
    private Uri sourceUrl;
    private List<Target> targets;
    private Uri webUrl;

    public static class Target {
        private final String appName;
        private final String className;
        private final String packageName;
        private final Uri url;

        public Target(String str, String str2, Uri uri, String str3) {
            this.packageName = str;
            this.className = str2;
            this.url = uri;
            this.appName = str3;
        }

        public String getAppName() {
            return this.appName;
        }

        public String getClassName() {
            return this.className;
        }

        public String getPackageName() {
            return this.packageName;
        }

        public Uri getUrl() {
            return this.url;
        }
    }

    public AppLink(Uri uri, List<Target> list, Uri uri2) {
        List emptyList;
        this.sourceUrl = uri;
        if (list == null) {
            emptyList = Collections.emptyList();
        }
        this.targets = emptyList;
        this.webUrl = uri2;
    }

    public Uri getSourceUrl() {
        return this.sourceUrl;
    }

    public List<Target> getTargets() {
        return Collections.unmodifiableList(this.targets);
    }

    public Uri getWebUrl() {
        return this.webUrl;
    }
}
