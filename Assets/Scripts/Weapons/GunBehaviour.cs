using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotOrigin;
    public float muzzleVelocity = 100;

    void Start()
    {
        bullet = Instantiate(bullet);
        bullet.transform.parent = this.transform;
        bullet.SetActive(false);
    }

    public void Shoot(ActivateEventArgs args)
    {
        bullet.transform.position = shotOrigin.position;
        bullet.transform.rotation = shotOrigin.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * muzzleVelocity);
    }

}
