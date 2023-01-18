using UnityEngine;
using UnityEngine.VFX;

namespace VR_Prototype
{
    public abstract class Potion : MonoBehaviour
    {
        public float radius = 1.0f;
        public float timeOfActivation = -1;
        public float effectDuration = 5;
        public PotionNames potionName;
        public ParticleSystem[] potionEffects;

        private bool isTriggered = false;

        protected abstract void HandlePotionEffect(Collision collision);

        public abstract void StopPotionEffect();

        protected void PlayEffects() {
            for (int i = 0; i < potionEffects.Length; i++)
            {
                potionEffects[i].Play();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.layer == 3 || collision.gameObject.layer == 7) && !isTriggered)
            {
                Debug.Log("Potion effect triggered");
                isTriggered = true;
                this.timeOfActivation = Time.time;
                PotionManager.instance.AddUsedPotion(this);
                PlayEffects();
                HandlePotionEffect(collision);
            }
        }
    }
}