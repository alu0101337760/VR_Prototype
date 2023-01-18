using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Destructible : MonoBehaviour
    {
        public float health = 1;
        public float maxHealth = 1;
        public virtual void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            gameObject.SetActive(false);
            enabled = false;
        }

        public virtual void Reset()
        {
            health = maxHealth;
            gameObject.SetActive(true);
            enabled = true;
        }
    }
}