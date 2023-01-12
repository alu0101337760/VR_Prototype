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
            GetComponent<Renderer>().material.color = Color.black;
            EnemyPool.instance.Switch2Spawner(id);
        }
    }
}