using System;
using UnityEngine;

namespace Erem.Reactive.Examples
{
    [Serializable]
    public class HeroDemo
    {
        [SerializeField]
        private ReactiveProperty<string> _name;

        [SerializeField]
        private ReactiveProperty<int> _health;

        [SerializeField]
        private ReactiveProperty<bool> _isDead;

        public IReactiveProperty<string> Name => _name;
        public IReadOnlyReactiveProperty<int> Health => _health;
        public IReadOnlyReactiveProperty<bool> IsDead => _isDead;

        public void ChangeHealth(int value)
        {
            _health.Value = Mathf.Max(_health.Value + value, 0);
            _isDead.Value = _health.Value <= 0;
        }
    }
}