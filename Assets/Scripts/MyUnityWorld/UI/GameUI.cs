using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUnityWorld
{
    public class GameUI : BaseUI
    {
        protected override EUIState GetUIState()
        {
            return EUIState.GAME;
        }
    }
}
