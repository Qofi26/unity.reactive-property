#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace Erem.Reactive
{
    public sealed class CompositeDisposable : ICompositeDisposable
    {
        private readonly HashSet<IDisposable> _disposables = new();

        public int Count => _disposables.Count;
        public bool IsReadOnly => false;

        public IEnumerator<IDisposable> GetEnumerator()
        {
            var result = new HashSet<IDisposable>(_disposables.Count);
            foreach (var disposable in _disposables)
            {
                result.Add(disposable);
            }

            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IDisposable item)
        {
            _disposables.Add(item);
        }

        public void Clear()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }

            _disposables.Clear();
        }

        public bool Contains(IDisposable item)
        {
            return _disposables.Contains(item);
        }

        public void CopyTo(IDisposable[] array, int arrayIndex)
        {
            _disposables.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDisposable item)
        {
            return _disposables.Remove(item);
        }

        public void Dispose()
        {
            Clear();
        }
    }
}