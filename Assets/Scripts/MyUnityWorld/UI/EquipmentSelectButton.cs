using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject _selectedIndicator;

    public bool IsInteractable = true;

    public void SetInteractable(bool isInteractable)
    {
        IsInteractable = isInteractable;
        _selectedIndicator.SetActive(isInteractable);
    }
}
