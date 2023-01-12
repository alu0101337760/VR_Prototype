using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;

namespace VR_Prototype
{
    public class GunBehaviour : MonoBehaviour
    {
        public GameObject bullet;
        public Transform shotOrigin;
        public float muzzleVelocity = 100;
        public float secondsOfCooldown = 1;

        [Range(0.1f,1)]
        public float vibrationAmplitude = 1;
        public float vibrationDuration = 1;

        private float shotTime = 0;
        
        int layerMask = 1 << 3;
        
        public ParticleSystem particles;

        public void Shoot(ActivateEventArgs args)
        {
            
            if (Time.time - shotTime >= secondsOfCooldown)
            {
                if(args.interactorObject is XRBaseControllerInteractor controllerInteractor)
                {
                    TriggerHaptic(controllerInteractor.xrController);
                }
                //particles.Play();
                if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    Debug.DrawRay(shotOrigin.position, shotOrigin.forward * hit.distance, Color.red);
                    EnemyPool.instance.EnemyHit(hit.transform.gameObject.GetComponent<Enemy>().id);
                }            
                shotTime = Time.time;
            }
        }
        
        private void TriggerHaptic(XRBaseController controller)
        {
            controller.SendHapticImpulse(vibrationAmplitude, vibrationDuration);
        }

    }
}