package com.crashlytics.android.core;

import io.fabric.sdk.android.Fabric;
import java.io.File;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

class SessionReport implements Report {
    private final Map<String, String> customHeaders;
    private final File file;

    public SessionReport(File file) {
        this(file, Collections.emptyMap());
    }

    public SessionReport(File file, Map<String, String> map) {
        this.file = file;
        this.customHeaders = new HashMap(map);
        if (this.file.length() == 0) {
            this.customHeaders.putAll(ReportUploader.HEADER_INVALID_CLS_FILE);
        }
    }

    public Map<String, String> getCustomHeaders() {
        return Collections.unmodifiableMap(this.customHeaders);
    }

    public File getFile() {
        return this.file;
    }

    public String getFileName() {
        return getFile().getName();
    }

    public String getIdentifier() {
        String fileName = getFileName();
        return fileName.substring(0, fileName.lastIndexOf(46));
    }

    public boolean remove() {
        Fabric.getLogger().mo4289d("Fabric", "Removing report at " + this.file.getPath());
        return this.file.delete();
    }
}
