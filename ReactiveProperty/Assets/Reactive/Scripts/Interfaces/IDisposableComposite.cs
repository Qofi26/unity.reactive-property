using System;

namespace Reactive
{
    public interface IDisposableComposite : IDisposable
    {
        IDisposableComposite Add(IDisposable disposable);
    }
}