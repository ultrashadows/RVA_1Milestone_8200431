using UnityEngine;
using UnityEngine.UI;

public class CanvasInRange : MonoBehaviour
{
    public Transform targetObject;
    public float activationRange = 10f;
    public Canvas canvas;

    private void Update()
    {
        if (targetObject != null && canvas != null)
        {
            float distanceToTarget = Vector3.Distance(targetObject.position, Camera.main.transform.position);

            if (distanceToTarget <= activationRange)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }
    }
}

