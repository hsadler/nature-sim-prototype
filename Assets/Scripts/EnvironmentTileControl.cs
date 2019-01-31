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

    private const float MIN_SHADOW_OVERLAY_ALPHA = 0;
    private const float MAX_SHADOW_OVERLAY_ALPHA = 0.65f;    


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
        SetAppearanceFromState();    
    }

    void SetAppearanceFromState() {
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
        float alphaRatio = 1 - (elevation / MAX_ELEVATION);
        float alpha = (MAX_SHADOW_OVERLAY_ALPHA - MIN_SHADOW_OVERLAY_ALPHA) * alphaRatio + MIN_SHADOW_OVERLAY_ALPHA;
        SpriteRenderer sr = shadowOverlay.GetComponent<SpriteRenderer>();
        Color shadowColor = sr.color;
        shadowColor.a = alpha;
        sr.color = shadowColor;
        // TODO: set water overlay tranparency based on water content
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
