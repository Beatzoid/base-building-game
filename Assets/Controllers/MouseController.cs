using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursor;

    // The position of the mouse in the last frame
    Vector3 lastFramePosition;
    // The position of the mouse in the current frame
    Vector3 currFramePosition;
    // The position that the player started dragging on
    Vector3 dragStartPosition;


    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCursor();
        UpdateDragging();
        UpdateCameraMovement();

        // If you use "currFramePostion" here it breaks so don't do that
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    void UpdateCameraMovement()
    {
        // Handle Screen Dragging
        // Right or middle mouse button
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

        }
    }

    void UpdateCursor()
    {
        // Update the circle cursor
        Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCord(currFramePosition);
        Debug.Log(tileUnderMouse);
        if (tileUnderMouse != null)
        {
            circleCursor.SetActive(true);
            Vector3 cursorPosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            circleCursor.transform.position = cursorPosition;
        }
        else
        {
            circleCursor.SetActive(false);
        }
    }

    void UpdateDragging()
    {
        // Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePosition;
        }

        // End Drag
        if (Input.GetMouseButtonUp(0))
        {
            int start_x = Mathf.FloorToInt(dragStartPosition.x);
            int end_x = Mathf.FloorToInt(currFramePosition.x);
            if (end_x < start_x)
            {
                int temp = end_x;
                end_x = start_x;
                start_x = temp;
            }

            int start_y = Mathf.FloorToInt(dragStartPosition.y);
            int end_y = Mathf.FloorToInt(currFramePosition.y);
            if (end_y < start_y)
            {
                int temp = end_y;
                end_y = start_y;
                start_y = temp;
            }

            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile t = WorldController.Instance.World.GetTileAt(x, y);
                    if (t != null)
                    {
                        t.Type = Tile.TileType.Floor;
                    }
                }
            }
        }
    }
}
