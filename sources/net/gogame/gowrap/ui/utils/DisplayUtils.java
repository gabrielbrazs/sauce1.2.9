package net.gogame.gowrap.ui.utils;

import android.app.Activity;
import android.content.Context;
import android.graphics.Point;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.LevelListDrawable;
import android.os.Build.VERSION;
import android.util.TypedValue;
import android.view.Display;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;

public final class DisplayUtils {
    private DisplayUtils() {
    }

    public static float dpFromPx(Context context, float f) {
        return f / context.getResources().getDisplayMetrics().density;
    }

    public static int pxFromDp(Context context, float f) {
        return Math.round(context.getResources().getDisplayMetrics().density * f);
    }

    public static int pxFromSp(Context context, float f) {
        return Math.round(TypedValue.applyDimension(2, f, context.getResources().getDisplayMetrics()));
    }

    public static int getBaseScreenOrientation(Activity activity) {
        return getBaseScreenOrientation(getScreenOrientation(activity));
    }

    public static int getBaseScreenOrientation(int i) {
        switch (i) {
            case 0:
            case 6:
            case 8:
            case 11:
                return 0;
            case 1:
            case 7:
            case 9:
            case 12:
                return 1;
            default:
                return -1;
        }
    }

    public static int getScreenOrientation(Activity activity) {
        int i = 1;
        if (VERSION.SDK_INT >= 13) {
            int i2;
            Display defaultDisplay = activity.getWindowManager().getDefaultDisplay();
            int rotation = defaultDisplay.getRotation();
            Point point = new Point();
            defaultDisplay.getSize(point);
            int i3 = point.x > point.y ? 1 : 0;
            if (rotation == 1 || rotation == 3) {
                i2 = 1;
            } else {
                i2 = 0;
            }
            if (i2 != 0) {
                if (i3 == 0) {
                    return rotation == 1 ? 9 : 1;
                } else {
                    if (rotation == 1) {
                        return 0;
                    }
                    return 8;
                }
            } else if (i3 == 0) {
                if (rotation != 0) {
                    i = 9;
                }
                return i;
            } else if (rotation != 0) {
                return 8;
            } else {
                return 0;
            }
        } else if (activity.getResources().getConfiguration().orientation != 2) {
            return 1;
        } else {
            return 0;
        }
    }

    public static void lockOrientation(Activity activity) {
        activity.setRequestedOrientation(getScreenOrientation(activity));
    }

    public static void hideSoftKeyboard(Activity activity) {
        View currentFocus = activity.getCurrentFocus();
        if (currentFocus != null) {
            InputMethodManager inputMethodManager = (InputMethodManager) activity.getSystemService("input_method");
            if (inputMethodManager != null) {
                inputMethodManager.hideSoftInputFromWindow(currentFocus.getWindowToken(), 0);
            }
        }
    }

    public static void showSoftKeyboard(Activity activity, EditText editText) {
        InputMethodManager inputMethodManager = (InputMethodManager) activity.getSystemService("input_method");
        if (inputMethodManager != null) {
            inputMethodManager.showSoftInput(editText, 1);
        }
    }

    public static void setLevel(Drawable drawable, int i) {
        if (drawable instanceof LevelListDrawable) {
            ((LevelListDrawable) drawable).setLevel(i);
        }
    }
}
