package com.amazon.device.iap.internal.p000a;

import com.amazon.device.iap.internal.C0185a;
import com.amazon.device.iap.internal.C0189c;
import com.amazon.device.iap.internal.C0191b;
import java.util.HashMap;
import java.util.Map;

/* renamed from: com.amazon.device.iap.internal.a.d */
public final class C0192d implements C0191b {
    /* renamed from: a */
    private static final Map<Class, Class> f17a = new HashMap();

    static {
        f17a.put(C0189c.class, C0190c.class);
        f17a.put(C0185a.class, C0186a.class);
    }

    /* renamed from: a */
    public <T> Class<T> mo1186a(Class<T> cls) {
        return (Class) f17a.get(cls);
    }
}
