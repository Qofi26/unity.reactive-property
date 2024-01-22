#nullable enable

using System;
using System.Collections.Generic;

namespace Reactive
{
    public interface ICompositeDisposable : ICollection<IDisposable>, IDisposable
    {
    }
}