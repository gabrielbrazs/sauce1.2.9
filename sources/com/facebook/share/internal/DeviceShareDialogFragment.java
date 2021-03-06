package com.facebook.share.internal;

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.text.Html;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import com.facebook.AccessToken;
import com.facebook.C0365R;
import com.facebook.FacebookRequestError;
import com.facebook.GraphRequest;
import com.facebook.GraphRequest.Callback;
import com.facebook.GraphResponse;
import com.facebook.HttpMethod;
import com.facebook.internal.Validate;
import com.facebook.share.model.ShareContent;
import com.facebook.share.model.ShareLinkContent;
import com.facebook.share.model.ShareOpenGraphContent;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.ScheduledThreadPoolExecutor;
import java.util.concurrent.TimeUnit;
import org.json.JSONException;
import org.json.JSONObject;

public class DeviceShareDialogFragment extends DialogFragment {
    private static final String DEVICE_SHARE_ENDPOINT = "device/share";
    private static final String REQUEST_STATE_KEY = "request_state";
    public static final String TAG = "DeviceShareDialogFragment";
    private static ScheduledThreadPoolExecutor backgroundExecutor;
    private volatile ScheduledFuture codeExpiredFuture;
    private TextView confirmationCode;
    private volatile RequestState currentRequestState;
    private Dialog dialog;
    private ProgressBar progressBar;
    private ShareContent shareContent;

    /* renamed from: com.facebook.share.internal.DeviceShareDialogFragment$1 */
    class C04711 implements OnClickListener {
        C04711() {
        }

        public void onClick(View view) {
            DeviceShareDialogFragment.this.dialog.dismiss();
        }
    }

    /* renamed from: com.facebook.share.internal.DeviceShareDialogFragment$2 */
    class C04722 implements Callback {
        C04722() {
        }

        public void onCompleted(GraphResponse graphResponse) {
            FacebookRequestError error = graphResponse.getError();
            if (error != null) {
                DeviceShareDialogFragment.this.finishActivityWithError(error);
                return;
            }
            JSONObject jSONObject = graphResponse.getJSONObject();
            RequestState requestState = new RequestState();
            try {
                requestState.setUserCode(jSONObject.getString("user_code"));
                requestState.setExpiresIn(jSONObject.getLong(AccessToken.EXPIRES_IN_KEY));
                DeviceShareDialogFragment.this.setCurrentRequestState(requestState);
            } catch (JSONException e) {
                DeviceShareDialogFragment.this.finishActivityWithError(new FacebookRequestError(0, "", "Malformed server response"));
            }
        }
    }

    /* renamed from: com.facebook.share.internal.DeviceShareDialogFragment$3 */
    class C04733 implements Runnable {
        C04733() {
        }

        public void run() {
            DeviceShareDialogFragment.this.dialog.dismiss();
        }
    }

    private static class RequestState implements Parcelable {
        public static final Creator<RequestState> CREATOR = new C04741();
        private long expiresIn;
        private String userCode;

        /* renamed from: com.facebook.share.internal.DeviceShareDialogFragment$RequestState$1 */
        static final class C04741 implements Creator<RequestState> {
            C04741() {
            }

            public RequestState createFromParcel(Parcel parcel) {
                return new RequestState(parcel);
            }

            public RequestState[] newArray(int i) {
                return new RequestState[i];
            }
        }

        RequestState() {
        }

        protected RequestState(Parcel parcel) {
            this.userCode = parcel.readString();
            this.expiresIn = parcel.readLong();
        }

        public int describeContents() {
            return 0;
        }

        public long getExpiresIn() {
            return this.expiresIn;
        }

        public String getUserCode() {
            return this.userCode;
        }

        public void setExpiresIn(long j) {
            this.expiresIn = j;
        }

        public void setUserCode(String str) {
            this.userCode = str;
        }

        public void writeToParcel(Parcel parcel, int i) {
            parcel.writeString(this.userCode);
            parcel.writeLong(this.expiresIn);
        }
    }

    private void detach() {
        if (isAdded()) {
            getFragmentManager().beginTransaction().remove(this).commit();
        }
    }

