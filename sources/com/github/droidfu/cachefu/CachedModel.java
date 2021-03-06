package com.github.droidfu.cachefu;

import android.os.Parcel;
import android.os.Parcelable;
import java.io.IOException;

public abstract class CachedModel implements Parcelable {
    private String id;
    private long transactionId = Long.MIN_VALUE;

    public CachedModel(Parcel parcel) throws IOException {
        readFromParcel(parcel);
    }

    public CachedModel(String str) {
        this.id = str;
    }

    public static CachedModel find(ModelCache modelCache, String str, Class<? extends CachedModel> cls) {
        try {
            CachedModel cachedModel = (CachedModel) cls.newInstance();
            cachedModel.setId(str);
            return cachedModel.reload(modelCache) ? cachedModel : null;
        } catch (Exception e) {
            return null;
        }
    }

    public abstract String createKey(String str);

    public int describeContents() {
        return 0;
    }

    public String getId() {
        return this.id;
    }

    public String getKey() {
        return this.id == null ? null : createKey(this.id);
    }

    public void readFromParcel(Parcel parcel) throws IOException {
        this.id = parcel.readString();
        this.transactionId = parcel.readLong();
    }

    public boolean reload(ModelCache modelCache) {
        String key = getKey();
        if (!(modelCache == null || key == null)) {
            CachedModel cachedModel = (CachedModel) modelCache.get(key);
            if (cachedModel != null && cachedModel.transactionId > this.transactionId) {
                reloadFromCachedModel(modelCache, cachedModel);
                return true;
            }
        }
        return false;
    }

    public abstract boolean reloadFromCachedModel(ModelCache modelCache, CachedModel cachedModel);

    public boolean save(ModelCache modelCache) {
        return save(modelCache, getKey());
    }

    protected boolean save(ModelCache modelCache, String str) {
        if (modelCache == null || str == null) {
            return false;
        }
        modelCache.put(str, this);
        return true;
    }

    public void setId(String str) {
        this.id = str;
    }

    void setTransactionId(long j) {
        this.transactionId = j;
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.id);
        parcel.writeLong(this.transactionId);
    }
}
