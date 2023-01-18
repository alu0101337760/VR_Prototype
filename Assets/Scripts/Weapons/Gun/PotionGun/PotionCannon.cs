using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VR_Prototype;

public class PotionCannon : MonoBehaviour, GunBehaviour
{
    public Collider loadingChamber;

    public PotionNames loadedPotion;
    public bool isLoaded = false;

    public ParticleSystem[] particles;
    public Transform shotOrigin;
    public Rigidbody rb;

    public float muzzleVelocity = 50;

    [Range(0.1f, 1)]
    public float vibrationAmplitude = 1;
    public float vibrationDuration = 1;

    public AudioClip shotClip;

    private GameObject potionVisuals;
    private MeshRenderer potionRenderer;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        potionVisuals = transform.Find("PotionVisuals").gameObject;
        potionRenderer = potionVisuals.transform.Find("Potion").GetComponent<MeshRenderer>();
        potionVisuals.SetActive(false);
    }

    public void OnEnable()
    {
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
        audioSource.PlayOneShot(shotClip);
    }

    public void Shoot(ActivateEventArgs args)
    {
        if (isLoaded)
        {
            if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
            {
                TriggerHaptic(controllerInteractor.xrController);
            }
            PlayEffects();
            PlaySound();
            ///SHOOT POTION LOGIC
            GameObject shotPotion = PotionManager.instance.InstantiatePotion(loadedPotion, shotOrigin.position, shotOrigin.rotation);
            shotPotion.GetComponent<Rigidbody>().AddForce(transform.forward * muzzleVelocity, ForceMode.Impulse);
            UnloadPotion();
        }
    }

    private void UnloadPotion()
    {
        potionVisuals.SetActive(false);
        loadingChamber.enabled = true;
        isLoaded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Potion potComponent = other.gameObject.GetComponent<Potion>();
        if ( potComponent != null && !isLoaded) {
            PotionNames potionName = other.GetComponent<Potion>().potionName;
            LoadPotion(potionName);
            Destroy(other.gameObject);
        }
    }

    public void LoadPotion(PotionNames potionToLoad)
    {
        // ACTIVATE MESH OBJECT
        potionVisuals.SetActive(true);

        // CHANGE MESH OBJECT'S POTION MATERIAL TO WHATEVER IT SHOULD BE
        PotionComponents potionComponent = PotionManager.instance.potionComponents.potionComponents[(int)potionToLoad];
        potionRenderer.material = potionComponent.potionMaterial;

        // ASIGN LOAD POTION NAME
        loadedPotion = potionToLoad;
        isLoaded = true;

        // DISABLE LOADING CHAMBER
        loadingChamber.enabled = false;
    }

    private void TriggerHaptic(XRBaseController controller)
    {
        controller.SendHapticImpulse(vibrationAmplitude, vibrationDuration);
    }
}
