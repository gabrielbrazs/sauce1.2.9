package org.apache.commons.lang3.text.translate;

import java.io.IOException;
import java.io.Writer;

public class OctalUnescaper extends CharSequenceTranslator {
    public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
        int length = (charSequence.length() - i) - 1;
        StringBuilder stringBuilder = new StringBuilder();
        if (charSequence.charAt(i) != '\\' || length <= 0 || !isOctalDigit(charSequence.charAt(i + 1))) {
            return 0;
        }
        int i2 = i + 1;
        int i3 = i + 2;
        int i4 = i + 3;
        stringBuilder.append(charSequence.charAt(i2));
        if (length > 1 && isOctalDigit(charSequence.charAt(i3))) {
            stringBuilder.append(charSequence.charAt(i3));
            if (length > 2 && isZeroToThree(charSequence.charAt(i2)) && isOctalDigit(charSequence.charAt(i4))) {
                stringBuilder.append(charSequence.charAt(i4));
            }
        }
        writer.write(Integer.parseInt(stringBuilder.toString(), 8));
        return stringBuilder.length() + 1;
    }

    private boolean isOctalDigit(char c) {
        return c >= '0' && c <= '7';
    }

    private boolean isZeroToThree(char c) {
        return c >= '0' && c <= '3';
    }
}
