using UnityEngine;
using System.Collections;
using System;

public class Tile
{
    public enum TileType { Empty, Floor };

    TileType type = TileType.Empty;

    Action<Tile> cbTileTypeChanged;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            TileType oldType = type;
            type = value;

            /* If there is a function and
            there is actually a change in the type
            call all functions registered
            to the TileTypeChanged callback
            */
            if (cbTileTypeChanged != null && oldType != type)
                cbTileTypeChanged(this);
        }
    }

    LooseObject looseObject;
    InstalledObject installedObject;

    World world;
    int x;
    public int X
    {
        get
        {
            return x;
        }
    }

    int y;
    public int Y
    {
        get
        {
            return y;
        }
    }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback)
    {
        cbTileTypeChanged -= callback;
    }
}
