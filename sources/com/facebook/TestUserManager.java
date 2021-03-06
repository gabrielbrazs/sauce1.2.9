package com.facebook;

import android.os.Bundle;
import android.text.TextUtils;
import android.util.Log;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import com.facebook.share.internal.ShareConstants;
import java.util.Arrays;
import java.util.Collection;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.json.JSONArray;
import org.json.JSONObject;

public class TestUserManager {
    static final /* synthetic */ boolean $assertionsDisabled = (!TestUserManager.class.desiredAssertionStatus());
    private static final String LOG_TAG = "TestUserManager";
    private Map<String, JSONObject> appTestAccounts;
    private String testApplicationId;
    private String testApplicationSecret;

    private enum Mode {
        PRIVATE,
        SHARED
    }

    public TestUserManager(String str, String str2) {
        if (Utility.isNullOrEmpty(str2) || Utility.isNullOrEmpty(str)) {
            throw new FacebookException("Must provide app ID and secret");
        }
        this.testApplicationSecret = str;
        this.testApplicationId = str2;
    }

    private JSONObject createTestAccount(List<String> list, Mode mode, String str) {
        Bundle bundle = new Bundle();
        bundle.putString("installed", ServerProtocol.DIALOG_RETURN_SCOPES_TRUE);
        bundle.putString(NativeProtocol.RESULT_ARGS_PERMISSIONS, getPermissionsString(list));
        bundle.putString("access_token", getAppAccessToken());
        if (mode == Mode.SHARED) {
            bundle.putString("name", String.format("Shared %s Testuser", new Object[]{getSharedTestAccountIdentifier(list, str)}));
        }
        GraphResponse executeAndWait = new GraphRequest(null, String.format("%s/accounts/test-users", new Object[]{this.testApplicationId}), bundle, HttpMethod.POST).executeAndWait();
        FacebookRequestError error = executeAndWait.getError();
        JSONObject jSONObject = executeAndWait.getJSONObject();
        if (error != null) {
            return null;
        }
        if (!$assertionsDisabled && jSONObject == null) {
            throw new AssertionError();
        } else if (mode != Mode.SHARED) {
            return jSONObject;
        } else {
            try {
                jSONObject.put("name", bundle.getString("name"));
            } catch (Throwable e) {
                Log.e(LOG_TAG, "Could not set name", e);
            }
            storeTestAccount(jSONObject);
            return jSONObject;
        }
    }

    private JSONObject findOrCreateSharedTestAccount(List<String> list, Mode mode, String str) {
        JSONObject findTestAccountMatchingIdentifier = findTestAccountMatchingIdentifier(getSharedTestAccountIdentifier(list, str));
        return findTestAccountMatchingIdentifier != null ? findTestAccountMatchingIdentifier : createTestAccount(list, mode, str);
    }

    private JSONObject findTestAccountMatchingIdentifier(String str) {
        JSONObject jSONObject;
        synchronized (this) {
            for (JSONObject jSONObject2 : this.appTestAccounts.values()) {
                if (jSONObject2.optString("name").contains(str)) {
                    break;
                }
            }
            jSONObject2 = null;
        }
        return jSONObject2;
    }

    private AccessToken getAccessTokenForUser(List<String> list, Mode mode, String str) {
        Object asList;
        retrieveTestAccountsForAppIfNeeded();
        if (Utility.isNullOrEmpty((Collection) list)) {
            asList = Arrays.asList(new String[]{"email", "publish_actions"});
        } else {
            List<String> list2 = list;
        }
        JSONObject createTestAccount = mode == Mode.PRIVATE ? createTestAccount(asList, mode, str) : findOrCreateSharedTestAccount(asList, mode, str);
        return new AccessToken(createTestAccount.optString("access_token"), this.testApplicationId, createTestAccount.optString("id"), asList, null, AccessTokenSource.TEST_USER, null, null);
    }

