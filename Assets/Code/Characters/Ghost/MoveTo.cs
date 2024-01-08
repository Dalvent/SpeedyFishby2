using Unity.VisualScripting;
using UnityEngine;

namespace Code.Characters
{
    public class MoveTo : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public float Speed;

        public Transform Target { get; set; }

        void Update()
        {
            if(Target.IsUnityNull())
                return;
        
            Vector2 direction = Target.position - transform.position;
            direction.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            SpriteRenderer.flipX = !(direction.x >= 0);

            if (SpriteRenderer.flipX)
                angle -= 180f;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
