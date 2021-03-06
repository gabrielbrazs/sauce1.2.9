package com.google.android.gms.internal;

import com.google.android.gms.common.data.BitmapTeleporter;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.zzb;
import com.google.android.gms.drive.metadata.internal.zzi;
import com.google.android.gms.drive.metadata.internal.zzo;
import com.google.android.gms.drive.metadata.internal.zzs;
import com.google.android.gms.drive.metadata.internal.zzt;
import com.google.android.gms.drive.metadata.internal.zzu;
import java.util.Collections;

public final class zzbnr {
    public static final MetadataField<DriveId> zzgkt = zzbol.zzgmr;
    public static final MetadataField<String> zzgku = new zzt("alternateLink", 4300000);
    public static final zzbnu zzgkv = new zzbnu(5000000);
    public static final MetadataField<String> zzgkw = new zzt("description", 4300000);
    public static final MetadataField<String> zzgkx = new zzt("embedLink", 4300000);
    public static final MetadataField<String> zzgky = new zzt("fileExtension", 4300000);
    public static final MetadataField<Long> zzgkz = new zzi("fileSize", 4300000);
    public static final MetadataField<String> zzgla = new zzt("folderColorRgb", 7500000);
    public static final MetadataField<Boolean> zzglb = new zzb("hasThumbnail", 4300000);
    public static final MetadataField<String> zzglc = new zzt("indexableText", 4300000);
    public static final MetadataField<Boolean> zzgld = new zzb("isAppData", 4300000);
    public static final MetadataField<Boolean> zzgle = new zzb("isCopyable", 4300000);
    public static final MetadataField<Boolean> zzglf = new zzb("isEditable", 4100000);
    public static final MetadataField<Boolean> zzglg = new zzbns("isExplicitlyTrashed", Collections.singleton("trashed"), Collections.emptySet(), 7000000);
    public static final MetadataField<Boolean> zzglh = new zzb("isLocalContentUpToDate", 7800000);
    public static final zzbnv zzgli = new zzbnv("isPinned", 4100000);
    public static final MetadataField<Boolean> zzglj = new zzb("isOpenable", 7200000);
    public static final MetadataField<Boolean> zzglk = new zzb("isRestricted", 4300000);
    public static final MetadataField<Boolean> zzgll = new zzb("isShared", 4300000);
    public static final MetadataField<Boolean> zzglm = new zzb("isGooglePhotosFolder", 7000000);
    public static final MetadataField<Boolean> zzgln = new zzb("isGooglePhotosRootFolder", 7000000);
    public static final MetadataField<Boolean> zzglo = new zzb("isTrashable", 4400000);
    public static final MetadataField<Boolean> zzglp = new zzb("isViewed", 4300000);
    public static final zzbnw zzglq = new zzbnw(4100000);
    public static final MetadataField<String> zzglr = new zzt("originalFilename", 4300000);
    public static final com.google.android.gms.drive.metadata.zzb<String> zzgls = new zzs("ownerNames", 4300000);
    public static final zzu zzglt = new zzu("lastModifyingUser", 6000000);
    public static final zzu zzglu = new zzu("sharingUser", 6000000);
    public static final zzo zzglv = new zzo(4100000);
    public static final zzbnx zzglw = new zzbnx("quotaBytesUsed", 4300000);
    public static final zzbnz zzglx = new zzbnz("starred", 4100000);
    public static final MetadataField<BitmapTeleporter> zzgly = new zzbnt("thumbnail", Collections.emptySet(), Collections.emptySet(), 4400000);
    public static final zzboa zzglz = new zzboa("title", 4100000);
    public static final zzbob zzgma = new zzbob("trashed", 4100000);
    public static final MetadataField<String> zzgmb = new zzt("webContentLink", 4300000);
    public static final MetadataField<String> zzgmc = new zzt("webViewLink", 4300000);
    public static final MetadataField<String> zzgmd = new zzt("uniqueIdentifier", 5000000);
    public static final zzb zzgme = new zzb("writersCanShare", 6000000);
    public static final MetadataField<String> zzgmf = new zzt("role", 6000000);
    public static final MetadataField<String> zzgmg = new zzt("md5Checksum", 7000000);
    public static final zzbny zzgmh = new zzbny(7000000);
    public static final MetadataField<String> zzgmi = new zzt("recencyReason", 8000000);
    public static final MetadataField<Boolean> zzgmj = new zzb("subscribed", 8000000);
}
