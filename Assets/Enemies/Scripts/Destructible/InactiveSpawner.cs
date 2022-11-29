using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class InactiveSpawner : Destructible
    {
        public override void Die()
        {
            EnemyPool.instance.TurnObjectiveIntoSpawner(GetComponent<Spawner>().id);
            enabled = false;
        }
    }
}