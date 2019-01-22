using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnvironmentGeneration : MonoBehaviour
{
    
    public GameObject environmentTilePrefab;

    public static IDictionary<int[], GameObject> environmentTiles;
    public int tilesWidth;
    public int tilesHeight;

    // Start is called before the first frame update
    void Start()
    {
        int halfWidth = tilesWidth / 2;
        int halfHeight = tilesHeight / 2;
        // proceedurally generate the game tiles
        for (int y = -halfHeight; y < halfHeight; y++)
        {
            for (int x = -halfWidth; x < halfWidth; x++)
            {
                Vector3 position = new Vector3(x, y, 0);
                Instantiate(environmentTilePrefab, position, transform.rotation);
            }
        }
        
    }

}
