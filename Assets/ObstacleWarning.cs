using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VR_PrototypeOld {
    public class ObstacleWarning : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<Enemy>().UpdatePath();
            }
        }
    }
}