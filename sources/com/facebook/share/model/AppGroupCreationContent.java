package com.facebook.share.model;

import android.os.Parcel;
import android.os.Parcelable.Creator;

public final class AppGroupCreationContent implements ShareModel {
    public static final Creator<AppGroupCreationContent> CREATOR = new C05081();
    private final String description;
    private final String name;
    private AppGroupPrivacy privacy;

    /* renamed from: com.facebook.share.model.AppGroupCreationContent$1 */
    static final class C05081 implements Creator<AppGroupCreationContent> {
        C05081() {
        }

        public AppGroupCreationContent createFromParcel(Parcel parcel) {
            return new AppGroupCreationContent(parcel);
        }

        public AppGroupCreationContent[] newArray(int i) {
            return new AppGroupCreationContent[i];
        }
    }

    public enum AppGroupPrivacy {
        Open,
        Closed
    }

    public static class Builder implements ShareModelBuilder<AppGroupCreationContent, Builder> {
        private String description;
        private String name;
        private AppGroupPrivacy privacy;

        public AppGroupCreationContent build() {
            return new AppGroupCreationContent();
        }

        public Builder readFrom(AppGroupCreationContent appGroupCreationContent) {
            return appGroupCreationContent == null ? this : setName(appGroupCreationContent.getName()).setDescription(appGroupCreationContent.getDescription()).setAppGroupPrivacy(appGroupCreationContent.getAppGroupPrivacy());
        }

        public Builder setAppGroupPrivacy(AppGroupPrivacy appGroupPrivacy) {
            this.privacy = appGroupPrivacy;
            return this;
        }

        public Builder setDescription(String str) {
            this.description = str;
            return this;
        }

        public Builder setName(String str) {
            this.name = str;
            return this;
        }
    }

    AppGroupCreationContent(Parcel parcel) {
        this.name = parcel.readString();
        this.description = parcel.readString();
        this.privacy = (AppGroupPrivacy) parcel.readSerializable();
    }

    private AppGroupCreationContent(Builder builder) {
        this.name = builder.name;
        this.description = builder.description;
        this.privacy = builder.privacy;
    }

    public int describeContents() {
        return 0;
    }

    public AppGroupPrivacy getAppGroupPrivacy() {
        return this.privacy;
    }

    public String getDescription() {
        return this.description;
    }

    public String getName() {
        return this.name;
    }

    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(this.name);
        parcel.writeString(this.description);
        parcel.writeSerializable(this.privacy);
    }
}
