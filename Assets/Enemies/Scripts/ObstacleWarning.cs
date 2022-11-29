using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_Prototype {
    public class ObstacleWarning : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().OnPathInterrupted(transform);
            }
        }
    }
}