using UnityEngine;

namespace Code.Characters
{
    public class DamageArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.GetComponent<IHealth>()?.TakeDamage();
        }
    }
}