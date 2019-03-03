using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRegistry_3d : MonoBehaviour
{


    // game object references
    public GameObject tileInfoText;

    
    // the static reference to the singleton instance
    public static GameObjectRegistry_3d instance { get; private set; }

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
