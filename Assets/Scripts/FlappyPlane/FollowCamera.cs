using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlappyPlane
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform Target;
        private float mOffsetX;

        void Start()
        {
            if (Target == null)
            {
                return;
            }

            mOffsetX = transform.position.x - Target.position.x;
        }

        // Update is called once per frame
        void Update()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 pos = transform.position;
            pos.x = Target.position.x + mOffsetX;
            transform.position = pos;
        }
    }
}
