package com.google.android.gms.games.achievement;

import android.database.CharArrayBuffer;
import android.net.Uri;
import android.os.Parcel;
import com.facebook.internal.ServerProtocol;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.common.data.zzc;
import com.google.android.gms.games.Player;
import com.google.android.gms.games.PlayerRef;

public final class AchievementRef extends zzc implements Achievement {
    AchievementRef(DataHolder dataHolder, int i) {
        super(dataHolder, i);
    }

    public final int describeContents() {
        return 0;
    }

    public final /* synthetic */ Object freeze() {
        return new AchievementEntity(this);
    }

    public final String getAchievementId() {
        return getString("external_achievement_id");
    }

    public final int getCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return getInteger("current_steps");
    }

    public final String getDescription() {
        return getString("description");
    }

    public final void getDescription(CharArrayBuffer charArrayBuffer) {
        zza("description", charArrayBuffer);
    }

    public final String getFormattedCurrentSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return getString("formatted_current_steps");
    }

    public final void getFormattedCurrentSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        zza("formatted_current_steps", charArrayBuffer);
    }

    public final String getFormattedTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return getString("formatted_total_steps");
    }

    public final void getFormattedTotalSteps(CharArrayBuffer charArrayBuffer) {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        zza("formatted_total_steps", charArrayBuffer);
    }

    public final long getLastUpdatedTimestamp() {
        return getLong("last_updated_timestamp");
    }

    public final String getName() {
        return getString("name");
    }

    public final void getName(CharArrayBuffer charArrayBuffer) {
        zza("name", charArrayBuffer);
    }

    public final Player getPlayer() {
        return new PlayerRef(this.zzfkz, this.zzfqb);
    }

    public final Uri getRevealedImageUri() {
        return zzfu("revealed_icon_image_uri");
    }

    public final String getRevealedImageUrl() {
        return getString("revealed_icon_image_url");
    }

    public final int getState() {
        return getInteger(ServerProtocol.DIALOG_PARAM_STATE);
    }

    public final int getTotalSteps() {
        boolean z = true;
        if (getType() != 1) {
            z = false;
        }
        com.google.android.gms.common.internal.zzc.zzbg(z);
        return getInteger("total_steps");
    }

    public final int getType() {
        return getInteger(ShareConstants.MEDIA_TYPE);
    }

    public final Uri getUnlockedImageUri() {
        return zzfu("unlocked_icon_image_uri");
    }

    public final String getUnlockedImageUrl() {
        return getString("unlocked_icon_image_url");
    }

    public final long getXpValue() {
        return (!zzft("instance_xp_value") || zzfv("instance_xp_value")) ? getLong("definition_xp_value") : getLong("instance_xp_value");
    }

    public final String toString() {
        return AchievementEntity.zza(this);
    }

    public final void writeToParcel(Parcel parcel, int i) {
        ((AchievementEntity) ((Achievement) freeze())).writeToParcel(parcel, i);
    }
}
