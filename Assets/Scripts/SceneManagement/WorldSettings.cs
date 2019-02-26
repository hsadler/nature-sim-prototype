using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings : MonoBehaviour
{

    // game variable settings
    public int tilesWidth;
    public int tilesHeight;
    public float tileEvaluationSpeed;


    // min and max for environment tile state properties
    public const float MIN_EARTH = 0;
    public const float MAX_EARTH = 100;
    public const float MIN_HEAT = 0;
    public const float MAX_HEAT = 100;
    public const float MIN_WATER = 0;
    public const float MAX_WATER = 100;

    
    // flow coefficients
    public const float EARTH_FLOW_COEFFICIENT = 0.1f;
    public const float HEAT_FLOW_COEFFICIENT = 0.3f;
    public const float WATER_FLOW_COEFFICIENT = 0.1f;


    // the static reference to the singleton instance
    public static WorldSettings instance { get; private set; }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // singleton pattern
        if(instance == null) {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}
