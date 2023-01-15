using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Lightning : Potion
    {
        public int lightningRange = 5;
        public int lightningDamage = 1;
        public override void StopPotionEffect(){      }

        protected override void HandlePotionEffect(Collision collision)
        {
            Enemy[] aliveEnemies = EnemyPool.instance.GetLivingEnemies();
            int randomIndex = Random.Range(0, aliveEnemies.Length);

            Vector3 center = aliveEnemies[randomIndex].transform.position;
            aliveEnemies[randomIndex].Die();
            Collider[] collisions = Physics.OverlapSphere(center, lightningRange, 1 << 3);

            for (int i = 0; i < collisions.Length; i++)
            {
                Debug.Log(collisions[i].gameObject.name);
                collisions[i].gameObject.GetComponent<Enemy>().TakeDamage(lightningDamage);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            this.effectDuration = 0;
        }

    }
}