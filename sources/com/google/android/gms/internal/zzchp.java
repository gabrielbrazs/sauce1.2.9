package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import com.google.android.gms.common.api.Api.ApiOptions.NoOptions;
import com.google.android.gms.common.api.Api.zza;
import com.google.android.gms.common.api.Api.zzf;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.PendingResult;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.nearby.connection.AdvertisingOptions;
import com.google.android.gms.nearby.connection.AppMetadata;
import com.google.android.gms.nearby.connection.ConnectionLifecycleCallback;
import com.google.android.gms.nearby.connection.Connections;
import com.google.android.gms.nearby.connection.Connections.ConnectionRequestListener;
import com.google.android.gms.nearby.connection.Connections.ConnectionResponseCallback;
import com.google.android.gms.nearby.connection.Connections.EndpointDiscoveryListener;
import com.google.android.gms.nearby.connection.Connections.MessageListener;
import com.google.android.gms.nearby.connection.Connections.StartAdvertisingResult;
import com.google.android.gms.nearby.connection.DiscoveryOptions;
import com.google.android.gms.nearby.connection.EndpointDiscoveryCallback;
import com.google.android.gms.nearby.connection.Payload;
import com.google.android.gms.nearby.connection.PayloadCallback;
import java.util.List;

public final class zzchp implements Connections {
    public static final zzf<zzcgp> zzdwq = new zzf();
    public static final zza<zzcgp, NoOptions> zzdwr = new zzchq();

    public final PendingResult<Status> acceptConnection(GoogleApiClient googleApiClient, String str, PayloadCallback payloadCallback) {
        return googleApiClient.zze(new zzcig(this, googleApiClient, str, googleApiClient.zzp(payloadCallback)));
    }

    @Deprecated
    public final PendingResult<Status> acceptConnectionRequest(GoogleApiClient googleApiClient, String str, byte[] bArr, MessageListener messageListener) {
        return googleApiClient.zze(new zzchx(this, googleApiClient, str, bArr, googleApiClient.zzp(messageListener)));
    }

    public final void disconnectFromEndpoint(GoogleApiClient googleApiClient, String str) {
        googleApiClient.zze(new zzchs(this, googleApiClient, str));
    }

    public final PendingResult<Status> rejectConnection(GoogleApiClient googleApiClient, String str) {
        return googleApiClient.zze(new zzcih(this, googleApiClient, str));
    }

    @Deprecated
    public final PendingResult<Status> rejectConnectionRequest(GoogleApiClient googleApiClient, String str) {
        return googleApiClient.zze(new zzchy(this, googleApiClient, str));
    }

    public final PendingResult<Status> requestConnection(GoogleApiClient googleApiClient, @Nullable String str, String str2, ConnectionLifecycleCallback connectionLifecycleCallback) {
        return googleApiClient.zze(new zzcif(this, googleApiClient, str, str2, googleApiClient.zzp(connectionLifecycleCallback)));
    }

    @Deprecated
    public final PendingResult<Status> sendConnectionRequest(GoogleApiClient googleApiClient, String str, String str2, byte[] bArr, ConnectionResponseCallback connectionResponseCallback, MessageListener messageListener) {
        return googleApiClient.zze(new zzchw(this, googleApiClient, str, str2, bArr, googleApiClient.zzp(connectionResponseCallback), googleApiClient.zzp(messageListener)));
    }

    public final PendingResult<Status> sendPayload(GoogleApiClient googleApiClient, String str, Payload payload) {
        return googleApiClient.zze(new zzcii(this, googleApiClient, str, payload));
    }

    public final PendingResult<Status> sendPayload(GoogleApiClient googleApiClient, List<String> list, Payload payload) {
        return googleApiClient.zze(new zzchr(this, googleApiClient, list, payload));
    }

    @Deprecated
    public final void sendReliableMessage(GoogleApiClient googleApiClient, String str, byte[] bArr) {
        googleApiClient.zze(new zzchz(this, googleApiClient, str, bArr));
    }

    @Deprecated
    public final void sendReliableMessage(GoogleApiClient googleApiClient, List<String> list, byte[] bArr) {
        googleApiClient.zze(new zzcia(this, googleApiClient, list, bArr));
    }

    @Deprecated
    public final void sendUnreliableMessage(GoogleApiClient googleApiClient, String str, byte[] bArr) {
        sendPayload(googleApiClient, str, Payload.fromBytes(bArr));
    }

    @Deprecated
    public final void sendUnreliableMessage(GoogleApiClient googleApiClient, List<String> list, byte[] bArr) {
        sendPayload(googleApiClient, (List) list, Payload.fromBytes(bArr));
    }

    @Deprecated
    public final PendingResult<StartAdvertisingResult> startAdvertising(GoogleApiClient googleApiClient, String str, AppMetadata appMetadata, long j, ConnectionRequestListener connectionRequestListener) {
        return googleApiClient.zze(new zzchu(this, googleApiClient, str, j, googleApiClient.zzp(connectionRequestListener)));
    }

    public final PendingResult<StartAdvertisingResult> startAdvertising(GoogleApiClient googleApiClient, String str, String str2, ConnectionLifecycleCallback connectionLifecycleCallback, AdvertisingOptions advertisingOptions) {
        return googleApiClient.zze(new zzcib(this, googleApiClient, str, str2, googleApiClient.zzp(connectionLifecycleCallback), advertisingOptions));
    }

    @Deprecated
    public final PendingResult<Status> startDiscovery(GoogleApiClient googleApiClient, String str, long j, EndpointDiscoveryListener endpointDiscoveryListener) {
        return googleApiClient.zze(new zzchv(this, googleApiClient, str, j, googleApiClient.zzp(endpointDiscoveryListener)));
    }

    public final PendingResult<Status> startDiscovery(GoogleApiClient googleApiClient, String str, EndpointDiscoveryCallback endpointDiscoveryCallback, DiscoveryOptions discoveryOptions) {
        return googleApiClient.zze(new zzcid(this, googleApiClient, str, googleApiClient.zzp(endpointDiscoveryCallback), discoveryOptions));
    }

    public final void stopAdvertising(GoogleApiClient googleApiClient) {
        googleApiClient.zze(new zzcic(this, googleApiClient));
    }

    public final void stopAllEndpoints(GoogleApiClient googleApiClient) {
        googleApiClient.zze(new zzcht(this, googleApiClient));
    }

    public final void stopDiscovery(GoogleApiClient googleApiClient) {
        googleApiClient.zze(new zzcie(this, googleApiClient));
    }

    @Deprecated
    public final void stopDiscovery(GoogleApiClient googleApiClient, String str) {
        stopDiscovery(googleApiClient);
    }
}
