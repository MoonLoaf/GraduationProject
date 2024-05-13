using System.Collections;
using UnityEngine;

namespace Tower.Projectile
{
    public class Explosion : MonoBehaviour
    {
        private WaitForSeconds wait;

        void Start()
        {
            wait = new WaitForSeconds(.5f);
            StartCoroutine(DestroyThis());
        }

        private IEnumerator DestroyThis()
        {
            yield return wait;
            Destroy(gameObject);
        }
    }
}
