using System;
using System.Collections.Generic;
using UnityEngine;

namespace Erem.Reactive.Demo
{
    public class Demo : MonoBehaviour, IDisposable
    {
        private readonly ICompositeDisposable _compositeDisposable = new CompositeDisposable();

        [SerializeField]
        private ReactiveProperty<int> _health = new();

        [SerializeField]
        private IntReactiveProperty _mana = new(5);

        [SerializeField]
        private List<IntReactiveProperty> _values = new();

        private readonly ReactiveProperty<int> _money = new();

        [SerializeField]
        private Test1 _test1;

        private void Awake()
        {
            _test1.Init();
            Init();
        }

        public void Init()
        {
            _health.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);
            _mana.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);
            _money.Subscribe(this, OnValueChanged).AddTo(_compositeDisposable);

            foreach (var value in _values)
            {
                value.Subscribe(this, OnValueChanged);
            }
        }

        private void OnValueChanged(int value)
        {
            Debug.Log($"OnValue changed. New value: {value}");
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        [Serializable]
        private class Test1
        {
            [SerializeField]
            private List<IntReactiveProperty> _values = new();

            [SerializeField]
            private IReactiveProperty<bool> _boolValue = new ReactiveProperty<bool>();

            [SerializeField]
            private ReactiveProperty<bool> _boolValue2 = new();

            [SerializeField]
            private IReactiveProperty<int> _intValue = new ReactiveProperty<int>();

            [SerializeField]
            private ReactiveProperty<int> _intValue2 = new();

            public void Init()
            {
                _boolValue.Subscribe(this, OnValueChanged);
                _boolValue2.Subscribe(this, OnValueChanged);
                _intValue.Subscribe(this, OnValueChanged);
                _intValue2.Subscribe(this, OnValueChanged);

                foreach (var value in _values)
                {
                    value.Subscribe(this, OnValueChanged);
                }
            }

            private void OnValueChanged(int value)
            {
                Debug.Log($"OnValue changed. New value: {value}");
            }

            private void OnValueChanged(bool value)
            {
                Debug.Log($"OnValue changed. New value: {value}");
            }
        }
    }
}