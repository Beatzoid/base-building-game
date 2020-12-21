using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursor;

    Vector3 lastFramePosition;

    void Start()
    {

    }


    void Update()
    {
        Vector3 currFramePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePositon.z = 0;

        // Update the circle cursor
        circleCursor.transform.position = currFramePositon;

        // Handle Screen Dragging
        // Right or middle mouse button
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currFramePositon;
            Camera.main.transform.Translate(diff);

        }

        // If you use "currFramePostion" here it breaks so don't do that
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }
}
