package org.apache.commons.lang3.reflect;

import java.lang.reflect.AccessibleObject;
import java.lang.reflect.Constructor;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Modifier;
import org.apache.commons.lang3.ArrayUtils;
import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.Validate;

public class ConstructorUtils {
    public static <T> T invokeConstructor(Class<T> cls, Object... objArr) throws NoSuchMethodException, IllegalAccessException, InvocationTargetException, InstantiationException {
        Object[] nullToEmpty = ArrayUtils.nullToEmpty(objArr);
        return invokeConstructor(cls, nullToEmpty, ClassUtils.toClass(nullToEmpty));
    }

    public static <T> T invokeConstructor(Class<T> cls, Object[] objArr, Class<?>[] clsArr) throws NoSuchMethodException, IllegalAccessException, InvocationTargetException, InstantiationException {
        Object[] nullToEmpty = ArrayUtils.nullToEmpty(objArr);
        Constructor matchingAccessibleConstructor = getMatchingAccessibleConstructor(cls, ArrayUtils.nullToEmpty((Class[]) clsArr));
        if (matchingAccessibleConstructor != null) {
            return matchingAccessibleConstructor.newInstance(nullToEmpty);
        }
        throw new NoSuchMethodException("No such accessible constructor on object: " + cls.getName());
    }

    public static <T> T invokeExactConstructor(Class<T> cls, Object... objArr) throws NoSuchMethodException, IllegalAccessException, InvocationTargetException, InstantiationException {
        Object[] nullToEmpty = ArrayUtils.nullToEmpty(objArr);
        return invokeExactConstructor(cls, nullToEmpty, ClassUtils.toClass(nullToEmpty));
    }

    public static <T> T invokeExactConstructor(Class<T> cls, Object[] objArr, Class<?>[] clsArr) throws NoSuchMethodException, IllegalAccessException, InvocationTargetException, InstantiationException {
        Object[] nullToEmpty = ArrayUtils.nullToEmpty(objArr);
        Constructor accessibleConstructor = getAccessibleConstructor(cls, ArrayUtils.nullToEmpty((Class[]) clsArr));
        if (accessibleConstructor != null) {
            return accessibleConstructor.newInstance(nullToEmpty);
        }
        throw new NoSuchMethodException("No such accessible constructor on object: " + cls.getName());
    }

    public static <T> Constructor<T> getAccessibleConstructor(Class<T> cls, Class<?>... clsArr) {
        Validate.notNull(cls, "class cannot be null", new Object[0]);
        try {
            return getAccessibleConstructor(cls.getConstructor(clsArr));
        } catch (NoSuchMethodException e) {
            return null;
        }
    }

    public static <T> Constructor<T> getAccessibleConstructor(Constructor<T> constructor) {
        Validate.notNull(constructor, "constructor cannot be null", new Object[0]);
        return (MemberUtils.isAccessible(constructor) && isAccessible(constructor.getDeclaringClass())) ? constructor : null;
    }

    public static <T> Constructor<T> getMatchingAccessibleConstructor(Class<T> cls, Class<?>... clsArr) {
        Validate.notNull(cls, "class cannot be null", new Object[0]);
        try {
            AccessibleObject constructor = cls.getConstructor(clsArr);
            MemberUtils.setAccessibleWorkaround(constructor);
            return constructor;
        } catch (NoSuchMethodException e) {
            Constructor<T> constructor2 = null;
            for (Constructor constructor3 : cls.getConstructors()) {
                if (ClassUtils.isAssignable((Class[]) clsArr, constructor3.getParameterTypes(), true)) {
                    AccessibleObject accessibleConstructor = getAccessibleConstructor(constructor3);
                    if (accessibleConstructor != null) {
                        MemberUtils.setAccessibleWorkaround(accessibleConstructor);
                        if (constructor2 == null || MemberUtils.compareParameterTypes(accessibleConstructor.getParameterTypes(), constructor2.getParameterTypes(), clsArr) < 0) {
                            constructor2 = accessibleConstructor;
                        }
                    }
                }
            }
            return constructor2;
        }
    }

    private static boolean isAccessible(Class<?> cls) {
        while (cls != null) {
            if (!Modifier.isPublic(cls.getModifiers())) {
                return false;
            }
            cls = cls.getEnclosingClass();
        }
        return true;
    }
}
