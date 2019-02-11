using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTiles : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR INITIALIZING AND EVALUATING ENVIRONMENT TILES


    public float tileEvaluationSpeed;

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
        InvokeRepeating("EvaluateEnvironmentTiles", 0, tileEvaluationSpeed);
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
            // Debug.Log("tile not found at coordinate x:" + coordinate[0] + " y:" + coordinate[1]);
        }
        return tile;
    }


    public void EvaluateEnvironmentTiles() {
        foreach (GameObject tile in environmentTiles) {
            EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
            // evaluate current tile and neighbors
            EvaluateTileAndNeighbors(
                tileControl,
                tileControl.GetTileNeighborsAsList()
            );
        }
        foreach (GameObject tile in environmentTiles) {
            EnvironmentTileControl tileControl = tile.GetComponent<EnvironmentTileControl>();
            tileControl.ApplyUpdateFromTempValues();
            tileControl.SetAppearanceFromState();
        }
    }

    private void EvaluateTileAndNeighbors(
        EnvironmentTileControl mTileControl, 
        List<EnvironmentTileControl> neighbors
    ) {
        float waterFlowCoefficient = mTileControl.GetWaterFlowCoefficient();
        float totalWaterDiff = 0;
        int participatingNeighbors = 0;
        foreach (EnvironmentTileControl nTileControl in neighbors) {
            // calculate total water diff
            if(mTileControl.water > nTileControl.water) {
                float waterDiff = mTileControl.water - nTileControl.water;
                totalWaterDiff += waterDiff;
                participatingNeighbors += 1;
            }
        }
        // calculate total water available for transfer
        float waterAvailForTransfer = ((totalWaterDiff/2)/participatingNeighbors) * waterFlowCoefficient;
        foreach (EnvironmentTileControl nTileControl in neighbors) {
            // calculate water trasfer amount and commit transfer
            if(mTileControl.water > nTileControl.water) {
                float waterDiff = mTileControl.water - nTileControl.water;
                float waterToTransfer = (waterDiff / totalWaterDiff) * waterAvailForTransfer;
                nTileControl.updateWaterAmount += waterToTransfer;
                mTileControl.updateWaterAmount -= waterToTransfer;

                // DEBUG: should never see water go below 0 or above limit
                if (waterToTransfer + nTileControl.water < 0 || waterToTransfer + nTileControl.water > 200) {
                    Debug.LogFormat(
                        "=====================>"
                        + "\nmain tile x={0} y={1}\nneighbor tile x={2} y={3}"
                        + "\nparticipating neighbors: {4}" 
                        + "\ntotal water diff: {5}"
                        + "\nwater available for transfer: {6}"
                        + "\nlocal water diff: {7}"
                        + "\nlocal water to transfer: {8}",
                        mTileControl.gameObject.transform.position.x,
                        mTileControl.gameObject.transform.position.y,
                        nTileControl.gameObject.transform.position.x,
                        nTileControl.gameObject.transform.position.y,
                        participatingNeighbors,
                        totalWaterDiff,
                        waterAvailForTransfer,
                        waterDiff,
                        waterToTransfer
                    );
                }

            }
        }
    } 


}
