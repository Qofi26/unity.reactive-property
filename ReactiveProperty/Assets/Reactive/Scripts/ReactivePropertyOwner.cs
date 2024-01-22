#nullable enable

using System.Collections;
using System.Collections.Generic;

namespace Reactive
{
    public sealed class ReactivePropertyOwner : IReactivePropertyOwner
    {
        public int Count => _reactiveProperties.Count;
        public bool IsReadOnly => false;

        private readonly HashSet<IDisposableReactiveProperty> _reactiveProperties = new();

        public IEnumerator<IDisposableReactiveProperty> GetEnumerator()
        {
            return _reactiveProperties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IDisposableReactiveProperty item)
        {
            _reactiveProperties.Add(item);
        }

        public void Clear()
        {
            foreach (var property in _reactiveProperties)
            {
                property.Unsubscribe(this);
            }

            _reactiveProperties.Clear();
        }

        public bool Contains(IDisposableReactiveProperty item)
        {
            return _reactiveProperties.Contains(item);
        }

        public void CopyTo(IDisposableReactiveProperty[] array, int arrayIndex)
        {
            _reactiveProperties.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDisposableReactiveProperty item)
        {
            return _reactiveProperties.Remove(item);
        }

        public void Dispose()
        {
            Clear();
        }
    }
}