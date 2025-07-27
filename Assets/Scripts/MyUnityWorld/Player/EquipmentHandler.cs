using UnityEngine;
using UnityEngine.UI;

namespace MyUnityWorld
{
    public class EquipmentHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _equipmentRenderer;
        public SpriteRenderer EquipmentRenderer { get { return _equipmentRenderer; } }
    }
}
