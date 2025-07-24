using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform Target;
        private float mOffsetX;
        private float mOffsetY;

        void Start()
        {
            if (Target == null)
            {
                return;
            }

            mOffsetX = transform.position.x - Target.position.x;
            mOffsetX = transform.position.y - Target.position.y;
        }

        void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 pos = transform.position;
            pos.x = Target.position.x + mOffsetX;
            pos.y = Target.position.y + mOffsetY;
            transform.position = pos;
        }
    }
}
