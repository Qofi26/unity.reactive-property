#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reactive
{
    [Serializable]
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        public T Value
        {
            get => _value;
            set
            {
                if (_equalityComparer.Equals(Value, value))
                {
                    return;
                }

                _value = value;
                Notify();
            }
        }

        [SerializeField]
        private T _value;

        private readonly List<Subscription> _subscriptions = new();
        private readonly EqualityComparer<T> _equalityComparer = EqualityComparer<T>.Default;

        public ReactiveProperty()
        {
            _value = default!;
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public void Notify()
        {
            _subscriptions.RemoveAll(x => x.Owner == null || x.Action == null);
            foreach (var subscription in _subscriptions)
            {
                subscription.Notify(Value);
            }
        }

        public IDisposableReactiveProperty Subscribe(object owner, Action<T>? action, bool notify = true)
        {
            return SubscribeInternal(new WeakReference(owner), action, notify);
        }

        public IDisposableReactiveProperty Subscribe(IReactivePropertyOwner owner,
            Action<T>? action,
            bool notify = true)
        {
            owner.Add(this);
            return SubscribeInternal(new WeakReference(owner), action, notify);
        }

        public void Unsubscribe(object? owner, Action<T>? action)
        {
            if (owner == null)
            {
                return;
            }

            _subscriptions.RemoveAll(x => x.Owner == owner && (x.Action == null || x.Action == action));
        }

        public void Unsubscribe(object? owner)
        {
            if (owner == null)
            {
                return;
            }

            _subscriptions.RemoveAll(x => x.Owner == owner || x.Action == null);
        }

        public void Dispose()
        {
            _subscriptions.Clear();
        }

        private IDisposableReactiveProperty SubscribeInternal(WeakReference owner, Action<T>? action, bool notify)
        {
            var subscription = new Subscription { Action = action, Owner = owner };
            _subscriptions.Add(subscription);
            if (notify)
            {
                subscription.Notify(Value);
            }

            return this;
        }

        private sealed class Subscription
        {
            public Action<T>? Action;
            public WeakReference? Owner;

            public void Notify(T value)
            {
                Action?.Invoke(value);
            }
        }

        public static implicit operator T(ReactiveProperty<T> property)
        {
            return property.Value;
        }

        public static implicit operator ReactiveProperty<T>(T value)
        {
            return new ReactiveProperty<T>(value);
        }
    }
}