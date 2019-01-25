﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration : MonoBehaviour
{
    
    public GameObject environmentTilePrefab;
    public GameObject environmentTileContainer;

    public IDictionary<int[], GameObject> coordToEnvironmentTile = new Dictionary<int[], GameObject>();
    public List<GameObject> environmentTiles = new List<GameObject>();
    
    public int tilesWidth;
    public int tilesHeight;

    
    // the static reference to the singleton instance
    public static EnvironmentGeneration instance { get; private set; }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // singleton pattern
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
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
                // add to coordinates -> tile dictionary
                int[] coords = {x, y};
                coordToEnvironmentTile.Add(coords, newTile);
                // add to tile list
                environmentTiles.Add(newTile); 
            }
        }
    }

}