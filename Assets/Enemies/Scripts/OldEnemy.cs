using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_PrototypeOld
{
    public abstract class OldEnemy : MonoBehaviour
    {
        public float HP = 100;
        //public float speed = 1;
        public float LifeTime = 10;
        //public Transform target;

        void Update()
        {
            //transform.Translate((target.position - transform.position).normalized * speed * Time.deltaTime);
            GetComponent<Renderer>().material.color = Color.Lerp(Color.black, Color.white, HP / 100);
            HP -= LifeTime == 0 ? 0 : 100*Time.deltaTime/LifeTime;
            if (HP <= 0)
            {
                Die();
            }
        }

        public void TakeDamage(float damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}