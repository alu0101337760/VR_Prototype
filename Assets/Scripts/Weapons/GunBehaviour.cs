using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public float muzzleVelocity = 100;

    public void Shoot(ActivateEventArgs args)
    {
        Instantiate(bullet, shotOrigin.position, Quaternion.Euler(shotOrigin.position));
        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, muzzleVelocity), ForceMode.Impulse);
    }

}
