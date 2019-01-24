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
        // TODO:
            // - set the neighbors

        // test: dynamically set sprite to dirt or dryDirt
        if(Random.value > 0.5) {
            GetComponent<SpriteRenderer>().sprite = EnvironmentSpriteList.instance.dirt;
        } else {
            GetComponent<SpriteRenderer>().sprite = EnvironmentSpriteList.instance.dryDirt;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
