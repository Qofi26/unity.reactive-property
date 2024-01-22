using System;
using UnityEngine;

namespace Reactive.Demo
{
    public class Demo : MonoBehaviour, IDisposable
    {
        private readonly ICompositeDisposable _compositeDisposable = new CompositeDisposable();
        private readonly IReactivePropertyOwner _owner = new ReactivePropertyOwner();

        [SerializeField]
        private ReactiveProperty<int> _health = new();

        [SerializeField]
        private IntReactiveProperty _mana = new(5);

        private readonly ReactiveProperty<int> _money = new();

        public void Test()
        {
            _health.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);
            _mana.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);
            _money.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);
        }

        public void Test2()
        {
            _health.Subscribe(_owner, OnValueChanged).AddTo(_compositeDisposable);
            _mana.Subscribe(_owner, OnValueChanged).AddTo(_compositeDisposable);
            _money.Subscribe(_owner, OnValueChanged).AddTo(_compositeDisposable);
        }

        private void OnValueChanged(int value)
        {
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _owner.Dispose();
        }
    }
}