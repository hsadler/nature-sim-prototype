using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTileControl_3d : MonoBehaviour
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


    // game object references
    public GameObject tileInfoText;
    public GameObject earthPoly;
    public GameObject waterPoly;


    // script references
    private EnvironmentSpriteList_3d eSpriteList;
    private GameObjectRegistry_3d goRegistry;

    
    // Start is called before the first frame update
    void Start() {
        // set game object references
        earthPoly = transform.Find("EarthPoly").gameObject;
        waterPoly = transform.Find("WaterPoly").gameObject;
        // set script references
        goRegistry = GameObjectRegistry_3d.instance;
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
        SetEarthPolyAppearance();
        SetWaterPolyAppearance();
    }

    private void SetEarthPolyAppearance() {
        // set scale
        float newHeight = (earth / WorldSettings.MAX_EARTH) * WorldSettings.MAX_EARTH;
        Vector3 tempScale = earthPoly.transform.localScale;
        earthPoly.transform.localScale = new Vector3(
            tempScale[0],
            newHeight,
            tempScale[2]
        );
        // set position
        float newPositionY = newHeight / 2;
        Vector3 tempPosition = earthPoly.transform.position;
        earthPoly.transform.position = new Vector3(
            tempPosition[0],
            newPositionY,
            tempPosition[2]
        );
    }
    private void SetWaterPolyAppearance() {
        // set scale
        float newHeight = (water / WorldSettings.MAX_WATER) * WorldSettings.MAX_WATER;
        Vector3 tempScale = waterPoly.transform.localScale;
        waterPoly.transform.localScale = new Vector3(
            tempScale[0],
            newHeight,
            tempScale[2]
        );
        // set position
        float newPositionY = earthPoly.transform.position.y + (earthPoly.transform.localScale.y / 2) + newHeight / 2;
        Vector3 tempPosition = waterPoly.transform.position;
        waterPoly.transform.position = new Vector3(
            tempPosition[0],
            newPositionY,
            tempPosition[2]
        );
    }

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    void OnMouseEnter()
    {
        // TODO: uncomment when ready
        // highlightFrame.SetActive(true);
    }

    /// <summary>
    /// Called when the mouse is not any longer over the GUIElement or Collider.
    /// </summary>
    void OnMouseExit()
    {
        // TODO: uncomment when ready
        // highlightFrame.SetActive(false);   
    }

    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    void OnMouseOver()
    {
        goRegistry.tileInfoText.GetComponent<TileInfoTextControl_3d>().UpdateInfo(gameObject);
    }

    // flow coefficient getters
    public float GetEarthFlowCoefficient() {
        return WorldSettings.EARTH_FLOW_COEFFICIENT;
    }
    public float GetHeatFlowCoefficient() {
        return WorldSettings.HEAT_FLOW_COEFFICIENT;
    }
    public float GetWaterFlowCoefficient() {
        return WorldSettings.WATER_FLOW_COEFFICIENT;
    }

    // neighbors getters
    public List<EnvironmentTileControl_3d> GetTileNeighborsAsList() {
        List<EnvironmentTileControl_3d> neighbors = new List<EnvironmentTileControl_3d>();
        if(neighborUp != null) {
            neighbors.Add(neighborUp.GetComponent<EnvironmentTileControl_3d>());
        }
        if(neighborDown != null) {
            neighbors.Add(neighborDown.GetComponent<EnvironmentTileControl_3d>());
        }
        if(neighborLeft != null) {
            neighbors.Add(neighborLeft.GetComponent<EnvironmentTileControl_3d>());
        }
        if(neighborRight != null) {
            neighbors.Add(neighborRight.GetComponent<EnvironmentTileControl_3d>());
        }
        return neighbors;
    } 

}
