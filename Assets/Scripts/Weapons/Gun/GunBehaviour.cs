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

        public float vibrationAmplitude = 1;
        public float vibrationDuration = 1;


        private float shotTime = 0;

        private GunVFX vfx;

        private void Start()
        {
            vfx = GetComponentInChildren<GunVFX>();
        }

        public void Shoot(ActivateEventArgs args)
        {
            if (Time.time - shotTime >= secondsOfCooldown)
            {
                //OpenXRInput.SendHapticImpulse();
                GameObject bulletShot = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(shotOrigin.position));
                bulletShot.GetComponent<Rigidbody>().AddForce(shotOrigin.forward * muzzleVelocity, ForceMode.Impulse);
                shotTime = Time.time;
                vfx.Shoot();
            }
        }
    }
}