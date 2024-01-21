using System;
using Reactive;
using Reactive.Demo;
using UnityEngine;

public class Demo : MonoBehaviour, IDisposable
{
    private IDisposableComposite _disposableComposite = new DisposableComposite();

    [SerializeField]
    private ReactiveProperty<int> _health = new();

    [SerializeField]
    private IntReactive _value1 = new(5);

    private readonly ReactiveProperty<int> _mana = new();
    private readonly ReactiveProperty<int> _money = new();

    public void Test()
    {
        _disposableComposite
            .Add(_health.Subscribe(this, OnValueChanged))
            .Add(_mana.Subscribe(this, OnValueChanged))
            .Add(_money.Subscribe(this, OnValueChanged));

        _health.Subscribe(this, OnValueChanged).AddTo(_disposableComposite);
        _mana.Subscribe(this, OnValueChanged).AddTo(_disposableComposite);
        _money.Subscribe(this, OnValueChanged).AddTo(_disposableComposite);

        _health.Subscribe(this, OnValueChanged, _disposableComposite);
        _mana.Subscribe(this, OnValueChanged, _disposableComposite);
        _money.Subscribe(this, OnValueChanged, _disposableComposite);
    }

    private void OnValueChanged(int value)
    {
    }

    public void Dispose()
    {
        _disposableComposite?.Dispose();
    }
}