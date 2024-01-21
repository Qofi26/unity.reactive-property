#nullable enable

using System;

namespace Reactive
{
    public interface INotifiedReactiveProperty
    {
        void Notify();
    }

    public interface IReadOnlyReactiveProperty<out T> : IDisposable
    {
        T Value { get; }
        IDisposable Subscribe(object owner, Action<T>? action);
        IDisposable Subscribe(object owner, Action<T>? action, bool notify);
        IDisposable Subscribe(object owner, Action<T>? action, IDisposableComposite disposableComposite);
        IDisposable Subscribe(object owner, Action<T>? action, bool notify, IDisposableComposite disposableComposite);
        void Unsubscribe(object owner, Action<T>? action);
        void Unsubscribe(object owner);
    }

    public interface IReactiveProperty<T> : INotifiedReactiveProperty, IReadOnlyReactiveProperty<T>
    {
        new T Value { get; set; }
    }
}