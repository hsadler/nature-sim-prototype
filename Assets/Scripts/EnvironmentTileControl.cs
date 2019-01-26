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
    
    // Start is called before the first frame update
    void Start()
    {
        // test: dynamically set sprite to random earth sprite
        // List<Sprite> earthSpriteList = EnvironmentSpriteList.instance.earthSpriteList;
        // Sprite randEarth = earthSpriteList[
        //     Random.Range(0, earthSpriteList.Count)
        // ];
        // GetComponent<SpriteRenderer>().sprite = randEarth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
