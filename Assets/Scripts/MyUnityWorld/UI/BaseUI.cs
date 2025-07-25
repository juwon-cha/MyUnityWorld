using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected abstract EUIState GetUIState();

        public void SetActive(EUIState state)
        {
            gameObject.SetActive(state == GetUIState());
        }
    }
}
