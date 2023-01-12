using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Prototype
{
    public class BulletBehaviour : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}