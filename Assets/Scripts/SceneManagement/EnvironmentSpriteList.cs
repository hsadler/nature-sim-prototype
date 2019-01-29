using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpriteList : MonoBehaviour
{

    // HOLDS SPRITE TYPES FOR THE GAME ENVIRONMENT

    
    // environment earth names
    public const string DIRT = "Dirt";
    public const string DRY_DIRT = "Dry Dirt";
    public const string ICE_DIRT = "Ice Dirt";
    public const string GRASS = "Grass";
    public const string GRASS_PEBBLES = "Grass Pebbles";
    public const string DRY_GRASS = "Dry Grass";
    public const string DRY_GRASS_MUSHROOMS = "Dry Grass Mushrooms";
    public const string SAND = "Sand";
    public const string DRY_SAND = "Dry Sand";

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


    // environment water names
    public const string WATER = "Water";
    public const string SNOW = "Snow"; 
    public const string ICE = "Ice"; 
    
    // environment water sprites
    public Sprite water;
    public Sprite snow;
    public Sprite ice;

    
    // sprite lists by type
    public List<Sprite> earthSpriteList = new List<Sprite>();
    public List<Sprite> waterSpriteList = new List<Sprite>();

    
    // sprite dict keyed by sprite name
    public IDictionary<string, Sprite> nameToEarthSprite = new Dictionary<string, Sprite>();
    public IDictionary<string, Sprite> nameToWaterSprite = new Dictionary<string, Sprite>();


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
            // DontDestroyOnLoad(gameObject);
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
        // assign earth sprites to dict keyed by name
        IDictionary<string, Sprite> ntes = nameToEarthSprite;
        ntes.Add(DIRT, dirt);
        ntes.Add(DRY_DIRT, dryDirt);
        ntes.Add(ICE_DIRT, iceDirt);
        ntes.Add(GRASS, grass);
        ntes.Add(GRASS_PEBBLES, grassPebbles);
        ntes.Add(DRY_GRASS, dryGrass);
        ntes.Add(DRY_GRASS_MUSHROOMS, dryGrassMushrooms);
        ntes.Add(SAND, sand);
        ntes.Add(DRY_SAND, drySand);
        // assign earth sprites to dict keyed by name
        IDictionary<string, Sprite> ntws = nameToWaterSprite;
        ntws.Add(WATER, water);
        ntws.Add(SNOW, snow);
        ntws.Add(ICE, ice);
    }

    public Sprite GetEarthSpriteByName(string name) {
        Sprite sprite = null;
        if(nameToEarthSprite.ContainsKey(name)) {
            sprite = nameToEarthSprite[name];
        }
        return sprite;
    }

    public Sprite GetWaterSpriteByName(string name) {
        Sprite sprite = null;
        if(nameToWaterSprite.ContainsKey(name)) {
            sprite = nameToWaterSprite[name];
        }
        return sprite;
    }

}
