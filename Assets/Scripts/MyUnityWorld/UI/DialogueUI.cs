using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class DialogueUI : BaseUI
    {
        protected override EUIState GetUIState()
        {
            return EUIState.DIALOGUE;
        }

        public void HideDialogueUI()
        {
            gameObject.SetActive(false);
        }
    }
}
