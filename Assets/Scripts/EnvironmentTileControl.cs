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

    public Sprite Dirt;
    
    // Start is called before the first frame update
    void Start()
    {
        // TODO:
            // - set the neighbors

        // test: dynamically set sprite to Dirt
        GetComponent<SpriteRenderer>().sprite = Dirt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
