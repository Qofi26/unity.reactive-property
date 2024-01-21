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
                if (EqualityComparer<T>.Default.Equals(Value, value))
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

        public IDisposable Subscribe(object owner, Action<T>? action)
        {
            return Subscribe(owner, action, true);
        }

        public IDisposable Subscribe(object owner, Action<T>? action, bool notify)
        {
            var subscription = new Subscription { Action = action, Owner = owner };
            _subscriptions.Add(subscription);
            if (notify)
            {
                subscription.Notify(Value);
            }

            return this;
        }

        public IDisposable Subscribe(object owner, Action<T>? action, IDisposableComposite disposableComposite)
        {
            return Subscribe(owner, action, true, disposableComposite);
        }

        public IDisposable Subscribe(object owner,
            Action<T>? action,
            bool notify,
            IDisposableComposite disposableComposite)
        {
            var property = Subscribe(owner, action, notify);
            disposableComposite.Add(property);
            return property;
        }

        public void Unsubscribe(object owner, Action<T>? action)
        {
            _subscriptions.RemoveAll(x => x.Owner == owner && (x.Action == null || x.Action == action));
        }

        public void Unsubscribe(object owner)
        {
            _subscriptions.RemoveAll(x => x.Owner == owner || x.Action == null);
        }

        public void Dispose()
        {
            _subscriptions.Clear();
        }

        private sealed class Subscription
        {
            public Action<T>? Action;
            public object? Owner;

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