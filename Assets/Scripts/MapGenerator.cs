using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Tile baseTile;

    private const int MAP_SIZE = 5;

    // Use this for initialization
    private void Start() {
        for (int x = 0; x < MAP_SIZE; x++) {
            for (int y = 0; y < MAP_SIZE; y++) {
                var instance = Instantiate(baseTile, new Vector2(x, y), Quaternion.identity);
                instance.transform.parent = transform;
            }
        }
    }
}