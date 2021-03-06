package net.gogame.gowrap.ui.v2017_1;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.TextView;
import java.util.ArrayList;
import java.util.Arrays;
import net.gogame.gowrap.C1110R;
import net.gogame.gowrap.model.faq.Article;
import net.gogame.gowrap.model.faq.Category;
import net.gogame.gowrap.model.faq.Section;

public class FaqExpandableListAdapter extends BaseExpandableListAdapter {
    private final Category category;
    private final Context context;
    private final Category searchResultsCategory;
    private String[] terms;

    public FaqExpandableListAdapter(Context context, Category category) {
        this.context = context;
        this.category = category;
        String string = context.getResources().getString(C1110R.string.net_gogame_gowrap_support_search_results_caption);
        this.searchResultsCategory = new Category(string, string, Arrays.asList(new Section[]{new Section(string, new ArrayList())}));
    }

    public void setSearchTerms(String[] strArr) {
        this.terms = strArr;
        Section section = (Section) this.searchResultsCategory.getSections().get(0);
        section.getArticles().clear();
        if (strArr != null) {
            for (Section articles : this.category.getSections()) {
                for (Article article : articles.getArticles()) {
                    String toLowerCase = article.getTitle() != null ? article.getTitle().toLowerCase() : null;
                    String toLowerCase2;
                    if (article.getBody() != null) {
                        toLowerCase2 = article.getBody().toLowerCase();
                    } else {
                        toLowerCase2 = null;
                    }
                    for (CharSequence charSequence : strArr) {
                        if (charSequence != null && ((toLowerCase != null && toLowerCase.contains(charSequence)) || (r4 != null && r4.contains(charSequence)))) {
                            section.getArticles().add(article);
                            break;
                        }
                    }
                }
            }
        }
        notifyDataSetChanged();
    }

    private Category getDataSource() {
        if (this.terms == null) {
            return this.category;
        }
        return this.searchResultsCategory;
    }

    public int getGroupCount() {
        return getDataSource().getSections().size();
    }

    public Object getGroup(int i) {
        return getDataSource().getSections().get(i);
    }

    public long getGroupId(int i) {
        return (long) i;
    }

    public int getChildrenCount(int i) {
        Section section = (Section) getGroup(i);
        if (this.terms == null || !section.getArticles().isEmpty()) {
            return section.getArticles().size();
        }
        return 1;
    }

    public Object getChild(int i, int i2) {
        Section section = (Section) getGroup(i);
        if (i2 < section.getArticles().size()) {
            return section.getArticles().get(i2);
        }
        return null;
    }

    public long getChildId(int i, int i2) {
        return (long) i2;
    }

    public boolean hasStableIds() {
        return false;
    }

    public boolean isChildSelectable(int i, int i2) {
        return true;
    }

    public View getGroupView(int i, boolean z, View view, ViewGroup viewGroup) {
        View inflate;
        Section section = (Section) getGroup(i);
        if (!z || getChildrenCount(i) <= 0) {
            inflate = ((LayoutInflater) this.context.getSystemService("layout_inflater")).inflate(C1110R.layout.net_gogame_gowrap_fragment_faq_section_list_item, viewGroup, false);
        } else {
            inflate = ((LayoutInflater) this.context.getSystemService("layout_inflater")).inflate(C1110R.layout.net_gogame_gowrap_fragment_faq_section_expanded_list_item, viewGroup, false);
        }
        ((TextView) inflate.findViewById(C1110R.id.net_gogame_gowrap_faq_section_name)).setText(section.getName());
        return inflate;
    }

    public int getChildType(int i, int i2) {
        if (i2 == getChildrenCount(i) - 1) {
            return 2;
        }
        if (i2 == 0) {
            return 0;
        }
        return 1;
    }

    public int getChildTypeCount() {
        return 3;
    }

    public View getChildView(int i, int i2, boolean z, View view, ViewGroup viewGroup) {
        Article article = (Article) getChild(i, i2);
        int childType = getChildType(i, i2);
        if (view == null) {
            LayoutInflater layoutInflater = (LayoutInflater) this.context.getSystemService("layout_inflater");
            switch (childType) {
                case 0:
                    view = layoutInflater.inflate(C1110R.layout.net_gogame_gowrap_fragment_faq_article_list_item_header, viewGroup, false);
                    break;
                case 1:
                    view = layoutInflater.inflate(C1110R.layout.net_gogame_gowrap_fragment_faq_article_list_item, viewGroup, false);
                    break;
                case 2:
                    view = layoutInflater.inflate(C1110R.layout.net_gogame_gowrap_fragment_faq_article_list_item_footer, viewGroup, false);
                    break;
            }
        }
        TextView textView = (TextView) view.findViewById(C1110R.id.net_gogame_gowrap_faq_article_title);
        if (article != null) {
            textView.setText(article.getTitle());
        } else {
            textView.setText(C1110R.string.net_gogame_gowrap_faq_search_no_results_message);
        }
        return view;
    }
}
