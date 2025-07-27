using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class RideHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _rideRenderer;
        public SpriteRenderer RideRenderer{ get { return _rideRenderer; } }

        [Range(1, 20)][SerializeField] private float _speed;
        public float Speed { get { return _speed; } }
    }
}
