using System;

namespace Reactive
{
    public static class DisposableCompositeExtensions
    {
        public static void AddTo(this IDisposable disposable, IDisposableComposite composite)
        {
            composite.Add(disposable);
        }
    }
}