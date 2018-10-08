package org.apache.commons.lang3;

public class SerializationException extends RuntimeException {
    private static final long serialVersionUID = 4029025366392702726L;

    public SerializationException(String str) {
        super(str);
    }

    public SerializationException(Throwable th) {
        super(th);
    }

    public SerializationException(String str, Throwable th) {
        super(str, th);
    }
}
