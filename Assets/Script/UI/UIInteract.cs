using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteract : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    [SerializeField] private PlayerInteract player;
    private bool sudah;

    private void Start()
    {
        sudah = player.sudah;
    }

    private void Update()
    {
        if (player.GetUIInteract() != null && !sudah)
        {
            Show();
        }
        else
        {
            Hide();
            player.sudah = false;
        }
    }

    private void Show()
    {
        interactUI.SetActive(true);
    }

    private void Hide()
    {
        interactUI.SetActive(false);
    }
}
