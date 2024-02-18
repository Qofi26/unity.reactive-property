using UnityEngine;

namespace Erem.Reactive.Examples
{
    public class HeroControllerDemo : MonoBehaviour
    {
        private HeroDemo _hero;

        private readonly CompositeDisposable _compositeDisposable = new();

        public void Init(HeroDemo hero)
        {
            Unsubscribe();
            _hero = hero;
            Subscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (_hero == null)
            {
                return;
            }

            _hero.Health.Subscribe(this, OnHealthChanged).AddTo(_compositeDisposable);
            _hero.IsDead.Subscribe(this, OnIsDeadChanged, false).AddTo(_compositeDisposable);
        }

        private void Unsubscribe()
        {
            _compositeDisposable.Unsubscribe();
        }

        private void OnIsDeadChanged(bool isDead)
        {
            Debug.Log($"Hero {_hero.Name.Value} isDead={isDead}");
        }

        private void OnHealthChanged(int health)
        {
            Debug.Log($"Hero {_hero.Name.Value} health: {health}");
        }
    }
}