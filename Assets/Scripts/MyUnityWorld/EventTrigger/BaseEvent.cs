using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public abstract class BaseEvent : MonoBehaviour
    {
        public abstract void StartEvent(Collider2D collision);
    }
}
