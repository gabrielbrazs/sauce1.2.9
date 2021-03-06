package net.gogame.chat;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Build.VERSION;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;
import com.squareup.picasso.Callback;
import com.squareup.picasso.Picasso;
import com.zopim.android.sdk.C0785R;
import java.util.List;
import net.gogame.chat.ChatContext.Rating;
import org.apache.commons.lang3.StringUtils;

public class ChatAdapterViewFactory {
    private final boolean allowRatingChange;
    private final Context context;
    private final UIContext uiContext;

    public interface Option {
        String getLabel();

        boolean isSelected();
    }

    public interface OptionListener {
        void onOptionSelected(Option option);
    }

    public interface RatingListener {
        void onRatingChanged(Rating rating);
    }

    public ChatAdapterViewFactory(Context context, UIContext uIContext, boolean z) {
        this.context = context;
        this.uiContext = uIContext;
        this.allowRatingChange = z;
    }

    private String getViewType(View view) {
        if (view == null || view.getTag() == null) {
            return null;
        }
        return String.valueOf(view.getTag());
    }

    private boolean viewIsOfType(View view, String str) {
        return StringUtils.equals(getViewType(view), str);
    }

    public View getEmptyView(View view, ViewGroup viewGroup) {
        if (viewIsOfType(view, "empty")) {
            return view;
        }
        return DisplayUtils.getLayoutInflater(this.context).inflate(C0785R.layout.net_gogame_chat_item_empty_layout, viewGroup, false);
    }

    public View getNotificationView(View view, ViewGroup viewGroup, String str) {
        return getNotificationView(view, viewGroup, str, false);
    }

    public View getNotificationView(View view, ViewGroup viewGroup, String str, boolean z) {
        if (!viewIsOfType(view, "notification")) {
            view = DisplayUtils.getLayoutInflater(this.context).inflate(C0785R.layout.net_gogame_chat_item_notification_layout, viewGroup, false);
        }
        TextView textView = (TextView) view.findViewById(C0785R.id.textView);
        textView.setText(str);
        if (z) {
            textView.setVisibility(8);
        } else {
            textView.setVisibility(0);
        }
        return view;
    }

    private View initAgentView(View view, ViewGroup viewGroup, boolean z, String str, String str2) {
        LayoutInflater layoutInflater = DisplayUtils.getLayoutInflater(this.context);
        if (!viewIsOfType(view, "agentMessage")) {
            view = layoutInflater.inflate(C0785R.layout.net_gogame_chat_item_agent_layout, viewGroup, false);
        }
        ImageView imageView = (RoundedImageView) view.findViewById(C0785R.id.profileIcon);
        TextView textView = (TextView) view.findViewById(C0785R.id.agentName);
        if (z) {
            imageView.setVisibility(0);
            textView.setVisibility(0);
            if (str2 != null) {
                Picasso.with(this.context).load(str2).placeholder(C0785R.drawable.net_gogame_chat_agent_profile_placeholder).into(imageView);
            }
            textView.setText(str);
        } else {
            imageView.setVisibility(4);
            textView.setVisibility(8);
        }
        return view;
    }

    public View getAgentMessageView(View view, ViewGroup viewGroup, boolean z, String str, String str2, String str3) {
        View initAgentView = initAgentView(view, viewGroup, z, str, str2);
        ((TextView) initAgentView.findViewById(C0785R.id.agentTextView)).setText(str3);
        ((LinearLayout) initAgentView.findViewById(C0785R.id.optionsLinearLayout)).setVisibility(8);
        return initAgentView;
    }

