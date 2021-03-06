package com.amazon.device.iap.internal.util;

import com.amazon.device.iap.internal.p001b.C0197e;
import com.amazon.device.iap.internal.p001b.p009h.C0228a;
import com.amazon.device.iap.model.RequestId;
import org.json.JSONObject;

public class MetricsHelper {
    private static final String DESCRIPTION = "description";
    private static final String EXCEPTION_MESSAGE = "exceptionMessage";
    private static final String JSON_STRING = "jsonString";
    private static final String RECEIPT_VERIFICATION_FAILED_METRIC = "IapReceiptVerificationFailed";
    private static final String SIGNATURE = "signature";
    private static final String STRING_TO_SIGN = "stringToSign";
    private static final String TAG = MetricsHelper.class.getSimpleName();

    protected static void submitMetric(String str, String str2, JSONObject jSONObject) {
        new C0228a(new C0197e(RequestId.fromString(str)), str2, jSONObject.toString()).a_();
    }

    public static void submitReceiptVerificationFailureMetrics(String str, String str2, String str3) {
        try {
            JSONObject jSONObject = new JSONObject();
            jSONObject.put(STRING_TO_SIGN, str2);
            jSONObject.put(SIGNATURE, str3);
            submitMetric(str, RECEIPT_VERIFICATION_FAILED_METRIC, jSONObject);
        } catch (Exception e) {
            C0244e.m175b(TAG, "error calling submitMetric: " + e);
        }
    }
}
