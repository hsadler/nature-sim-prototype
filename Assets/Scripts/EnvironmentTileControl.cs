using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileControl : MonoBehaviour
{
    

    // HOLDS ENVIRONMENT TILE STATE AND SETS APPEARANCE

    
    // tile state properties
    public string earthType;
    public float earth;
    public float heat;
    public float water;

    
    // temp properties for keeping track of update values
    public float updateEarthAmount;
    public float updateHeatAmount;
    public float updateWaterAmount;

    
    // neighboring environment tiles
    public GameObject neighborUp;
    public GameObject neighborRight;
    public GameObject neighborDown;
    public GameObject neighborLeft;


    // TODO: put this in a global config
    // config

    // min and max for state properties
    private const float MIN_EARTH = 0;
    private const float MAX_EARTH = 200;
    private const float MIN_HEAT = 0;
    private const float MAX_HEAT = 200;
    private const float MIN_WATER = 0;
    private const float MAX_WATER = 200;

    // property flow coefficients
    private const float EARTH_FLOW_COEFFICIENT = 0.1f;
    private const float HEAT_FLOW_COEFFICIENT = 0.3f;
    private const float WATER_FLOW_COEFFICIENT = 0.02f;

    // water content thresholds for deriving earth type
    private const float SAND_WATER_THRESHOLD = 20;
    private const float DRY_DIRT_WATER_THRESHOLD = 40;
    private const float DIRT_WATER_THRESHOLD = 60;
    private const float STANDING_WATER_THRESHOLD = 80;
    
    // max and min transparencies for overlays
    private const float MIN_SHADOW_OVERLAY_ALPHA = 0;
    private const float MAX_SHADOW_OVERLAY_ALPHA = 0.65f;
    private const float MIN_WATER_OVERLAY_ALPHA = 0.1f;
    private const float MAX_WATER_OVERLAY_ALPHA = 0.9f;
    private const float MIN_HEAT_OVERLAY_ALPHA = 0;
    private const float MAX_HEAT_OVERLAY_ALPHA = 0.3f;


    // game object references
    public GameObject tileInfoText;
    private GameObject highlightFrame;
    private GameObject shadowOverlay;
    private GameObject waterOverlay;
    private GameObject heatOverlay;


    // script references
    private EnvironmentSpriteList eSpriteList;
    private GameObjectRegistry goRegistry;

    
    // Start is called before the first frame update
    void Start() {
        // set game object references
        highlightFrame = transform.Find("HighlightFrame").gameObject;
        shadowOverlay = transform.Find("ShadowOverlay").gameObject;
        waterOverlay = transform.Find("WaterOverlay").gameObject;
        heatOverlay = transform.Find("HeatOverlay").gameObject;
        // set script references
        eSpriteList = EnvironmentSpriteList.instance;
        goRegistry = GameObjectRegistry.instance;
    }

    public void InitState(float earth, float heat, float water) {
        this.earth = earth;
        this.heat = heat;
        this.water = water;
    }

    public void ApplyUpdateFromTempValues() {
        earth += updateEarthAmount;
        updateEarthAmount = 0;
        heat += updateHeatAmount;
        updateHeatAmount = 0;
        water += updateWaterAmount;
        updateWaterAmount = 0;
    }

    public void SetAppearanceFromState() {
        SetEarthAppearance();
        SetShadowAppearance();
        SetWaterAppearance();
        SetHeatAppearance();
    }

    private void SetEarthAppearance() {
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
    }

    private void SetShadowAppearance() {
        // set shadow overlay transparency based on elevation
        float shadowAlphaRatio = 1 - (earth / MAX_EARTH);
        float shadownAlpha = (MAX_SHADOW_OVERLAY_ALPHA - MIN_SHADOW_OVERLAY_ALPHA) * 
            shadowAlphaRatio + MIN_SHADOW_OVERLAY_ALPHA;
        SpriteRenderer shadowSr = shadowOverlay.GetComponent<SpriteRenderer>();
        Color shadowColor = shadowSr.color;
        shadowColor.a = shadownAlpha;
        shadowSr.color = shadowColor;
    }

    private void SetWaterAppearance() {
        // set water overlay transparency based on water content
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

    private void SetHeatAppearance() {

        // TODO: this is a naive heat appearance implementation
        // should appear cold as well as hot since cold is the absence of heat

        // set heat overlay transparency based on heat
        float heatAlphaRatio = heat / MAX_HEAT;
        float shadownAlpha = (MAX_HEAT_OVERLAY_ALPHA - MIN_HEAT_OVERLAY_ALPHA) * 
            heatAlphaRatio + MIN_HEAT_OVERLAY_ALPHA;
        SpriteRenderer heatSr = heatOverlay.GetComponent<SpriteRenderer>();
        Color heatColor = heatSr.color;
        heatColor.a = shadownAlpha;
        heatSr.color = heatColor;
    }

    
    // MOCK METHODS
    void InitMockState() {
        earth = Random.Range(MIN_EARTH, MAX_EARTH);
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

    // flow coefficient getters
    public float GetEarthFlowCoefficient() {
        return EARTH_FLOW_COEFFICIENT;
    }
    public float GetHeatFlowCoefficient() {
        return HEAT_FLOW_COEFFICIENT;
    }
    public float GetWaterFlowCoefficient() {
        return WATER_FLOW_COEFFICIENT;
    }

    // neighbors getters
    public List<EnvironmentTileControl> GetTileNeighborsAsList() {
        List<EnvironmentTileControl> neighbors = new List<EnvironmentTileControl>();
        if(neighborUp != null) {
            neighbors.Add(neighborUp.GetComponent<EnvironmentTileControl>());
        }
        if(neighborDown != null) {
            neighbors.Add(neighborDown.GetComponent<EnvironmentTileControl>());
        }
        if(neighborLeft != null) {
            neighbors.Add(neighborLeft.GetComponent<EnvironmentTileControl>());
        }
        if(neighborRight != null) {
            neighbors.Add(neighborRight.GetComponent<EnvironmentTileControl>());
        }
        return neighbors;
    } 

}
