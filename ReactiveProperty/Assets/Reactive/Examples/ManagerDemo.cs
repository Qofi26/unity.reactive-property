using System.Collections;
using UnityEngine;

namespace Erem.Reactive.Examples
{
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
}