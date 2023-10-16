using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcInteractable : MonoBehaviour
{

    public GameObject dialogue;
    private Animator anim;
    private string nama;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        nama = gameObject.name;
    }

    public void Interact()
    {
        anim.SetBool("Bicara", true);
        dialogue.GetComponent<DialogueBox>().SetNPC(nama);
        dialogue.SetActive(true);
    }

    public void OutRange()
    {
        dialogue.SetActive(false);
    }
}
