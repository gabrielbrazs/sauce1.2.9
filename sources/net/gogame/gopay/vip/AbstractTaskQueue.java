package net.gogame.gopay.vip;

import net.gogame.gopay.vip.TaskQueue.Listener;

public abstract class AbstractTaskQueue<T> implements Runnable, TaskQueue<T> {
    public static final long DEFAULT_DELAY = 60000;
    /* renamed from: a */
    private final Listener<T> f3647a;
    /* renamed from: b */
    private long f3648b = 60000;
    /* renamed from: c */
    private Thread f3649c = null;

    protected abstract T peek();

    protected abstract void remove();

    protected abstract boolean shouldProcess();

    public AbstractTaskQueue(Listener<T> listener) {
        this.f3647a = listener;
    }

    public long getDelay() {
        return this.f3648b;
    }

    public void setDelay(long j) {
        this.f3648b = j;
    }

    public void start() {
        if (this.f3649c == null || !this.f3649c.isAlive()) {
            this.f3649c = new Thread(this);
            this.f3649c.start();
            return;
        }
        throw new IllegalStateException();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void run() {
        /*
        r3 = this;
    L_0x0000:
        r0 = r3.shouldProcess();	 Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
        if (r0 == 0) goto L_0x000c;
    L_0x0006:
        r0 = r3.peek();	 Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
        if (r0 != 0) goto L_0x0014;
    L_0x000c:
        r0 = r3.f3648b;	 Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
        java.lang.Thread.sleep(r0);	 Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
        goto L_0x0000;
    L_0x0012:
        r0 = move-exception;
        return;
    L_0x0014:
        r1 = r3.f3647a;	 Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
        if (r1 == 0) goto L_0x0036;
    L_0x0018:
        r1 = r3.f3647a;	 Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
        r0 = r1.onTask(r0);	 Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
        if (r0 == 0) goto L_0x000c;
    L_0x0020:
        r3.remove();	 Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
        goto L_0x0000;
    L_0x0024:
        r0 = move-exception;
        r1 = "goPay";
        r2 = "Exception";
        android.util.Log.e(r1, r2, r0);	 Catch:{ InterruptedException -> 0x0012, Exception -> 0x002d }
        goto L_0x0000;
    L_0x002d:
        r0 = move-exception;
        r1 = "goPay";
        r2 = "Exception";
        android.util.Log.e(r1, r2, r0);
        goto L_0x0000;
    L_0x0036:
        r3.remove();	 Catch:{ Exception -> 0x0024, InterruptedException -> 0x0012 }
        goto L_0x0000;
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gopay.vip.AbstractTaskQueue.run():void");
    }
}
