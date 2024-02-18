#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erem.Reactive
{
    [Serializable]
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        [SerializeField]
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                if (DefaultEqualityComparer.Equals(_value, value))
                {
                    return;
                }

                _value = value;
                Notify();
            }
        }

        protected virtual EqualityComparer<T> EqualityComparer => DefaultEqualityComparer;

        protected readonly EqualityComparer<T> DefaultEqualityComparer = EqualityComparer<T>.Default;

        private readonly List<Subscription> _subscriptions = new();

        public ReactiveProperty()
        {
            _value = default!;
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public static implicit operator T(ReactiveProperty<T> property)
        {
            return property.Value;
        }

        public void SetValue(T value)
        {
            Value = value;
        }

        public IDisposable Subscribe(object owner, Action<T>? action, bool notify = true)
        {
            return SubscribeInternal(owner, action, notify);
        }

        public void Unsubscribe(object? owner, Action<T>? action)
        {
            if (owner == null)
            {
                return;
            }

            _subscriptions.RemoveAll(x => x.IsEmptyOrCompare(owner, action));
        }

        public void Unsubscribe(object? owner)
        {
            if (owner == null)
            {
                return;
            }

            _subscriptions.RemoveAll(x => x.IsEmptyOrCompare(owner));
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }

            _subscriptions.Clear();
        }

        public void Notify()
        {
            _subscriptions.RemoveAll(x => x.IsEmptyOrCompare());
            foreach (var subscription in _subscriptions)
            {
                subscription.Notify(Value);
            }
        }

        private IDisposable SubscribeInternal(object owner, Action<T>? action, bool notify)
        {
            var subscription = new Subscription(owner, action);
            _subscriptions.Add(subscription);
            if (notify)
            {
                subscription.Notify(Value);
            }

            return subscription;
        }

        private sealed class Subscription : IDisposable
        {
            private object? _owner;
            private Action<T>? _action;

            public Subscription(object? owner, Action<T>? action)
            {
                _owner = owner;
                _action = action;
            }

            public void Notify(T value)
            {
                _action?.Invoke(value);
            }

            public bool IsEmptyOrCompare()
            {
                return _owner == null || _action == null;
            }

            public bool IsEmptyOrCompare(object? owner)
            {
                return IsEmptyOrCompare() || _owner == owner;
            }

            public bool IsEmptyOrCompare(object? owner, Action<T>? action)
            {
                return IsEmptyOrCompare() || IsEmptyOrCompare(owner) && _action == action;
            }

            public void Dispose()
            {
                _owner = null;
                _action = null;
            }
        }
    }
}