using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class EnemyHealth : Destructible
    {
        public override void Die()
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}