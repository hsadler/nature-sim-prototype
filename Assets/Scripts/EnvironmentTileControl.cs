using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileControl : MonoBehaviour
{
    
    
    // tile state properties
    public string earthType;
    public float elevation;
    public float heat;
    public float water;

    
    // neighboring environment tiles
    public GameObject neighborUp;
    public GameObject neighborRight;
    public GameObject neighborDown;
    public GameObject neighborLeft;


    // config
    public float maxElevation;
    public float minElevation;
    public float maxHeat;
    public float minHeat;
    public float maxWater;
    public float minWater;


    // script references
    private EnvironmentSpriteList eSpriteList;

    
    // Start is called before the first frame update
    void Start() {

        // set script references
        eSpriteList = EnvironmentSpriteList.instance;
        
        // MOCK:
        InitMockState();
        InitMockAppearance();
        
        // REAL:
        // InitState();
        // SetAppearanceFromState();

    }

    void InitState() {
        
    }

    // Update is called once per frame
    void Update() {
        SetAppearanceFromState();    
    }

    void SetAppearanceFromState() {
        // TODO:
    }

    
    // MOCK METHODS
    void InitMockState() {
        List<string> earthNames = new List<string>(eSpriteList.nameToEarthSprite.Keys);
        earthType = earthNames[Random.Range(0, earthNames.Count)];
        elevation = Random.Range(minElevation, maxElevation);
        heat = Random.Range(minHeat, maxHeat);
        water = Random.Range(minWater, maxWater);
    }
    
    void InitMockAppearance() {
        Sprite earthSprite = eSpriteList.GetEarthSpriteByName(earthType);
        GetComponent<SpriteRenderer>().sprite = earthSprite;
    }

}
