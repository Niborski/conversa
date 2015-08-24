// Copyright (c) 2000-2014 Developer Express Inc.
// Licensed under the The MIT License (MIT).
// --------------------------------------------------
// https://github.com/DevExpress/DevExpress.Mvvm.Free
// --------------------------------------------------

using Conversa.Common;
using Conversa.W10.Common;
using DevExpress.Mvvm.Native;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace DevExpress.Mvvm
{
    public abstract class BindableBase 
        : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            return GetPropertyNameFast(expression);
        }

        internal static string GetPropertyNameFast(LambdaExpression expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("MemberExpression is expected in expression.Body", "expression");
            }
            const string vblocalPrefix = "$VB$Local_";
            var member = memberExpression.Member;
            if (
#if !NETFX_CORE
                member.MemberType == System.Reflection.MemberTypes.Field &&
#endif
                member.Name != null &&
                member.Name.StartsWith(vblocalPrefix))
                return member.Name.Substring(vblocalPrefix.Length);
            return member.Name;
        }

        protected DispatcherWrapper Dispatcher { get { return WindowWrapper.Current().Dispatcher; } }

        protected T GetProperty<T>(Expression<Func<T>> expression)
        {
            return PropertyManager.GetProperty<T>(GetPropertyName(expression));
        }
        protected bool SetProperty<T>(ref T storage, T value, string propertyName, Action changedCallback)
        {
            return PropertyManager.SetProperty<T>(ref storage, value, propertyName, () => CallChangedCallBackAndRaisePropertyChanged(propertyName, changedCallback));
        }
        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression, Action changedCallback)
        {
            return this.SetProperty(ref storage, value, GetPropertyName(expression), changedCallback);
        }
        protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> expression)
        {
            return this.SetProperty<T>(ref storage, value, expression, null);
        }
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return this.SetProperty<T>(ref storage, value, propertyName, null);
        }
        protected bool SetProperty<T>(ref T storage, T value, Action<T, T> changedCallback, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(storage, value))
                return false;
            T oldValue = storage;
            storage = value;
            if (changedCallback != null)
                changedCallback(oldValue, value);
            RaisePropertiesChanged(propertyName);
            return true;
        }
        protected bool SetProperty<T>(Expression<Func<T>> expression, T value)
        {
            return this.SetProperty(expression, value, null);
        }
        protected bool SetProperty<T>(Expression<Func<T>> expression, T value, Action changedCallback)
        {
            string propertyName = GetPropertyName(expression);
            return PropertyManager.SetProperty<T>(propertyName, value, () => CallChangedCallBackAndRaisePropertyChanged(propertyName, changedCallback));
        }

        void CallChangedCallBackAndRaisePropertyChanged(string propertyName, Action changedCallback)
        {
            RaisePropertyChanged(propertyName);
            if (changedCallback != null)
                changedCallback();
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var ignored = Dispatcher.DispatchAsync(() => {
                PropertyChanged.Do(x => x(this, new PropertyChangedEventArgs(propertyName)));
            });
        }
        protected void RaisePropertyChanged<T>(Expression<Func<T>> expression)
        {
            RaisePropertyChanged(GetPropertyName(expression));
        }
        protected void RaisePropertiesChanged(params string[] propertyNames)
        {
            if (propertyNames == null)
            {
                RaisePropertyChanged(string.Empty);
                return;
            }
            foreach (string propertyName in propertyNames)
            {
                RaisePropertyChanged(propertyName);
            }
        }
        protected void RaisePropertiesChanged<T1, T2>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
        }
        protected void RaisePropertiesChanged<T1, T2, T3>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
        }
        protected void RaisePropertiesChanged<T1, T2, T3, T4>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
            RaisePropertyChanged(expression4);
        }
        protected void RaisePropertiesChanged<T1, T2, T3, T4, T5>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4, Expression<Func<T5>> expression5)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
            RaisePropertyChanged(expression4);
            RaisePropertyChanged(expression5);
        }
        protected void RaisePropertiesChanged<T1, T2, T3, T4, T5, T6>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2, Expression<Func<T3>> expression3, Expression<Func<T4>> expression4, Expression<Func<T5>> expression5, Expression<Func<T5>> expression6)
        {
            RaisePropertyChanged(expression1);
            RaisePropertyChanged(expression2);
            RaisePropertyChanged(expression3);
            RaisePropertyChanged(expression4);
            RaisePropertyChanged(expression5);
            RaisePropertyChanged(expression6);
        }

        PropertyManager propertyManager;
        internal PropertyManager PropertyManager
        {
            get { return propertyManager ?? (propertyManager = new PropertyManager()); }
        }
    }
}