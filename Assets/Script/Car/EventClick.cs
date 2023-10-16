using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClick : MonoBehaviour, IPointerClickHandler
{
    private Point character;

    private void Start()
    {
        character = GameObject.Find("Character").GetComponent<Point>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        character.setStarUp();
        this.enabled = false;
    }


}
