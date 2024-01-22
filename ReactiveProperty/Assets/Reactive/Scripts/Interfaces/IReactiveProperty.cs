#nullable enable

using System;

namespace Reactive
{
    public interface IReactiveProperty
    {
    }

    public interface INotifiedReactiveProperty : IReactiveProperty
    {
        void Notify();
    }

    public interface IDisposableReactiveProperty : IReactiveProperty, IDisposable
    {
        void Unsubscribe(object owner);
    }

    // TODO: IObservable
    public interface IReadOnlyReactiveProperty<out T> : IReactiveProperty
    {
        T Value { get; }
        IDisposableReactiveProperty Subscribe(object owner, Action<T>? action, bool notify = true);
        IDisposableReactiveProperty Subscribe(IReactivePropertyOwner owner, Action<T>? action, bool notify = true);
        void Unsubscribe(object owner, Action<T>? action);
        void Unsubscribe(object owner);
    }

    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IReactiveProperty<T> : INotifiedReactiveProperty, IReadOnlyReactiveProperty<T>,
        IDisposableReactiveProperty
    {
        new T Value { get; set; }
    }
}