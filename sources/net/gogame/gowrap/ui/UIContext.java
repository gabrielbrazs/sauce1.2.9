package net.gogame.gowrap.ui;

import android.app.Fragment;
import net.gogame.gowrap.support.DownloadManager;

public interface UIContext {
    void clearFragments();

    void enterFullscreen(Integer num);

    void exitFullscreen();

    DownloadManager getDownloadManager();

    String getGuid();

    boolean goBack();

    boolean isVipChatEnabled();

    void loadHtml(String str, String str2);

    void loadUrl(String str, boolean z);

    void onLoadingFinished();

    void onLoadingStarted();

    void pushFragment(Fragment fragment);
}
