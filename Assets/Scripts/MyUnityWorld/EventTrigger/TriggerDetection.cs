using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    enum TriggerType
    {
        MINI_GAME,
        NPC,
        LEADER_BOARD,
        MIRROR
    }

    [SerializeField] private GameObject _interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _interaction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _interaction.SetActive(false);
        }
    }
}
