#nullable enable

using System;
using System.Collections.Generic;

namespace Erem.Reactive
{
    public interface ICompositeDisposable : ICollection<IDisposable>, IDisposable
    {
        void Unsubscribe();
    }
}