using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private string npc;

    private int index;
    private PlayerMovement move;
    private Animator animNPC;

    public void Start()
    {
        textComponent.text = string.Empty;
        move = GameObject.Find("Character").GetComponent<PlayerMovement>();
        animNPC = GameObject.Find(npc).GetComponent<Animator>();
        startDialogue();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(textComponent.text == lines[index])
            {
                nextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    private void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char C in lines[index].ToCharArray())
        {
            textComponent.text += C;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void nextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            animNPC.SetBool("Bicara", false);
            move.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void SetNPC(string name)
    {
        npc = name;
    }
}
