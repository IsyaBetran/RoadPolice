using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerMovement move;
    public bool sudah = false;

    private void Start()
    {
        move = gameObject.GetComponent<PlayerMovement>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach(Collider collider in colliderArray)
            {
                if(collider.TryGetComponent(out NpcInteractable npcInteract))
                {
                    npcInteract.Interact();
                    sudah = true;
                    move.enabled = false;
                }
            }
        }

    }

    public NpcInteractable GetUIInteract()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NpcInteractable npcInteract))
            {
                return npcInteract;
            }
        }
        return null;
    }
}
