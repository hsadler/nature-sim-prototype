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


    // script references
    private EnvironmentSpriteList eSpriteList;

    
    // Start is called before the first frame update
    void Start() {

        // set script references
        eSpriteList = EnvironmentSpriteList.instance;
        
        // InitState();
        // SetAppearanceFromState();
        
        // MOCK:
        InitMockState();
        InitMockAppearance();

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
        // float rand
    }
    
    void InitMockAppearance() {
        // set sprite to random earth sprite
        List<Sprite> earthSpriteList = EnvironmentSpriteList.instance.earthSpriteList;
        Sprite randEarth = earthSpriteList[
            Random.Range(0, earthSpriteList.Count)
        ];
        GetComponent<SpriteRenderer>().sprite = randEarth;
    }

}
