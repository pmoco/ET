using UnityEngine;
using UnityEngine.Tilemaps;

public class TileChangeController : MonoBehaviour
{
    public Tilemap tilemap; // Reference to the Tilemap component

    public TileBase newTile; // The new tile to replace the old one

    // Function to change the tile at given grid coordinates
    public void ChangeTile(Vector3Int tilePosition)
    {
        // Get the current tile at the specified position
        TileBase currentTile = tilemap.GetTile(tilePosition);

        // Check if the current tile is not null (i.e., there is a tile at the position)
        if (currentTile != null)
        {
            // Set the new tile at the specified position
            tilemap.SetTile(tilePosition, newTile);
        }
    }
}
