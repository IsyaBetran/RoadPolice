using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public int star = 0;

    public void setStarUp()
    {
        star++;
    }

    public void setStarDown()
    {
        star--;
    }
}
