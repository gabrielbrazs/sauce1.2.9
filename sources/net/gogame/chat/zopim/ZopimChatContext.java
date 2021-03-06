package net.gogame.chat.zopim;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.Uri;
import android.support.v4.app.FragmentActivity;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import com.zopim.android.sdk.api.ChatApi;
import com.zopim.android.sdk.api.ChatSession;
import com.zopim.android.sdk.api.ZopimChat;
import com.zopim.android.sdk.api.ZopimChat.SessionConfig;
import com.zopim.android.sdk.data.observers.AgentsObserver;
import com.zopim.android.sdk.data.observers.ChatLogObserver;
import com.zopim.android.sdk.data.observers.ConnectionObserver;
import com.zopim.android.sdk.model.Agent;
import com.zopim.android.sdk.model.ChatLog;
import com.zopim.android.sdk.model.ChatLog.Type;
import com.zopim.android.sdk.model.Connection;
import java.io.File;
import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import net.gogame.chat.AbstractChatContext;
import net.gogame.chat.AgentTypingEntry;
import net.gogame.chat.ChatAdapterViewFactory;
import net.gogame.chat.ChatAdapterViewFactory.Option;
import net.gogame.chat.ChatAdapterViewFactory.OptionListener;
import net.gogame.chat.ChatAdapterViewFactory.RatingListener;
import net.gogame.chat.ChatContext.Rating;
import net.gogame.chat.UIContext;
import org.apache.commons.lang3.StringUtils;

public class ZopimChatContext extends AbstractChatContext {
    private static boolean hasSession = false;
    private final FragmentActivity activity;
    private String agentNick = "";
    private final AgentTypingEntry agentTypingEntry = new AgentTypingEntry(false);
    private Map<String, Agent> agents;
    private final AgentsObserver agentsObserver = new C10172();
    private final BroadcastReceiver broadcastReceiver = new ChatTimeoutReceiver();
    private ChatApi chat;
    private List<ChatLog> chatLogList = new ArrayList();
    private final ChatLogObserver chatLogObserver = new C10183();
    private final ConnectionObserver connectionObserver = new C10161();
    private List<Boolean> profileIconToShowList = new ArrayList();
    private final List<File> queuedFiles = new ArrayList();
    private final SessionConfig sessionConfig;
    private final UIContext uiContext;
    private final ChatAdapterViewFactory viewFactory;

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$1 */
    class C10161 extends ConnectionObserver {
        C10161() {
        }

        public void update(Connection connection) {
        }
    }

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$2 */
    class C10172 extends AgentsObserver {
        C10172() {
        }

        public void update(Map<String, Agent> map) {
            ZopimChatContext.this.doNotifyDataSetChanged();
        }
    }

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$3 */
    class C10183 extends ChatLogObserver {
        C10183() {
        }

        public void update(LinkedHashMap<String, ChatLog> linkedHashMap) {
            ZopimChatContext.this.doNotifyDataSetChanged();
        }
    }

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$4 */
    class C10194 implements Runnable {
        C10194() {
        }

        public void run() {
            List arrayList = new ArrayList();
            List arrayList2 = new ArrayList();
            boolean z = true;
            for (String str : ZopimChat.getDataSource().getChatLog().keySet()) {
                boolean z2;
                ChatLog chatLog = (ChatLog) ZopimChat.getDataSource().getChatLog().get(str);
                arrayList.add(chatLog);
                if (z) {
                    arrayList2.add(Boolean.valueOf(true));
                } else {
                    arrayList2.add(Boolean.valueOf(false));
                }
                if (chatLog.getType() == Type.CHAT_MSG_AGENT) {
                    z2 = false;
                } else {
                    z2 = true;
                }
                z = z2;
            }
            ZopimChatContext.this.chatLogList = arrayList;
            ZopimChatContext.this.profileIconToShowList = arrayList2;
            ZopimChatContext.this.agents = ZopimChat.getDataSource().getAgents();
            ZopimChatContext.this.notifyDataSetChanged();
        }
    }

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$5 */
    class C10205 implements OptionListener {
        C10205() {
        }

        public void onOptionSelected(Option option) {
            ZopimChatContext.this.send(option.getLabel());
        }
    }

