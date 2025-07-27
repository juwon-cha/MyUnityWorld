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

            mOffsetX = Target.position.x;
            mOffsetX = Target.position.y;
        }

        private void OnEnable()
        {
            if (Target == null)
            {
                return;
            }

            mOffsetX = Target.position.x;
            mOffsetX = Target.position.y;
        }

        void LateUpdate()
        {
            if (Target == null)
            {
                return;
            }

            Vector3 pos = transform.position;
            pos.x = Target.position.x;
            pos.y = Target.position.y;
            transform.position = pos;
        }
    }
}
