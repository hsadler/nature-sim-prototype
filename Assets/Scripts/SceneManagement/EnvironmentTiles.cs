using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTiles : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR ENVIRONMENT TILES


    public IDictionary<string, GameObject> coordToEnvironmentTile = new Dictionary<string, GameObject>();
    public List<GameObject> environmentTiles = new List<GameObject>();
    
    
    // the static reference to the singleton instance
    public static EnvironmentTiles instance { get; private set; }

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

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        InvokeRepeating("UpdateEnvironmentTiles", 0, 1);
    }


    public void SetNeighborsForTiles() {
        foreach (GameObject tile in environmentTiles)
        {
           SetTileNeighbors(tile); 
        }
    }

    public string GetFormattedCoordinateFromTile(GameObject tile) {
        return string.Format(
            "{0},{1}", 
            tile.transform.position.x, 
            tile.transform.position.y
        );
    }

    public string GetFormattedCoordinateFromArray(int[] coordinate) {
        return string.Format(
            "{0},{1}", 
            coordinate[0], 
            coordinate[1]
        );
    }

    private void SetTileNeighbors(GameObject tile) {
        EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
        int[] tileCoord = {
            (int)tile.transform.position.x, 
            (int)tile.transform.position.y
        };
        int[] upCoord = {tileCoord[0], tileCoord[1] + 1};
        tileControl.neighborUp = GetTileAtCoordinate(upCoord);
        int[] rightCoord = {tileCoord[0] + 1, tileCoord[1]};
        tileControl.neighborRight = GetTileAtCoordinate(rightCoord);
        int[] downCoord = {tileCoord[0], tileCoord[1] - 1};
        tileControl.neighborDown = GetTileAtCoordinate(downCoord);
        int[] leftCoord = {tileCoord[0] - 1, tileCoord[1]};
        tileControl.neighborLeft = GetTileAtCoordinate(leftCoord);
    }

    private GameObject GetTileAtCoordinate(int[] coordinate) {
        GameObject tile = null;
        string formattedCoords = GetFormattedCoordinateFromArray(coordinate);
        if(coordToEnvironmentTile.ContainsKey(formattedCoords)) {
            tile = coordToEnvironmentTile[formattedCoords];
        } else {
            // should only occur at the edges of the map
            Debug.Log("tile not found at coordinate x:" + coordinate[0] + " y:" + coordinate[1]);
        }
        return tile;
    }


    private void UpdateEnvironmentTiles() {
        foreach (GameObject tile in environmentTiles) {
            EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
            // only update right and down neigbors so that neighbor relationships 
            // are only evaluated once
            GameObject rightN = tileControl.neighborRight;
            UpdateTilesFromTileRelationship(tile, rightN);
            GameObject downN = tileControl.neighborDown;
            UpdateTilesFromTileRelationship(tile, downN);
        }
        foreach (GameObject tile in environmentTiles) {
            EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
            tileControl.SetAppearanceFromState();
        }
    }

    private void UpdateTilesFromTileRelationship(GameObject tile1, GameObject tile2) {
        // TODO
        // Debug.Log("updating from tile relationship");
    }

}
