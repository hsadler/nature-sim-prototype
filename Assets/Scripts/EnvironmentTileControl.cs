using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileControl : MonoBehaviour
{
    

    // HOLDS ENVIRONMENT TILE STATE AND SETS APPEARANCE

    
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
    private const float MIN_ELEVATION = 0;
    private const float MIN_HEAT = 0;
    private const float MIN_WATER = 0;
    private const float SAND_WATER_THRESHOLD = 20;
    private const float DRY_DIRT_WATER_THRESHOLD = 40;
    private const float DIRT_WATER_THRESHOLD = 60;


    // game object references
    public GameObject tileInfoText;
    private GameObject highlightFrame;


    // script references
    private EnvironmentSpriteList eSpriteList;
    private GameObjectRegistry goRegistry;

    
    // Start is called before the first frame update
    void Start() {

        // set game object references
        highlightFrame = transform.Find("HighlightFrame").gameObject;

        // set script references
        eSpriteList = EnvironmentSpriteList.instance;
        goRegistry = GameObjectRegistry.instance;
        
        // MOCK:
        InitMockState();
        
        // REAL:
        // InitState();

    }

    void InitState() {
        
    }

    // Update is called once per frame
    void Update() {
        SetAppearanceFromState();    
    }

    void SetAppearanceFromState() {
        if(water < SAND_WATER_THRESHOLD) {
            earthType = EnvironmentSpriteList.DRY_SAND;
        } else if(water < DRY_DIRT_WATER_THRESHOLD) {
            earthType = EnvironmentSpriteList.SAND;
        } else if(water < DIRT_WATER_THRESHOLD) {
            earthType = EnvironmentSpriteList.DRY_DIRT;
        } else {
            earthType = EnvironmentSpriteList.DIRT;
        }
        Sprite earthSprite = eSpriteList.GetEarthSpriteByName(earthType);
        GetComponent<SpriteRenderer>().sprite = earthSprite;
    }

    
    // MOCK METHODS
    void InitMockState() {
        elevation = Random.Range(MIN_ELEVATION, 200);
        heat = Random.Range(MIN_HEAT, 200);
        water = Random.Range(MIN_WATER, 200);
    }


    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter()
    {
        highlightFrame.SetActive(true);
    }

    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    void OnMouseOver()
    {
        goRegistry.tileInfoText.GetComponent<TileInfoTextControl>().UpdateInfo(gameObject);
    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit()
    {
        highlightFrame.SetActive(false);   
    }

}
