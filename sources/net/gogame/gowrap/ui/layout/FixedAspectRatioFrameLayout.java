package net.gogame.gowrap.p019ui.layout;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.view.View.MeasureSpec;
import android.widget.FrameLayout;
import net.gogame.gowrap.p019ui.common.C1685R;

/* renamed from: net.gogame.gowrap.ui.layout.FixedAspectRatioFrameLayout */
public class FixedAspectRatioFrameLayout extends FrameLayout {
    private int mAspectRatioHeight;
    private int mAspectRatioWidth;
    private float maxHeight;

    public FixedAspectRatioFrameLayout(Context context) {
        super(context);
    }

    public FixedAspectRatioFrameLayout(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        init(context, attributeSet);
    }

    public FixedAspectRatioFrameLayout(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        init(context, attributeSet);
    }

    private void init(Context context, AttributeSet attributeSet) {
        TypedArray obtainStyledAttributes = context.obtainStyledAttributes(attributeSet, C1685R.styleable.FixedAspectRatioFrameLayout);
        this.mAspectRatioWidth = obtainStyledAttributes.getInt(C1685R.styleable.FixedAspectRatioFrameLayout_aspectRatioWidth, 4);
        this.mAspectRatioHeight = obtainStyledAttributes.getInt(C1685R.styleable.FixedAspectRatioFrameLayout_aspectRatioHeight, 3);
        this.maxHeight = obtainStyledAttributes.getDimension(C1685R.styleable.FixedAspectRatioFrameLayout_maxHeight, Float.MAX_VALUE);
        obtainStyledAttributes.recycle();
    }

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        int size = MeasureSpec.getSize(i);
        int size2 = MeasureSpec.getSize(i2);
        int i3 = (this.mAspectRatioHeight * size) / this.mAspectRatioWidth;
        if (i3 > size2) {
            size = (this.mAspectRatioWidth * size2) / this.mAspectRatioHeight;
        } else {
            size2 = i3;
        }
        if (((float) size2) > this.maxHeight) {
            size2 = (int) this.maxHeight;
            size = (this.mAspectRatioWidth * size2) / this.mAspectRatioHeight;
        }
        super.onMeasure(MeasureSpec.makeMeasureSpec(size, 1073741824), MeasureSpec.makeMeasureSpec(size2, 1073741824));
    }
}
