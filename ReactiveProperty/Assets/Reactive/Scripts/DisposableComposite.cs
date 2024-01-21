using System;
using System.Collections.Generic;

namespace Reactive
{
    public class DisposableComposite : IDisposableComposite
    {
        private readonly HashSet<IDisposable> _disposables = new();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }

        public IDisposableComposite Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
            return this;
        }
    }
}