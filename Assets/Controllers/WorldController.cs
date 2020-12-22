using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }

    public Sprite floorSprite;

    public World World { get; protected set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
            Debug.LogError("There should never be two world controllers");

        Instance = this;

        // Create a world with empty tiles
        World = new World();

        // Create a GameObject for each of our tiles, so they show on the screen
        for (int x = 0; x < World.Width; x++)
        {
            for (int y = 0; y < World.Height; y++)
            {
                Tile tile_data = World.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.SetParent(this.transform, true);
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

                // Add a Sprite Renderer but don't set a sprite because all the tiles
                // are empty right now
                tile_go.AddComponent<SpriteRenderer>();

                // Ensures that the tile is always above the background 
                // and visible in the camera view
                tile_go.GetComponent<SpriteRenderer>().sortingLayerName = "Tiles";

                // Link the function
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
            }
        }

        World.RandomizeTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("[OnTileTypeChanged] Unrecognized tile type");
        }
    }
}
