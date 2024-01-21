using System;

namespace Reactive.Demo
{
    [Serializable]
    public class IntReactive : ReactiveProperty<int>
    {
        public IntReactive(int value) : base(value)
        {
        }
    }
}