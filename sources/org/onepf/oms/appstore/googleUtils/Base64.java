package org.onepf.oms.appstore.googleUtils;

import android.support.v4.media.TransportMediator;
import com.google.android.gms.nearby.messages.Strategy;
import org.jetbrains.annotations.NotNull;

public class Base64 {
    private static final byte[] ALPHABET = new byte[]{(byte) 65, (byte) 66, (byte) 67, (byte) 68, (byte) 69, (byte) 70, (byte) 71, (byte) 72, (byte) 73, (byte) 74, (byte) 75, (byte) 76, (byte) 77, (byte) 78, (byte) 79, (byte) 80, (byte) 81, (byte) 82, (byte) 83, (byte) 84, (byte) 85, (byte) 86, (byte) 87, (byte) 88, (byte) 89, (byte) 90, (byte) 97, (byte) 98, (byte) 99, (byte) 100, (byte) 101, (byte) 102, (byte) 103, (byte) 104, (byte) 105, (byte) 106, (byte) 107, (byte) 108, (byte) 109, (byte) 110, (byte) 111, (byte) 112, (byte) 113, (byte) 114, (byte) 115, (byte) 116, (byte) 117, (byte) 118, (byte) 119, (byte) 120, (byte) 121, (byte) 122, (byte) 48, (byte) 49, (byte) 50, (byte) 51, (byte) 52, (byte) 53, (byte) 54, (byte) 55, (byte) 56, (byte) 57, (byte) 43, (byte) 47};
    private static final byte[] DECODABET = new byte[]{(byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, (byte) -9, (byte) -9, WHITE_SPACE_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, WHITE_SPACE_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) 62, (byte) -9, (byte) -9, (byte) -9, (byte) 63, (byte) 52, (byte) 53, (byte) 54, (byte) 55, (byte) 56, (byte) 57, (byte) 58, (byte) 59, (byte) 60, EQUALS_SIGN, (byte) -9, (byte) -9, (byte) -9, EQUALS_SIGN_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) 0, (byte) 1, (byte) 2, (byte) 3, (byte) 4, (byte) 5, (byte) 6, (byte) 7, (byte) 8, (byte) 9, NEW_LINE, (byte) 11, (byte) 12, (byte) 13, (byte) 14, (byte) 15, (byte) 16, (byte) 17, (byte) 18, (byte) 19, (byte) 20, (byte) 21, (byte) 22, (byte) 23, (byte) 24, (byte) 25, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) 26, (byte) 27, (byte) 28, (byte) 29, (byte) 30, (byte) 31, (byte) 32, (byte) 33, (byte) 34, (byte) 35, (byte) 36, (byte) 37, (byte) 38, (byte) 39, (byte) 40, (byte) 41, (byte) 42, (byte) 43, (byte) 44, (byte) 45, (byte) 46, (byte) 47, (byte) 48, (byte) 49, (byte) 50, (byte) 51, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9};
    public static final boolean DECODE = false;
    public static final boolean ENCODE = true;
    private static final byte EQUALS_SIGN = (byte) 61;
    private static final byte EQUALS_SIGN_ENC = (byte) -1;
    private static final byte NEW_LINE = (byte) 10;
    private static final byte[] WEBSAFE_ALPHABET = new byte[]{(byte) 65, (byte) 66, (byte) 67, (byte) 68, (byte) 69, (byte) 70, (byte) 71, (byte) 72, (byte) 73, (byte) 74, (byte) 75, (byte) 76, (byte) 77, (byte) 78, (byte) 79, (byte) 80, (byte) 81, (byte) 82, (byte) 83, (byte) 84, (byte) 85, (byte) 86, (byte) 87, (byte) 88, (byte) 89, (byte) 90, (byte) 97, (byte) 98, (byte) 99, (byte) 100, (byte) 101, (byte) 102, (byte) 103, (byte) 104, (byte) 105, (byte) 106, (byte) 107, (byte) 108, (byte) 109, (byte) 110, (byte) 111, (byte) 112, (byte) 113, (byte) 114, (byte) 115, (byte) 116, (byte) 117, (byte) 118, (byte) 119, (byte) 120, (byte) 121, (byte) 122, (byte) 48, (byte) 49, (byte) 50, (byte) 51, (byte) 52, (byte) 53, (byte) 54, (byte) 55, (byte) 56, (byte) 57, (byte) 45, (byte) 95};
    private static final byte[] WEBSAFE_DECODABET = new byte[]{(byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, WHITE_SPACE_ENC, WHITE_SPACE_ENC, (byte) -9, (byte) -9, WHITE_SPACE_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, WHITE_SPACE_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) 62, (byte) -9, (byte) -9, (byte) 52, (byte) 53, (byte) 54, (byte) 55, (byte) 56, (byte) 57, (byte) 58, (byte) 59, (byte) 60, EQUALS_SIGN, (byte) -9, (byte) -9, (byte) -9, EQUALS_SIGN_ENC, (byte) -9, (byte) -9, (byte) -9, (byte) 0, (byte) 1, (byte) 2, (byte) 3, (byte) 4, (byte) 5, (byte) 6, (byte) 7, (byte) 8, (byte) 9, NEW_LINE, (byte) 11, (byte) 12, (byte) 13, (byte) 14, (byte) 15, (byte) 16, (byte) 17, (byte) 18, (byte) 19, (byte) 20, (byte) 21, (byte) 22, (byte) 23, (byte) 24, (byte) 25, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) 63, (byte) -9, (byte) 26, (byte) 27, (byte) 28, (byte) 29, (byte) 30, (byte) 31, (byte) 32, (byte) 33, (byte) 34, (byte) 35, (byte) 36, (byte) 37, (byte) 38, (byte) 39, (byte) 40, (byte) 41, (byte) 42, (byte) 43, (byte) 44, (byte) 45, (byte) 46, (byte) 47, (byte) 48, (byte) 49, (byte) 50, (byte) 51, (byte) -9, (byte) -9, (byte) -9, (byte) -9, (byte) -9};
    private static final byte WHITE_SPACE_ENC = (byte) -5;

    private Base64() {
    }

    @NotNull
    public static byte[] decode(@NotNull String str) throws Base64DecoderException {
        byte[] bytes = str.getBytes();
        return decode(bytes, 0, bytes.length);
    }

    @NotNull
    public static byte[] decode(@NotNull byte[] bArr) throws Base64DecoderException {
        return decode(bArr, 0, bArr.length);
    }

    @NotNull
    public static byte[] decode(byte[] bArr, int i, int i2) throws Base64DecoderException {
        return decode(bArr, i, i2, DECODABET);
    }

    @NotNull
    public static byte[] decode(byte[] bArr, int i, int i2, byte[] bArr2) throws Base64DecoderException {
        Object obj;
        Object obj2 = new byte[(((i2 * 3) / 4) + 2)];
        byte[] bArr3 = new byte[4];
        int i3 = 0;
        int i4 = 0;
        int i5 = 0;
        while (i3 < i2) {
            byte b = (byte) (bArr[i3 + i] & TransportMediator.KEYCODE_MEDIA_PAUSE);
            byte b2 = bArr2[b];
            if (b2 >= WHITE_SPACE_ENC) {
                int i6;
                if (b2 < EQUALS_SIGN_ENC) {
                    i6 = i5;
                } else if (b == EQUALS_SIGN) {
                    i6 = i2 - i3;
                    b = (byte) (bArr[(i2 - 1) + i] & TransportMediator.KEYCODE_MEDIA_PAUSE);
                    if (i5 == 0 || i5 == 1) {
                        throw new Base64DecoderException("invalid padding byte '=' at byte offset " + i3);
                    } else if ((i5 != 3 || i6 <= 2) && (i5 != 4 || i6 <= 1)) {
                        if (!(b == EQUALS_SIGN || b == NEW_LINE)) {
                            throw new Base64DecoderException("encoded value has invalid trailing byte");
                        }
                        if (i5 != 0) {
                            if (i5 != 1) {
                                throw new Base64DecoderException("single trailing character at offset " + (i2 - 1));
                            }
                            bArr3[i5] = (byte) 61;
                            i4 += decode4to3(bArr3, 0, obj2, i4, bArr2);
                        }
                        obj = new byte[i4];
                        System.arraycopy(obj2, 0, obj, 0, i4);
                        return obj;
                    } else {
                        throw new Base64DecoderException("padding byte '=' falsely signals end of encoded value at offset " + i3);
                    }
                } else {
                    i6 = i5 + 1;
                    bArr3[i5] = (byte) b;
                    if (i6 == 4) {
                        i4 += decode4to3(bArr3, 0, obj2, i4, bArr2);
                        i6 = 0;
                    }
                }
                i3++;
                i5 = i6;
            } else {
                throw new Base64DecoderException("Bad Base64 input character at " + i3 + ": " + bArr[i3 + i] + "(decimal)");
            }
        }
        if (i5 != 0) {
            if (i5 != 1) {
                bArr3[i5] = (byte) 61;
                i4 += decode4to3(bArr3, 0, obj2, i4, bArr2);
            } else {
                throw new Base64DecoderException("single trailing character at offset " + (i2 - 1));
            }
        }
        obj = new byte[i4];
        System.arraycopy(obj2, 0, obj, 0, i4);
        return obj;
    }

    private static int decode4to3(byte[] bArr, int i, byte[] bArr2, int i2, byte[] bArr3) {
        if (bArr[i + 2] == EQUALS_SIGN) {
            bArr2[i2] = (byte) ((byte) ((((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12)) >>> 16));
            return 1;
        } else if (bArr[i + 3] == EQUALS_SIGN) {
            r0 = (((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12)) | ((bArr3[bArr[i + 2]] << 24) >>> 18);
            bArr2[i2] = (byte) ((byte) (r0 >>> 16));
            bArr2[i2 + 1] = (byte) ((byte) (r0 >>> 8));
            return 2;
        } else {
            r0 = ((((bArr3[bArr[i]] << 24) >>> 6) | ((bArr3[bArr[i + 1]] << 24) >>> 12)) | ((bArr3[bArr[i + 2]] << 24) >>> 18)) | ((bArr3[bArr[i + 3]] << 24) >>> 24);
            bArr2[i2] = (byte) ((byte) (r0 >> 16));
            bArr2[i2 + 1] = (byte) ((byte) (r0 >> 8));
            bArr2[i2 + 2] = (byte) ((byte) r0);
            return 3;
        }
    }

    @NotNull
    public static byte[] decodeWebSafe(@NotNull String str) throws Base64DecoderException {
        byte[] bytes = str.getBytes();
        return decodeWebSafe(bytes, 0, bytes.length);
    }

    @NotNull
    public static byte[] decodeWebSafe(@NotNull byte[] bArr) throws Base64DecoderException {
        return decodeWebSafe(bArr, 0, bArr.length);
    }

    @NotNull
    public static byte[] decodeWebSafe(byte[] bArr, int i, int i2) throws Base64DecoderException {
        return decode(bArr, i, i2, WEBSAFE_DECODABET);
    }

    @NotNull
    public static String encode(@NotNull byte[] bArr) {
        return encode(bArr, 0, bArr.length, ALPHABET, true);
    }

    @NotNull
    public static String encode(byte[] bArr, int i, int i2, byte[] bArr2, boolean z) {
        byte[] encode = encode(bArr, i, i2, bArr2, (int) Strategy.TTL_SECONDS_INFINITE);
        int length = encode.length;
        while (!z && length > 0 && encode[length - 1] == EQUALS_SIGN) {
            length--;
        }
        return new String(encode, 0, length);
    }

    @NotNull
    public static byte[] encode(byte[] bArr, int i, int i2, byte[] bArr2, int i3) {
        int i4 = ((i2 + 2) / 3) * 4;
        byte[] bArr3 = new byte[(i4 + (i4 / i3))];
        int i5 = 0;
        int i6 = 0;
        int i7 = 0;
        while (i5 < i2 - 2) {
            i4 = (((bArr[i5 + i] << 24) >>> 8) | ((bArr[(i5 + 1) + i] << 24) >>> 16)) | ((bArr[(i5 + 2) + i] << 24) >>> 24);
            bArr3[i7] = (byte) bArr2[i4 >>> 18];
            bArr3[i7 + 1] = (byte) bArr2[(i4 >>> 12) & 63];
            bArr3[i7 + 2] = (byte) bArr2[(i4 >>> 6) & 63];
            bArr3[i7 + 3] = (byte) bArr2[i4 & 63];
            i4 = i6 + 4;
            if (i4 == i3) {
                bArr3[i7 + 4] = (byte) 10;
                i7++;
                i4 = 0;
            }
            i5 += 3;
            i7 += 4;
            i6 = i4;
        }
        if (i5 < i2) {
            encode3to4(bArr, i5 + i, i2 - i5, bArr3, i7, bArr2);
            if (i6 + 4 == i3) {
                bArr3[i7 + 4] = (byte) 10;
                i4 = i7 + 1;
            }
        }
        return bArr3;
    }

    private static byte[] encode3to4(byte[] bArr, int i, int i2, byte[] bArr2, int i3, byte[] bArr3) {
        int i4 = 0;
        int i5 = i2 > 0 ? (bArr[i] << 24) >>> 8 : 0;
        int i6 = i2 > 1 ? (bArr[i + 1] << 24) >>> 16 : 0;
        if (i2 > 2) {
            i4 = (bArr[i + 2] << 24) >>> 24;
        }
        i4 |= i6 | i5;
        switch (i2) {
            case 1:
                bArr2[i3] = (byte) bArr3[i4 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i4 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) 61;
                bArr2[i3 + 3] = (byte) 61;
                break;
            case 2:
                bArr2[i3] = (byte) bArr3[i4 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i4 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) bArr3[(i4 >>> 6) & 63];
                bArr2[i3 + 3] = (byte) 61;
                break;
            case 3:
                bArr2[i3] = (byte) bArr3[i4 >>> 18];
                bArr2[i3 + 1] = (byte) bArr3[(i4 >>> 12) & 63];
                bArr2[i3 + 2] = (byte) bArr3[(i4 >>> 6) & 63];
                bArr2[i3 + 3] = (byte) bArr3[i4 & 63];
                break;
        }
        return bArr2;
    }

    @NotNull
    public static String encodeWebSafe(@NotNull byte[] bArr, boolean z) {
        return encode(bArr, 0, bArr.length, WEBSAFE_ALPHABET, z);
    }
}
