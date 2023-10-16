using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreen : MonoBehaviour
{
    [SerializeField] private Text star;
    private int starPoint;
    private Point points;

    private void Start()
    {
        points = GameObject.Find("Character").GetComponent<Point>();
    }

    void Update()
    {
        starPoint = points.star;
        star.text = starPoint.ToString();
    }
}
