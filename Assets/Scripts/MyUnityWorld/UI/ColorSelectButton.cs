using MyUnityWorld;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject _selectedIndicator;

    public bool IsInteractable = true;

    public void SetInteractable(bool isInteractable)
    {
        IsInteractable = isInteractable;
        _selectedIndicator.SetActive(isInteractable);
    }
}
