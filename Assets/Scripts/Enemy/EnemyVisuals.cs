using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    [RequireComponent(typeof(Animator))]
    public class EnemyVisuals : MonoBehaviour
    {
        private Animator anim;
        [HideInInspector]
        public bool isWalking = false;
        
        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        public void Walk() {
            isWalking = true;
            anim.SetBool("isWalking", true);
        }
        public void Stop() {
            isWalking = false;
            anim.SetBool("isWalking", false);
        }
        public void Die() {
            anim.SetBool("Died", true);
            anim.SetBool("isWalking", false);
        }
        public void Attack() {
            anim.SetTrigger("Attack");
            anim.SetBool("isWalking", false);
        }

        public void Reset() {
            if (anim == null) anim = GetComponent<Animator>();
            isWalking = false;
            anim.SetBool("Died", false);
            anim.SetBool("isWalking", false);
            anim.SetTrigger("Reset");
        }
    }
}