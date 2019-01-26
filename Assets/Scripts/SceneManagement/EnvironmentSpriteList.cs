using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpriteList : MonoBehaviour
{

    // SIMPLY HOLDS SPRITE TYPES FOR THE GAME ENVIRONMENT

    // environment earth sprites
    public Sprite dirt;
    public Sprite dryDirt;
    public Sprite iceDirt;
    public Sprite grass;
    public Sprite grassPebbles;
    public Sprite dryGrass;
    public Sprite dryGrassMushrooms;
    public Sprite sand;
    public Sprite drySand;
    
    // environment water sprites
    public Sprite water;
    public Sprite snow;
    public Sprite ice;

    // sprite lists by type
    public List<Sprite> earthSpriteList = new List<Sprite>();
    public List<Sprite> waterSpriteList = new List<Sprite>();

    // the static reference to the singleton instance
    public static EnvironmentSpriteList instance { get; private set; }

    
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
        Init();
    }

    void Init() {
        // assign earth sprites
        List<Sprite> e = earthSpriteList;
        e.Add(dirt);
        e.Add(dryDirt);
        e.Add(iceDirt);
        e.Add(grass);
        e.Add(grassPebbles);
        e.Add(dryGrass);
        e.Add(dryGrassMushrooms);
        e.Add(sand);
        e.Add(drySand);
        // assign water sprites
        List<Sprite> w = waterSpriteList;
        w.Add(water);
        w.Add(snow);
        w.Add(ice);
    }

}