    private String getPermissionsString(List<String> list) {
        return TextUtils.join(",", list);
    }

    private String getSharedTestAccountIdentifier(List<String> list, String str) {
        return validNameStringFromInteger((str != null ? ((long) str.hashCode()) & 4294967295L : 0) ^ (((long) getPermissionsString(list).hashCode()) & 4294967295L));
    }

    private void populateTestAccounts(JSONArray jSONArray, JSONObject jSONObject) {
        synchronized (this) {
            for (int i = 0; i < jSONArray.length(); i++) {
                JSONObject optJSONObject = jSONArray.optJSONObject(i);
                try {
                    optJSONObject.put("name", jSONObject.optJSONObject(optJSONObject.optString("id")).optString("name"));
                } catch (Throwable e) {
                    Log.e(LOG_TAG, "Could not set name", e);
                }
                storeTestAccount(optJSONObject);
            }
        }
    }

    private void retrieveTestAccountsForAppIfNeeded() {
        synchronized (this) {
            if (this.appTestAccounts == null) {
                this.appTestAccounts = new HashMap();
                GraphRequest.setDefaultBatchApplicationId(this.testApplicationId);
                Bundle bundle = new Bundle();
                bundle.putString("access_token", getAppAccessToken());
                GraphRequest graphRequest = new GraphRequest(null, "app/accounts/test-users", bundle, null);
                graphRequest.setBatchEntryName("testUsers");
                graphRequest.setBatchEntryOmitResultOnSuccess(false);
                bundle = new Bundle();
                bundle.putString("access_token", getAppAccessToken());
                bundle.putString("ids", "{result=testUsers:$.data.*.id}");
                bundle.putString(GraphRequest.FIELDS_PARAM, "name");
                new GraphRequest(null, "", bundle, null).setBatchEntryDependsOn("testUsers");
                List executeBatchAndWait = GraphRequest.executeBatchAndWait(graphRequest, r2);
                if (executeBatchAndWait == null || executeBatchAndWait.size() != 2) {
                    throw new FacebookException("Unexpected number of results from TestUsers batch query");
                }
                populateTestAccounts(((GraphResponse) executeBatchAndWait.get(0)).getJSONObject().optJSONArray(ShareConstants.WEB_DIALOG_PARAM_DATA), ((GraphResponse) executeBatchAndWait.get(1)).getJSONObject());
            }
        }
    }

    private void storeTestAccount(JSONObject jSONObject) {
        synchronized (this) {
            this.appTestAccounts.put(jSONObject.optString("id"), jSONObject);
        }
    }

    private String validNameStringFromInteger(long j) {
        String l = Long.toString(j);
        StringBuilder stringBuilder = new StringBuilder("Perm");
        char[] toCharArray = l.toCharArray();
        int length = toCharArray.length;
        int i = 0;
        int i2 = 0;
        while (i2 < length) {
            int i3 = toCharArray[i2];
            if (i3 == i) {
                i3 = (char) (i3 + 10);
            }
            stringBuilder.append((char) ((i3 + 97) - 48));
            i2++;
            i = i3;
        }
        return stringBuilder.toString();
    }

    public AccessToken getAccessTokenForPrivateUser(List<String> list) {
        return getAccessTokenForUser(list, Mode.PRIVATE, null);
    }

    public AccessToken getAccessTokenForSharedUser(List<String> list) {
        return getAccessTokenForSharedUser(list, null);
    }

    public AccessToken getAccessTokenForSharedUser(List<String> list, String str) {
        return getAccessTokenForUser(list, Mode.SHARED, str);
    }

    final String getAppAccessToken() {
        return this.testApplicationId + "|" + this.testApplicationSecret;
    }

    public String getTestApplicationId() {
        String str;
        synchronized (this) {
            str = this.testApplicationId;
        }
        return str;
    }

    public String getTestApplicationSecret() {
        String str;
        synchronized (this) {
            str = this.testApplicationSecret;
        }
        return str;
    }
}
