using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardUI : MonoBehaviour
{
    [SerializeField] private Button _flappyPlaneButton;
    [SerializeField] private Button _theStackButton;
    [SerializeField] private Button _TopdownButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _flappyPlaneButton.onClick.AddListener(OnClickFlappyPlaneButton);
        _theStackButton.onClick.AddListener(OnClickTheStackButton);
        _TopdownButton.onClick.AddListener(OnClickTopDownButton);
        _exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickFlappyPlaneButton()
    {

    }

    public void OnClickTheStackButton()
    {
    }

    public void OnClickTopDownButton()
    {
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }
}
