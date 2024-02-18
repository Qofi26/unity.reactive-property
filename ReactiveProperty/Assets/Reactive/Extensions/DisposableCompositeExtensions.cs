#nullable enable

using System;
using System.Collections.Generic;

namespace Erem.Reactive
{
    public static class DisposableCompositeExtensions
    {
        public static void AddTo(this IDisposable disposable, ICollection<IDisposable> composite)
        {
            composite.Add(disposable);
        }
    }
}