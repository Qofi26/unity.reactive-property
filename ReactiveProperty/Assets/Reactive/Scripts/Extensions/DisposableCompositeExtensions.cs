#nullable enable

using System;

namespace Reactive
{
    public static class DisposableCompositeExtensions
    {
        public static void AddTo(this IDisposable disposable, ICompositeDisposable composite)
        {
            composite.Add(disposable);
        }
    }
}