package com.github.droidfu.http;

import io.fabric.sdk.android.services.network.HttpRequest;
import java.util.HashMap;
import org.apache.http.HttpEntity;
import org.apache.http.HttpEntityEnclosingRequest;
import org.apache.http.impl.client.AbstractHttpClient;

class HttpPost extends BetterHttpRequestBase {
    HttpPost(AbstractHttpClient abstractHttpClient, String str, HashMap<String, String> hashMap) {
        super(abstractHttpClient);
        this.request = new org.apache.http.client.methods.HttpPost(str);
        for (String str2 : hashMap.keySet()) {
            this.request.setHeader(str2, (String) hashMap.get(str2));
        }
    }

    HttpPost(AbstractHttpClient abstractHttpClient, String str, HttpEntity httpEntity, HashMap<String, String> hashMap) {
        super(abstractHttpClient);
        this.request = new org.apache.http.client.methods.HttpPost(str);
        ((HttpEntityEnclosingRequest) this.request).setEntity(httpEntity);
        this.request.setHeader(HttpRequest.HEADER_CONTENT_TYPE, httpEntity.getContentType().getValue());
        for (String str2 : hashMap.keySet()) {
            this.request.setHeader(str2, (String) hashMap.get(str2));
        }
    }
}
