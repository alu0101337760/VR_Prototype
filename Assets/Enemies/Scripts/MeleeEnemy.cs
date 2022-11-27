using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_PrototypeOld
{
    public class MeleeEnemy : Enemy
    {
        public float attackDamage = 10f;
        public float attackDelay = 1f;
        private float attackTimer = 0f;
        override protected void Attack()
        {
            if (attackTimer > 0) attackTimer -= Time.deltaTime;
            else
            {
                attackTimer = attackDelay;
            }
        }
    }
}