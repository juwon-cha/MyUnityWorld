using UnityEngine;

namespace TheStack
{
    public abstract class BaseUI : MonoBehaviour
    {
        private protected UIManager mUIManager;

        public virtual void Init(UIManager uiManager)
        {
            this.mUIManager = uiManager;
        }

        protected abstract EUIState GetUIState();
        public void SetActive(EUIState state)
        {
            gameObject.SetActive(GetUIState() == state);
        }
    }
}
