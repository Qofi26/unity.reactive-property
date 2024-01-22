#nullable enable

using System;
using System.Collections.Generic;

namespace Reactive
{
    public interface IReactivePropertyOwner : ICollection<IDisposableReactiveProperty>, IDisposable
    {
    }
}