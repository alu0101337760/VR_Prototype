using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class Objective : Destructible
    {
        public int id;
        override public void Die()
        {
            health = 0;
            GetComponent<Renderer>().material.color = Color.black;
            EnemyPool.instance.Switch2Spawner(id);
        }

        public override void Reset() {
            health = maxHealth;
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}