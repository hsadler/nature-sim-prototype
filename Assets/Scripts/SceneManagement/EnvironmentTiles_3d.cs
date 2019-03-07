using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTiles_3d : MonoBehaviour
{
    
    // DATASTORE AND SERVICE FOR INITIALIZING AND EVALUATING ENVIRONMENT TILES


    public IDictionary<string, GameObject> coordToEnvironmentTile = new Dictionary<string, GameObject>();
    public List<GameObject> environmentTiles = new List<GameObject>();
    
    
    // the static reference to the singleton instance
    public static EnvironmentTiles_3d instance { get; private set; }

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
        // TODO: uncomment when ready
        InvokeRepeating(
            "EvaluateEnvironmentTiles", 
            0, 
            WorldSettings_3d.instance.tileEvaluationSpeed
        );
    }


    public void SetNeighborsForTiles() {
        foreach (GameObject tile in environmentTiles) {
           SetTileNeighbors(tile); 
        }
    }

    public string GetFormattedCoordinateFromTile(GameObject tile) {
        return string.Format(
            "{0},{1}", 
            tile.transform.position.x, 
            tile.transform.position.z
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
        EnvironmentTileControl_3d tileControl = tile.GetComponent<EnvironmentTileControl_3d>();
        int[] tileCoord = {
            (int)tile.transform.position.x, 
            (int)tile.transform.position.z
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
            EnvironmentTileControl_3d tileControl = tile.GetComponent<EnvironmentTileControl_3d>();
            // evaluate current tile and neighbors
            EvaluateTileAndNeighbors(
                tileControl,
                tileControl.GetTileNeighborsAsList()
            );
        }
        foreach (GameObject tile in environmentTiles) {
            EnvironmentTileControl_3d tileControl = tile.GetComponent<EnvironmentTileControl_3d>();
            tileControl.ApplyUpdateFromTempValues();
            tileControl.SetAppearanceFromState();
        }
    }

    private void EvaluateTileAndNeighbors(
        EnvironmentTileControl_3d mTileControl, 
        List<EnvironmentTileControl_3d> neighbors
    ) {
        // water
        EvaluateTileAndNeighborsWater(mTileControl, neighbors);
        // TODO: heat
        // EvaluateTileAndNeighborsHeat(mTileControl, neighbors);
    } 

    private void EvaluateTileAndNeighborsWater(
        EnvironmentTileControl_3d mTileControl, 
        List<EnvironmentTileControl_3d> neighbors
    ) {
        float waterFlowCoefficient = mTileControl.GetWaterFlowCoefficient();
        float totalWaterDiff = 0;
        int participatingNeighbors = 0;
        foreach (EnvironmentTileControl_3d nTileControl in neighbors) {
            // calculate total water height diff
            float mWaterHeight = mTileControl.earth + mTileControl.water;
            float nWaterHeight = nTileControl.earth + nTileControl.water;
            if(mTileControl.water > 0 && mWaterHeight > nWaterHeight) {
                float waterDiff = mWaterHeight - nWaterHeight;
                totalWaterDiff += waterDiff;
                participatingNeighbors += 1;
            }
        }
        // calculate total water available for transfer
        float waterAvailForTransfer = ((totalWaterDiff/2)/participatingNeighbors) * waterFlowCoefficient;
        foreach (EnvironmentTileControl_3d nTileControl in neighbors) {
            // calculate water trasfer amount and commit transfer
            float mWaterHeight = mTileControl.earth + mTileControl.water;
            float nWaterHeight = nTileControl.earth + nTileControl.water;
            if(mTileControl.water > 0 && mWaterHeight > nWaterHeight) {
                float waterDiff = mWaterHeight - nWaterHeight;
                float waterToTransfer = (waterDiff / totalWaterDiff) * waterAvailForTransfer;
                nTileControl.updateWaterAmount += waterToTransfer;
                mTileControl.updateWaterAmount -= waterToTransfer;

                // DEBUG: should never see water go below 0 or above limit
                bool printDebug = false;
                if (printDebug && (waterToTransfer + nTileControl.water < 0 || waterToTransfer + nTileControl.water > 200)) {
                    Debug.LogFormat(
                        "=====================>"
                        + "\nmain tile x={0} y={1}\nneighbor tile x={2} y={3}"
                        + "\nparticipating neighbors: {4}" 
                        + "\ntotal water diff: {5}"
                        + "\nwater available for transfer: {6}"
                        + "\nlocal water diff: {7}"
                        + "\nlocal water to transfer: {8}",
                        mTileControl.gameObject.transform.position.x,
                        mTileControl.gameObject.transform.position.z,
                        nTileControl.gameObject.transform.position.x,
                        nTileControl.gameObject.transform.position.z,
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

    private void EvaluateTileAndNeighborsHeat(
        EnvironmentTileControl_3d mTileControl, 
        List<EnvironmentTileControl_3d> neighbors
    ) {
        // TODO
    }

}
