using UnityEngine;

namespace VR_Prototype
{
    public class SlowPotion : Potion
    {
        public override void StopPotionEffect() {Destroy(potionEffect);}

        protected override void HandlePotionEffect(Collision collision)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            SphereCollider triggerZone = this.gameObject.AddComponent<SphereCollider>();
            triggerZone.isTrigger = true;
            triggerZone.radius = this.radius;
            potionEffect = Instantiate(potionEffect, transform.position, transform.rotation); 
            potionEffect.transform.localScale = new Vector3(radius, radius, radius);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == (1 << 3))
            {
                other.GetComponent<Enemy>().SetSpeed(0.7f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == (1 << 3))
            {
                other.GetComponent<Enemy>().Resume();
            }
        }

        void Start()
        {
            this.effectDuration = 5;
        }
    }
}