    /* renamed from: net.gogame.chat.zopim.ZopimChatContext$6 */
    class C10216 implements RatingListener {
        C10216() {
        }

        public void onRatingChanged(Rating rating) {
            ZopimChatContext.this.send(rating);
        }
    }

    public class ChatTimeoutReceiver extends BroadcastReceiver {
        public void onReceive(Context context, Intent intent) {
            if (intent != null && !ChatSession.ACTION_CHAT_SESSION_TIMEOUT.equals(intent.getAction())) {
            }
        }
    }

    public static class ZopimOption implements Option {
        private final ChatLog.Option option;

        public ZopimOption(ChatLog.Option option) {
            this.option = option;
        }

        public String getLabel() {
            return this.option.getLabel();
        }

        public boolean isSelected() {
            return this.option.isSelected();
        }
    }

    public ZopimChatContext(FragmentActivity fragmentActivity, SessionConfig sessionConfig, ChatAdapterViewFactory chatAdapterViewFactory, UIContext uIContext) {
        this.activity = fragmentActivity;
        this.sessionConfig = sessionConfig;
        this.viewFactory = chatAdapterViewFactory;
        this.uiContext = uIContext;
    }

    private void doNotifyDataSetChanged() {
        this.activity.runOnUiThread(new C10194());
    }

    public void start() {
        if (hasSession) {
            this.chat = ZopimChat.resume(this.activity);
        } else {
            this.chat = this.sessionConfig.build(this.activity);
            hasSession = true;
        }
        this.uiContext.registerReceiver(this.broadcastReceiver, new IntentFilter(ChatSession.ACTION_CHAT_SESSION_TIMEOUT));
        ZopimChat.getDataSource().addConnectionObserver(this.connectionObserver);
        ZopimChat.getDataSource().addAgentsObserver(this.agentsObserver);
        ZopimChat.getDataSource().addChatLogObserver(this.chatLogObserver);
        doNotifyDataSetChanged();
        List<File> arrayList = new ArrayList(this.queuedFiles);
        this.queuedFiles.clear();
        for (File send : arrayList) {
            send(send);
        }
    }

    public void stop() {
        this.uiContext.unregisterReceiver(this.broadcastReceiver);
        ZopimChat.getDataSource().deleteConnectionObserver(this.connectionObserver);
        ZopimChat.getDataSource().deleteAgentsObserver(this.agentsObserver);
        ZopimChat.getDataSource().deleteChatLogObserver(this.chatLogObserver);
        if (this.chat != null) {
            this.chat = null;
        }
    }

    public boolean isAttachmentSupported() {
        return true;
    }

    public int getChatEntryCount() {
        if (this.chatLogList == null) {
            return 0;
        }
        return this.chatLogList.size();
    }

    public Object getChatEntry(int i) {
        if (this.chatLogList == null || i < 0 || i >= this.chatLogList.size()) {
            return null;
        }
        return this.chatLogList.get(i);
    }

    public AgentTypingEntry getAgentTypingEntry() {
        boolean z;
        if (this.agents != null && this.agents.containsKey(this.agentNick)) {
            Agent agent = (Agent) this.agents.get(this.agentNick);
            if (!(agent == null || agent.isTyping() == null || !agent.isTyping().booleanValue())) {
                z = true;
                this.agentTypingEntry.setTyping(z);
                return this.agentTypingEntry;
            }
        }
        z = false;
        this.agentTypingEntry.setTyping(z);
        return this.agentTypingEntry;
    }

    public void send(String str) {
        String trimToNull = StringUtils.trimToNull(str);
        if (trimToNull != null) {
            this.chat.send(trimToNull);
        }
    }

    public void send(File file) {
        if (file == null) {
            return;
        }
        if (this.chat != null) {
            this.chat.send(file);
        } else {
            this.queuedFiles.add(file);
        }
    }

    public void send(Rating rating) {
        if (rating != null) {
            this.chat.sendChatRating(toRating(rating));
        }
    }

