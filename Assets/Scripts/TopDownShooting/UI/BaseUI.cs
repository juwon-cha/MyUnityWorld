using System.Collections;
using System.Collections.Generic;
using TheStack;
using UnityEngine;

namespace TopDownShooting
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected UIManager _uiManager;

        protected abstract EUIState GetUIState();

        public virtual void Init(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public void SetActive(EUIState state)
        {
            gameObject.SetActive(state == GetUIState());
        }
    }
}
