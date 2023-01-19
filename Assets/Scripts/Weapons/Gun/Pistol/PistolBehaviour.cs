using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.VFX;

namespace VR_Prototype
{
    public class PistolBehaviour : MonoBehaviour,  GunBehaviour
    {
        public ParticleSystem[] particles;
        public ParticleSystem HitEffects;

        public AudioClip[] shotClips;

        public Transform shotOrigin;
        public Rigidbody rb;

        [Range(0.1f, 1)]
        public float vibrationAmplitude = 1;
        public float vibrationDuration = 1;

        [Space]
        public bool alreadyShot = false;
        public float timeBetweenShots = 0.5f;
        
        
        private int layerMask = 1 << 3; // Enemigos es la layer 3
        private AudioSource audioSource;
        private float timeOfActivation = -1;

        private void Awake()
        {
            HitEffects.transform.parent = null;
            audioSource = gameObject.GetComponent<AudioSource>();
            rb = gameObject.GetComponent<Rigidbody>();
        }

        public void OnEnable()
        {
            alreadyShot = false;
        }

        public void PlayEffects()
        {
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].Play();
            }
        }

        public void PlaySound()
        {
            int randomClip = Random.Range(0, shotClips.Length);
            AudioSource.PlayClipAtPoint(shotClips[randomClip], shotOrigin.position);
        }

        public void Shoot(ActivateEventArgs args)
        {
            if (!alreadyShot)
            {
                if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
                {
                    TriggerHaptic(controllerInteractor.xrController);
                }
                PlayEffects();
                PlaySound();
                if (Physics.Raycast(shotOrigin.position, shotOrigin.forward, out RaycastHit hit, Mathf.Infinity))
                {
                    HitEffects.transform.position = hit.point;
                    HitEffects.Play();
                    if (hit.transform.gameObject.layer == 3)
                    {
                        Debug.DrawRay(shotOrigin.position, shotOrigin.forward * hit.distance, Color.red);
                        EnemyPool.instance.EnemyHit(hit.transform.gameObject.GetComponent<Enemy>().id);
                    }
                }
                timeOfActivation = Time.time;
                alreadyShot = true;
            }

        }

        private void Update() {
            if(alreadyShot) {
                if(Time.time - timeOfActivation > timeBetweenShots) {
                    alreadyShot = false;
                }
            }
        }

        private void TriggerHaptic(XRBaseController controller)
        {
            controller.SendHapticImpulse(vibrationAmplitude, vibrationDuration);
        }
    }
}