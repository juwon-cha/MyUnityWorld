using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyUnityWorld
{
    public class MiniGameEvent : BaseEvent
    {
        enum EMiniGameType
        {
            FLAPPY_PLANE,
            THE_STACK,
            TOPDOWN_SHOOTING,
        }

        private EMiniGameType _miniGameType;

        public override void StartEvent(Collider2D collision)
        {
            string name = collision.gameObject.name;

            if(name == "MiniGame0")
            {
                _miniGameType = EMiniGameType.FLAPPY_PLANE;
            }
            else if (name == "MiniGame1")
            {
                _miniGameType = EMiniGameType.THE_STACK;
            }
            else if (name == "MiniGame2")
            {
                _miniGameType = EMiniGameType.TOPDOWN_SHOOTING;
            }
            
            //StartMiniGame(_miniGameType);
        }

        private void StartMiniGame(EMiniGameType miniGameType)
        {
            switch(miniGameType)
            {
                case EMiniGameType.FLAPPY_PLANE:
                    SceneManager.LoadScene("FlappyPlane");
                    break;

                case EMiniGameType.THE_STACK:
                    SceneManager.LoadScene("TheStack");
                    break;

                case EMiniGameType.TOPDOWN_SHOOTING:
                    SceneManager.LoadScene("TopDownShooting");
                    break;

                default:
                    Debug.LogError("Unknown mini game type: " + _miniGameType);
                    break;
            }
        }
    }
}
