using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    
    // RESPONSIBLE FOR PROCEDURAL ENVIRONMENT GENERATION


    // TODO: put this in a global config
    // config

    // min and max for state properties
    private const float MIN_EARTH = 0;
    private const float MAX_EARTH = 200;
    private const float MIN_HEAT = 0;
    private const float MAX_HEAT = 200;
    private const float MIN_WATER = 0;
    private const float MAX_WATER = 200;


    // game object references
    public GameObject environmentTilePrefab;
    public GameObject environmentTileContainer;
    

    // script references
    private EnvironmentTiles eTiles;
    
    
    // config
    public int tilesWidth;
    public int tilesHeight;
    
    
    // static reference to the singleton instance
    public static EnvironmentGeneration instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake() {
        // singleton pattern
        if(instance == null) {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start() {
        // set references
        eTiles = EnvironmentTiles.instance;
        // generate the map
        GenerateMap();
        eTiles.SetNeighborsForTiles();
    }

    void GenerateMap() {
        int halfWidth = tilesWidth / 2;
        int halfHeight = tilesHeight / 2;
        // proceedurally generate the game tiles
        for (int y = -halfHeight; y < halfHeight; y++)
        {
            for (int x = -halfWidth; x < halfWidth; x++)
            {
                // create environment tile
                Vector3 position = new Vector3(x, y, 0);
                GameObject newTile = Instantiate(
                    environmentTilePrefab, 
                    position, 
                    transform.rotation, 
                    environmentTileContainer.transform
                );
                newTile.GetComponent<EnvironmentTileControl>().InitState(
                    Random.Range(MIN_EARTH, MAX_EARTH),
                    Random.Range(MIN_HEAT, MAX_HEAT),
                    Random.Range(MIN_WATER, MAX_WATER)
                );
                // add to coordinates -> tile dictionary
                string coordsKey = eTiles.GetFormattedCoordinateFromTile(newTile);
                eTiles.coordToEnvironmentTile.Add(coordsKey, newTile);
                // add to tile list
                eTiles.environmentTiles.Add(newTile); 
            }
        }
    }

}
