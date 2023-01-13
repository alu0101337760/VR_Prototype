using UnityEngine;

namespace VR_Prototype
{
    public class ExplosivePotion : Potion
    {
        protected override void HandlePotionEffect(Collision collision)
        {
            Vector3 center = transform.position;
            Collider[] collisions = Physics.OverlapSphere(center, this.radius);

            // filter collisions (too lazy to use collision layers, will check in the future)
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].tag == "Enemy")
                {
                    collisions[i].gameObject.GetComponent<Enemy>().Die();
                }
            }
        }

        private void Start()
        {
            this.effectDuration = 0;
        }
    }
}