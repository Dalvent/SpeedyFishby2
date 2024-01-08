using Code.Tools;
using UnityEngine;

namespace Code.Characters
{
    public class FlyForward : MonoBehaviour
    {
        public float Speed;
    
        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position.AddX(-Speed * Time.deltaTime);
        }
    }
}
