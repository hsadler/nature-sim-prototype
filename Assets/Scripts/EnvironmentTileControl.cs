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
    private const float MAX_ELEVATION = 200;
    private const float MIN_HEAT = 0;
    private const float MAX_HEAT = 200;
    private const float MIN_WATER = 0;
    private const float MAX_WATER = 200;

    private const float SAND_WATER_THRESHOLD = 20;
    private const float DRY_DIRT_WATER_THRESHOLD = 40;
    private const float DIRT_WATER_THRESHOLD = 60;
    private const float STANDING_WATER_THRESHOLD = 80;

    private const float MIN_SHADOW_OVERLAY_ALPHA = 0;
    private const float MAX_SHADOW_OVERLAY_ALPHA = 0.65f;
    private const float MIN_WATER_OVERLAY_ALPHA = 0.1f;
    private const float MAX_WATER_OVERLAY_ALPHA = 0.9f;


    // game object references
    public GameObject tileInfoText;
    private GameObject highlightFrame;
    private GameObject shadowOverlay;
    private GameObject waterOverlay;


    // script references
    private EnvironmentSpriteList eSpriteList;
    private GameObjectRegistry goRegistry;

    
    // Start is called before the first frame update
    void Start() {

        // set game object references
        highlightFrame = transform.Find("HighlightFrame").gameObject;
        shadowOverlay = transform.Find("ShadowOverlay").gameObject;
        waterOverlay = transform.Find("WaterOverlay").gameObject;

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
        // TODO: these updates may not need to be done every tick
        // may want to do them per longer time intervals
        SetAppearanceFromState();    
    }

    void SetAppearanceFromState() {

        // TODO: decompose this mofo 

        // set earth sprite based on water amount
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
        // set shadow overlay transparency based on elevation
        float shadowAlphaRatio = 1 - (elevation / MAX_ELEVATION);
        float shadownAlpha = (MAX_SHADOW_OVERLAY_ALPHA - MIN_SHADOW_OVERLAY_ALPHA) * 
            shadowAlphaRatio + MIN_SHADOW_OVERLAY_ALPHA;
        SpriteRenderer shadowSr = shadowOverlay.GetComponent<SpriteRenderer>();
        Color shadowColor = shadowSr.color;
        shadowColor.a = shadownAlpha;
        shadowSr.color = shadowColor;
        // set water overlay tranparency based on water content
        SpriteRenderer waterSr = waterOverlay.GetComponent<SpriteRenderer>();
        Color waterColor = waterSr.color;
        float waterAlpha = 0;
        if(water > STANDING_WATER_THRESHOLD) {
            float waterToShow = water - STANDING_WATER_THRESHOLD;
            float waterAlphaRatio = waterToShow / (MAX_WATER - STANDING_WATER_THRESHOLD);
            waterAlpha = (MAX_WATER_OVERLAY_ALPHA - MIN_WATER_OVERLAY_ALPHA) * 
                waterAlphaRatio + MIN_WATER_OVERLAY_ALPHA;
        } else {
            // not enough water to show overlay, leave alpha at 0
        }
        waterColor.a = waterAlpha;
        waterSr.color = waterColor;
    }

    
    // MOCK METHODS
    void InitMockState() {
        elevation = Random.Range(MIN_ELEVATION, MAX_ELEVATION);
        heat = Random.Range(MIN_HEAT, MAX_HEAT);
        water = Random.Range(MIN_WATER, MAX_WATER);
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
