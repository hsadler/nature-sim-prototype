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


    // game object references
    public GameObject tileInfoText;
    private GameObject highlightFrame;


    // script references
    private EnvironmentSpriteList eSpriteList;
    private GameObjectRegistry goRegistry;

    
    // Start is called before the first frame update
    void Start() {

        // set game object references
        highlightFrame = GameObject.FindWithTag("HighlightFrame");

        // set script references
        eSpriteList = EnvironmentSpriteList.instance;
        goRegistry = GameObjectRegistry.instance;
        
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
        earthType = "DIRT";
        elevation = Random.Range(MIN_ELEVATION, 100);
        heat = Random.Range(MIN_HEAT, 100);
        water = Random.Range(MIN_WATER, 100);
    }
    
    void InitMockAppearance() {
        Sprite earthSprite = eSpriteList.GetEarthSpriteByName(earthType);
        GetComponent<SpriteRenderer>().sprite = earthSprite;
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