    public View getView(Object obj, int i, View view, ViewGroup viewGroup) {
        String str = null;
        if (!(obj instanceof ChatLog)) {
            return null;
        }
        ChatLog chatLog = (ChatLog) obj;
        switch (chatLog.getType()) {
            case CHAT_MSG_AGENT:
                boolean booleanValue = ((Boolean) this.profileIconToShowList.get(i)).booleanValue();
                String displayName = chatLog.getDisplayName();
                Agent agent = (Agent) this.agents.get(chatLog.getNick());
                if (agent != null) {
                    str = agent.getAvatarUri();
                }
                if (chatLog.getAttachment() != null) {
                    if (chatLog.getAttachment().getUrl() == null || chatLog.getAttachment().getThumbnail() == null) {
                        return this.viewFactory.getEmptyView(view, viewGroup);
                    }
                    return this.viewFactory.getAgentAttachmentView(view, viewGroup, booleanValue, displayName, str, chatLog.getAttachment().getUrl().toString(), chatLog.getAttachment().getThumbnail().toString());
                } else if (chatLog.getOptions() != null && chatLog.getOptions().length > 0) {
                    return this.viewFactory.getAgentOptionsView(view, viewGroup, booleanValue, displayName, str, chatLog.getMessage(), toOptions(chatLog.getOptions()), new C10205());
                } else if (chatLog.getMessage() == null) {
                    return this.viewFactory.getEmptyView(view, viewGroup);
                } else {
                    return this.viewFactory.getAgentMessageView(view, viewGroup, booleanValue, displayName, str, chatLog.getMessage());
                }
            case CHAT_MSG_VISITOR:
                return this.viewFactory.getVisitorMessageView(view, viewGroup, chatLog.getMessage());
            case CHAT_MSG_TRIGGER:
                return this.viewFactory.getNotificationView(view, viewGroup, chatLog.getMessage());
            case CHAT_MSG_SYSTEM:
                return this.viewFactory.getNotificationView(view, viewGroup, chatLog.getMessage());
            case MEMBER_JOIN:
                if (chatLog.getNick() != null && chatLog.getNick().contains("agent")) {
                    this.agentNick = chatLog.getNick();
                }
                return this.viewFactory.getEmptyView(view, viewGroup);
            case MEMBER_LEAVE:
                return this.viewFactory.getNotificationView(view, viewGroup, String.format("%s left the chat", new Object[]{chatLog.getDisplayName()}));
            case SYSTEM_OFFLINE:
                return this.viewFactory.getNotificationView(view, viewGroup, "System offline");
            case ATTACHMENT_UPLOAD:
                Uri imageUri = getImageUri(chatLog.getFileName());
                if (imageUri != null) {
                    return this.viewFactory.getVisitorAttachmentView(view, viewGroup, imageUri);
                }
                return this.viewFactory.getVisitorMessageView(view, viewGroup, String.format("%s sent", new Object[]{chatLog.getFileName()}));
            case CHAT_RATING:
                return this.viewFactory.getRatingView(view, viewGroup, toRating(chatLog.getRating()), new C10216());
            case UNKNOWN:
                log(chatLog);
                return this.viewFactory.getEmptyView(view, viewGroup);
            default:
                log(chatLog);
                return this.viewFactory.getEmptyView(view, viewGroup);
        }
    }

    private List<Option> toOptions(ChatLog.Option[] optionArr) {
        List<Option> arrayList = new ArrayList();
        for (ChatLog.Option zopimOption : optionArr) {
            arrayList.add(new ZopimOption(zopimOption));
        }
        return arrayList;
    }

    private Rating toRating(ChatLog.Rating rating) {
        if (rating == null) {
            return Rating.UNRATED;
        }
        switch (rating) {
            case GOOD:
                return Rating.GOOD;
            case BAD:
                return Rating.BAD;
            case UNRATED:
                return Rating.UNRATED;
            default:
                return Rating.UNRATED;
        }
    }

    private ChatLog.Rating toRating(Rating rating) {
        if (rating == null) {
            return ChatLog.Rating.UNRATED;
        }
        switch (rating) {
            case GOOD:
                return ChatLog.Rating.GOOD;
            case BAD:
                return ChatLog.Rating.BAD;
            case UNRATED:
                return ChatLog.Rating.UNRATED;
            default:
                return ChatLog.Rating.UNRATED;
        }
    }

    private void log(ChatLog chatLog) {
    }

    private OnClickListener onOptionClickListener(final String str) {
        return new OnClickListener() {
            public void onClick(View view) {
                ZopimChatContext.this.send(str);
            }
        };
    }
}
