using UnityEngine;

namespace VR_Prototype
{
    public class SlowPotion : Potion
    {
        public override void StopPotionEffect() {}

        protected override void HandlePotionEffect(Collision collision)
        {
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            SphereCollider triggerZone = this.gameObject.AddComponent<SphereCollider>();
            triggerZone.isTrigger = true;
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