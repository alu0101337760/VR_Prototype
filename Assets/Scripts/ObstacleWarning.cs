using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype {
    public class ObstacleWarning : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnPathInterrupted(transform);
            }
        }
    }
}