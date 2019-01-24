using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpriteList : MonoBehaviour
{

    // the static reference to the singleton instance
    public static EnvironmentSpriteList instance { get; private set; }

    // environment sprite list
    public Sprite dirt;
    public Sprite dryDirt;

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

}
