using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
    public GameObject proyectilePrefab;
    public ParticleSystem smoke;
    private ParticleSystem[] particles;
    private GameObject proyectile;

    private float timer = 0;
    private void Start()
    {
        particles = GetComponentsInChildren<ParticleSystem>();
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (proyectile == null) proyectile = Instantiate(proyectilePrefab, transform.position, Quaternion.identity);
        proyectile.transform.parent = transform;
        proyectile.transform.position = transform.position;
        foreach (ParticleSystem particle in particles)
        {
            //particle.Simulate(0.000001f, true, true, false);
            particle.Play();
        }
        foreach(ParticleSystem particle in proyectile.GetComponentsInChildren<ParticleSystem>())
        {
            particle.Simulate(0.000001f, true, true, false);
            particle.Play();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer % 5 > 4) {
            Shoot();
            timer = 0;
            smoke.Play();
        }
    }
}