    public View getAgentAttachmentView(View view, ViewGroup viewGroup, boolean z, String str, String str2, final String str3, String str4) {
        if (str3 == null || str4 == null) {
            return getEmptyView(view, viewGroup);
        }
        View initAgentView = initAgentView(view, viewGroup, z, str, str2);
        ((TextView) initAgentView.findViewById(C0785R.id.agentTextView)).setVisibility(8);
        final ImageView imageView = (ImageView) initAgentView.findViewById(C0785R.id.cellImageView);
        imageView.setVisibility(0);
        Picasso.with(this.context).load(str4).resize(400, 400).centerCrop().onlyScaleDown().into(imageView, new Callback() {
            public void onSuccess() {
                Picasso.with(ChatAdapterViewFactory.this.context).load(str3).placeholder(imageView.getDrawable()).resize(400, 400).centerCrop().onlyScaleDown().into(imageView);
            }

            public void onError() {
            }
        });
        imageView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ChatAdapterViewFactory.this.uiContext.showImage(str3);
            }
        });
        return initAgentView;
    }

    public View getAgentOptionsView(View view, ViewGroup viewGroup, boolean z, String str, String str2, String str3, List<Option> list, OptionListener optionListener) {
        if (list == null || list.isEmpty()) {
            return getEmptyView(view, viewGroup);
        }
        LayoutInflater layoutInflater = DisplayUtils.getLayoutInflater(this.context);
        View initAgentView = initAgentView(view, viewGroup, z, str, str2);
        ((TextView) initAgentView.findViewById(C0785R.id.agentTextView)).setText(str3);
        LinearLayout linearLayout = (LinearLayout) initAgentView.findViewById(C0785R.id.optionsLinearLayout);
        linearLayout.setVisibility(0);
        Option option = null;
        for (Option option2 : list) {
            if (option2 != null && option2.isSelected()) {
                option = option2;
                break;
            }
        }
        if (option != null) {
            View inflate = layoutInflater.inflate(C0785R.layout.net_gogame_chat_item_option_sublayout, viewGroup, false);
            LinearLayout linearLayout2 = (LinearLayout) inflate.findViewById(C0785R.id.chatOption);
            if (VERSION.SDK_INT >= 16) {
                linearLayout2.setBackground(getDrawable(C0785R.drawable.net_gogame_chat_rounded_corner_for_visitor));
            } else {
                linearLayout2.setBackgroundDrawable(getDrawable(C0785R.drawable.net_gogame_chat_rounded_corner_for_visitor));
            }
            ((ImageView) inflate.findViewById(C0785R.id.optionBulletImageView)).setVisibility(8);
            TextView textView = (TextView) inflate.findViewById(C0785R.id.optionLabelTextView);
            textView.setText(option.getLabel());
            textView.setTextColor(-1);
            linearLayout.addView(inflate);
        } else {
            for (final Option option22 : list) {
                if (option22 != null) {
                    View inflate2 = layoutInflater.inflate(C0785R.layout.net_gogame_chat_item_option_sublayout, viewGroup, false);
                    TextView textView2 = (TextView) inflate2.findViewById(C0785R.id.optionLabelTextView);
                    textView2.setText(option22.getLabel());
                    final OptionListener optionListener2 = optionListener;
                    textView2.setOnClickListener(new OnClickListener() {
                        public void onClick(View view) {
                            optionListener2.onOptionSelected(option22);
                        }
                    });
                    linearLayout.addView(inflate2);
                }
            }
        }
        return initAgentView;
    }

    public View getVisitorMessageView(View view, ViewGroup viewGroup, String str) {
        if (!viewIsOfType(view, "visitorMessage")) {
            view = DisplayUtils.getLayoutInflater(this.context).inflate(C0785R.layout.net_gogame_chat_item_visitor_layout, viewGroup, false);
        }
        ((ImageView) view.findViewById(C0785R.id.attachmentThumbnailImageView)).setVisibility(8);
        TextView textView = (TextView) view.findViewById(C0785R.id.messageTextView);
        textView.setVisibility(0);
        textView.setText(str);
        return view;
    }

    public View getVisitorAttachmentView(View view, ViewGroup viewGroup, final Uri uri) {
        if (!viewIsOfType(view, "visitorMessage")) {
            view = DisplayUtils.getLayoutInflater(this.context).inflate(C0785R.layout.net_gogame_chat_item_visitor_layout, viewGroup, false);
        }
        TextView textView = (TextView) view.findViewById(C0785R.id.messageTextView);
        textView.setVisibility(8);
        textView.setText(null);
        ImageView imageView = (ImageView) view.findViewById(C0785R.id.attachmentThumbnailImageView);
        imageView.setVisibility(0);
        Picasso with = Picasso.with(this.context);
        with.setLoggingEnabled(true);
        with.load(uri).resize(400, 400).onlyScaleDown().centerCrop().into(imageView);
        imageView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ChatAdapterViewFactory.this.uiContext.showImage(uri.toString());
            }
        });
        return view;
    }

    public View getRatingView(View view, ViewGroup viewGroup, final Rating rating, final RatingListener ratingListener) {
        if (!viewIsOfType(view, "rating")) {
            view = DisplayUtils.getLayoutInflater(this.context).inflate(C0785R.layout.net_gogame_chat_item_rating_layout, viewGroup, false);
        }
        ImageView imageView = (ImageView) view.findViewById(C0785R.id.rateGoodImageView);
        ImageView imageView2 = (ImageView) view.findViewById(C0785R.id.rateBadImageView);
        if (rating == Rating.GOOD) {
            imageView.setImageResource(C0785R.drawable.net_gogame_chat_rate_good_selected);
            imageView2.setImageResource(C0785R.drawable.net_gogame_chat_rate_bad_unselected);
        } else if (rating == Rating.BAD) {
            imageView.setImageResource(C0785R.drawable.net_gogame_chat_rate_good_unselected);
            imageView2.setImageResource(C0785R.drawable.net_gogame_chat_rate_bad_selected);
        } else {
            imageView.setImageResource(C0785R.drawable.net_gogame_chat_rate_good_unselected);
            imageView2.setImageResource(C0785R.drawable.net_gogame_chat_rate_bad_unselected);
        }
        imageView.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (!ChatAdapterViewFactory.this.allowRatingChange && (rating == Rating.GOOD || rating == Rating.BAD)) {
                    ChatAdapterViewFactory.this.showAlreadyRated();
                } else if (rating == Rating.GOOD) {
                    ratingListener.onRatingChanged(Rating.UNRATED);
                } else {
                    ratingListener.onRatingChanged(Rating.GOOD);
                }
            }
        });
        imageView2.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (!ChatAdapterViewFactory.this.allowRatingChange && (rating == Rating.GOOD || rating == Rating.BAD)) {
                    ChatAdapterViewFactory.this.showAlreadyRated();
                } else if (rating == Rating.BAD) {
                    ratingListener.onRatingChanged(Rating.UNRATED);
                } else {
                    ratingListener.onRatingChanged(Rating.BAD);
                }
            }
        });
        return view;
    }

    private void showAlreadyRated() {
        Toast.makeText(this.context, C0785R.string.net_gogame_chat_already_rated_message, 1).show();
    }

    private Drawable getDrawable(int i) {
        if (VERSION.SDK_INT >= 21) {
            return this.context.getResources().getDrawable(i, this.context.getTheme());
        }
        return this.context.getResources().getDrawable(i);
    }
}
