// Copyright (c) 2000-2014 Developer Express Inc.
// Licensed under the The MIT License (MIT).
// --------------------------------------------------
// https://github.com/DevExpress/DevExpress.Mvvm.Free
// --------------------------------------------------

using System;
using System.Collections.Generic;

namespace DevExpress.Mvvm.Native
{
    public class PropertyManager
    {
        internal Dictionary<string, object> propertyBag = new Dictionary<string, object>();
        public bool SetProperty<T>(string propertyName, T value, Action changedCallback)
        {
            T currentValue = default(T);
            object val;
            if (propertyBag.TryGetValue(propertyName, out val))
                currentValue = (T)val;
            if (CompareValues<T>(currentValue, value))
                return false;
            propertyBag[propertyName] = value;
            changedCallback.Do(x => x());
            return true;
        }
        public T GetProperty<T>(string propertyName)
        {
            object val;
            if (propertyBag.TryGetValue(propertyName, out val))
                return (T)val;
            return default(T);
        }
        public bool SetProperty<T>(ref T storage, T value, string propertyName, Action changedCallback)
        {
            if (PropertyManager.CompareValues<T>(storage, value))
                return false;
            T oldValue = storage;
            storage = value;
            changedCallback.Do(x => x());
            return true;
        }
        static bool CompareValues<T>(T storage, T value)
        {
            return object.Equals(storage, value);
        }
    }
}