using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TheStack
{
    public class HomeUI : BaseUI
    {
        private Button mStartBtn;
        private Button mExitBtn;

        protected override EUIState GetUIState()
        {
            return EUIState.HOME;
        }

        public override void Init(UIManager uiManager)
        {
            base.Init(uiManager);

            mStartBtn = transform.Find("StartBtn").GetComponent<Button>();
            mExitBtn = transform.Find("ExitBtn").GetComponent<Button>();

            // 버튼 클릭 이벤트 등록
            mStartBtn.onClick.AddListener(OnClickStartBtn);
            mExitBtn.onClick.AddListener(OnClickExitBtn);
        }

        private void OnClickStartBtn()
        {
            mUIManager.OnClickStart();
        }

        private void OnClickExitBtn()
        {
            mUIManager.OnClickExit();
        }
    }
}
