using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VR_Prototype;

public class PotionCannon : MonoBehaviour, GunBehaviour
{
    public Collider loadingChamber;

    public PotionNames? LoadedPotion = null;

    public ParticleSystem[] particles;
    public Transform shotOrigin;
    public Rigidbody rb;

    [Range(0.1f, 1)]
    public float vibrationAmplitude = 1;
    public float vibrationDuration = 1;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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

    public void Shoot(ActivateEventArgs args)
    {
        if (LoadedPotion != null)
        {
            if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
            {
                TriggerHaptic(controllerInteractor.xrController);
            }
            PlayEffects();

            ///SHOOT POTION LOGIC
            GameObject shotPotion = PotionManager.instance.InstantiatePotion(LoadedPotion, shotOrigin.position, shotOrigin.rotation);

            LoadedPotion = null;
        }
    }


    public void LoadPotion(PotionNames potionToLoad)
    {
        // ACTIVATE MESH OBJECT
        transform.Find("PotionVisuals").gameObject.SetActive(true);

        // CHANGE MESH OBJECT'S POTION MATERIAL TO WHATEVER IT SHOULD BE
        PotionComponents potionComponent = PotionManager.instance.potionComponents.potionComponents[(int)potionToLoad];
        transform.Find("Potion").GetComponent<MeshRenderer>().material = potionComponent.potionMaterial;

        // ASIGN LOAD POTION NAME
        LoadedPotion = potionToLoad;
    }

    private void TriggerHaptic(XRBaseController controller)
    {
        controller.SendHapticImpulse(vibrationAmplitude, vibrationDuration);
    }
}
