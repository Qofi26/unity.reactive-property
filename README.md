# Reactive property for Unity

## Install via UPM (using Git URL)

````
https://github.com/qofi26/unity.reactive-property.git?path=/ReactiveProperty/Assets/Reactive#v/0.1.0
````

# Basic Usage

Create ReactiveProperty (IReactiveProperty). Use IReadOnlyReactiveProperty for get value and subscribe only

```
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
```

Use reactive property in any other class. You can Subscribe/Unsubscribe about value changed
Also you can use CompositeDisposable for multi unsubscribe
````
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
````

````
    public class ManagerDemo : MonoBehaviour
    {
        [SerializeField]
        private HeroDemo _hero;

        [SerializeField]
        private HeroControllerDemo _heroController;

        private void Start()
        {
            _heroController.Init(_hero);
            StartCoroutine(ApplyHeroDamage());
        }

        private IEnumerator ApplyHeroDamage()
        {
            const int interval = 1;
            const int damage = -1;

            while (!_hero.IsDead.Value)
            {
                yield return new WaitForSeconds(interval);
                _hero.ChangeHealth(damage);
            }
        }
    }
````

