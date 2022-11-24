using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VR_Prototype
{
    public class GunBehaviour : MonoBehaviour
    {
        public GameObject bullet;
        public Transform shotOrigin;
        public float muzzleVelocity = 100;
        public float secondsOfCooldown = 1;

        private float shotTime = 0;

        public void Shoot(ActivateEventArgs args)
        {
            if (Time.time - shotTime >= secondsOfCooldown)
            {
                GameObject bulletShot = Instantiate(bullet, shotOrigin.position, Quaternion.Euler(shotOrigin.position));
                bulletShot.GetComponent<Rigidbody>().AddForce(shotOrigin.forward * muzzleVelocity, ForceMode.Impulse);
                shotTime = Time.time;
            }
        }

    }
}