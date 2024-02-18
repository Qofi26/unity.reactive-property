#nullable enable

using System;

namespace Erem.Reactive
{
    public interface IReactiveProperty
    {
    }

    public interface INotifiedReactiveProperty
    {
        void Notify();
    }

    public interface IReadOnlyReactiveProperty<out T>
    {
        T Value { get; }
        IDisposable Subscribe(object owner, Action<T>? action, bool notify = true);
        void Unsubscribe(object owner, Action<T>? action);
        void Unsubscribe(object owner);
    }

    public interface IReactiveProperty<T> : IReactiveProperty, INotifiedReactiveProperty, IReadOnlyReactiveProperty<T>,
        IDisposable
    {
        new T Value { get; set; }
        public void SetValue(T value);
    }
}