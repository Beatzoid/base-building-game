using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursor;

    Vector3 lastFramePosition;
    Vector3 dragStartPosition;

    void Start()
    {
    }


    void Update()
    {
        Vector3 currFramePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePositon.z = 0;

        // Update the circle cursor
        Tile tileUnderMouse = GetTileAtWorldCord(currFramePositon);
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

        // Start Drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currFramePositon;
        }

        // End Drag
        if (Input.GetMouseButtonUp(0))
        {
            int start_x = Mathf.FloorToInt(dragStartPosition.x);
            int end_x = Mathf.FloorToInt(currFramePositon.x);
            if (end_x < start_x)
            {
                int temp = end_x;
                end_x = start_x;
                start_x = temp;
            }

            int start_y = Mathf.FloorToInt(dragStartPosition.y);
            int end_y = Mathf.FloorToInt(currFramePositon.y);
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

    Tile GetTileAtWorldCord(Vector3 cord)
    {
        int x = Mathf.FloorToInt(cord.x);
        int y = Mathf.FloorToInt(cord.y);

        return WorldController.Instance.World.GetTileAt(x, y);
    }
}
