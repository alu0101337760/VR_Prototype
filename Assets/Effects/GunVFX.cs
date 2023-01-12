using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVFX : MonoBehaviour
{
    public ParticleSystem smoke;
    public ParticleSystem flash;
    public ParticleSystem sparks;

    public void Shoot()
    {
        sparks.Play();
        flash.Play();
        smoke.Play();
    }
}
