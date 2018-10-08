package com.amazon.device.iap.model;

import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import com.amazon.device.iap.internal.util.C0243d;
import java.util.UUID;
import org.json.JSONException;
import org.json.JSONObject;

public final class RequestId implements Parcelable {
    public static final Creator<RequestId> CREATOR = new C02461();
    private static final String ENCODED_ID = "encodedId";
    private final String encodedId;

    /* renamed from: com.amazon.device.iap.model.RequestId$1 */
    static final class C02461 implements Creator<RequestId> {
        C02461() {
        }

        public RequestId createFromParcel(Parcel parcel) {
            return new RequestId(parcel);
        }

        public RequestId[] newArray(int i) {
            return new RequestId[i];
        }
    }

    public RequestId() {
        this.encodedId = UUID.randomUUID().toString();
    }

    private RequestId(Parcel parcel) {
        this.encodedId = parcel.readString();
    }

    private RequestId(String str) {
        C0243d.m169a((Object) str, ENCODED_ID);
        this.encodedId = str;
    }

    public static RequestId fromString(String str) {
        return new RequestId(str);
    }

    public int describeContents() {
        return 0;
    }

    public boolean equals(Object obj) {
        return (obj == null || getClass() != obj.getClass()) ? false : this.encodedId.equals(((RequestId) obj).encodedId);
    }

    public int hashCode() {
        return (this.encodedId == null ? 0 : this.encodedId.hashCode()) + 31;
    }

    public JSONObject toJSON() {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put(ENCODED_ID, this.encodedId);
        } catch (JSONException e) {
        }
        return jSONObject;
    }

    public String toString() {
        return this.encodedId;
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.encodedId);
    }
}