    private void finishActivity(int i, Intent intent) {
        if (isAdded()) {
            Activity activity = getActivity();
            activity.setResult(i, intent);
            activity.finish();
        }
    }

    private void finishActivityWithError(FacebookRequestError facebookRequestError) {
        detach();
        Intent intent = new Intent();
        intent.putExtra("error", facebookRequestError);
        finishActivity(-1, intent);
    }

    private static ScheduledThreadPoolExecutor getBackgroundExecutor() {
        synchronized (DeviceShareDialogFragment.class) {
            Class cls;
            try {
                if (backgroundExecutor == null) {
                    cls = true;
                    backgroundExecutor = new ScheduledThreadPoolExecutor(1);
                }
                ScheduledThreadPoolExecutor scheduledThreadPoolExecutor = backgroundExecutor;
                return scheduledThreadPoolExecutor;
            } finally {
                cls = DeviceShareDialogFragment.class;
            }
        }
    }

    private Bundle getGraphParametersForShareContent() {
        ShareContent shareContent = this.shareContent;
        if (shareContent != null) {
            if (shareContent instanceof ShareLinkContent) {
                return WebDialogParameters.create((ShareLinkContent) shareContent);
            }
            if (shareContent instanceof ShareOpenGraphContent) {
                return WebDialogParameters.create((ShareOpenGraphContent) shareContent);
            }
        }
        return null;
    }

    private void setCurrentRequestState(RequestState requestState) {
        this.currentRequestState = requestState;
        this.confirmationCode.setText(requestState.getUserCode());
        this.confirmationCode.setVisibility(0);
        this.progressBar.setVisibility(8);
        this.codeExpiredFuture = getBackgroundExecutor().schedule(new C04733(), requestState.getExpiresIn(), TimeUnit.SECONDS);
    }

    private void startShare() {
        Bundle graphParametersForShareContent = getGraphParametersForShareContent();
        if (graphParametersForShareContent == null || graphParametersForShareContent.size() == 0) {
            finishActivityWithError(new FacebookRequestError(0, "", "Failed to get share content"));
        }
        graphParametersForShareContent.putString("access_token", Validate.hasAppID() + "|" + Validate.hasClientToken());
        new GraphRequest(null, DEVICE_SHARE_ENDPOINT, graphParametersForShareContent, HttpMethod.POST, new C04722()).executeAsync();
    }

    @NonNull
    public Dialog onCreateDialog(Bundle bundle) {
        this.dialog = new Dialog(getActivity(), C0365R.style.com_facebook_auth_dialog);
        View inflate = getActivity().getLayoutInflater().inflate(C0365R.layout.com_facebook_device_auth_dialog_fragment, null);
        this.progressBar = (ProgressBar) inflate.findViewById(C0365R.id.progress_bar);
        this.confirmationCode = (TextView) inflate.findViewById(C0365R.id.confirmation_code);
        ((Button) inflate.findViewById(C0365R.id.cancel_button)).setOnClickListener(new C04711());
        ((TextView) inflate.findViewById(C0365R.id.com_facebook_device_auth_instructions)).setText(Html.fromHtml(getString(C0365R.string.com_facebook_device_auth_instructions)));
        ((TextView) inflate.findViewById(C0365R.id.com_facebook_device_dialog_title)).setText(getString(C0365R.string.com_facebook_share_button_text));
        this.dialog.setContentView(inflate);
        startShare();
        return this.dialog;
    }

    @Nullable
    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View onCreateView = super.onCreateView(layoutInflater, viewGroup, bundle);
        if (bundle != null) {
            RequestState requestState = (RequestState) bundle.getParcelable(REQUEST_STATE_KEY);
            if (requestState != null) {
                setCurrentRequestState(requestState);
            }
        }
        return onCreateView;
    }

    public void onDismiss(DialogInterface dialogInterface) {
        super.onDismiss(dialogInterface);
        if (this.codeExpiredFuture != null) {
            this.codeExpiredFuture.cancel(true);
        }
        finishActivity(-1, new Intent());
    }

    public void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        if (this.currentRequestState != null) {
            bundle.putParcelable(REQUEST_STATE_KEY, this.currentRequestState);
        }
    }

    public void setShareContent(ShareContent shareContent) {
        this.shareContent = shareContent;
    }
}
