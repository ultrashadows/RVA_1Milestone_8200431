using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegOriginalPos : MonoBehaviour
{
    Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
    }

    public void ResetToOriginalPosition()
    {
        transform.position = originalPosition;
    }
}
