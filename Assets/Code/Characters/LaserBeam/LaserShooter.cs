using System.Collections;
using System.Threading.Tasks;
using Code.Tools;
using UnityEngine;

namespace Code.Characters.LaserBeam
{
    public class LaserShooter : MonoBehaviour
    {
        public AudioSource ShootAudioSource;
        public GameObject LaserGameObject;
        
        public void Awake()
        {
            LaserGameObject.SetActive(false);
        }

        public bool IsShooting { get; private set; }
        
        public Task Shoot(float shootTime)
        {
            LaserGameObject.SetActive(false);
            if(shootTime != 0)
                ShootAudioSource.PlayForDuration(shootTime);

            return this.StartCoroutineWithCallbackAsync(ShowLaserFor(shootTime));
        }

        private IEnumerator ShowLaserFor(float shootTime)
        {
            IsShooting = true;
            LaserGameObject.SetActive(true);
            yield return new WaitForSeconds(shootTime);
            LaserGameObject.SetActive(false);
            IsShooting = false;
        }
    }
}