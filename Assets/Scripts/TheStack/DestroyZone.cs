using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheStack
{
    public class DestroyZone : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.Equals("Rubble") || collision.gameObject.name.Equals("Block"))
            {
                Destroy(collision.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.Equals("Block"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
