﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGeneration_3d : MonoBehaviour
{
    
    // RESPONSIBLE FOR PROCEDURAL ENVIRONMENT GENERATION


    // game object references
    public GameObject environmentTilePrefab;
    public GameObject environmentTileContainer;
    

    // script references
    private EnvironmentTiles_3d eTiles;
    
    
    // static reference to the singleton instance
    public static EnvironmentGeneration_3d instance { get; private set; }

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
        eTiles = EnvironmentTiles_3d.instance;
        // generate the map
        GenerateMap();
        eTiles.SetNeighborsForTiles();
    }

    void GenerateMap() {
        int halfWidth = WorldSettings_3d.instance.tilesWidth / 2;
        int halfHeight = WorldSettings_3d.instance.tilesHeight / 2;
        // proceedurally generate the game tiles
        for (int z = -halfHeight; z < halfHeight; z++)
        {
            for (int x = -halfWidth; x < halfWidth; x++)
            {
                // create environment tile
                Vector3 position = new Vector3(x, 0, z);
                GameObject newTile = Instantiate(
                    environmentTilePrefab, 
                    position, 
                    transform.rotation, 
                    environmentTileContainer.transform
                );
                // init tile state
                newTile.GetComponent<EnvironmentTileControl_3d>().InitState(
                    Random.Range(WorldSettings_3d.MIN_EARTH, WorldSettings_3d.MAX_EARTH),
                    Random.Range(WorldSettings_3d.MIN_HEAT, WorldSettings_3d.MAX_HEAT),
                    Random.Range(WorldSettings_3d.MIN_WATER, WorldSettings_3d.MAX_WATER)
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
