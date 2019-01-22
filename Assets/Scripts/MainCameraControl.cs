using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour
{
    
    public GameObject player;

    public float zoomSpeed;
    public int zoomMinDistance;
    public int zoomMaxDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: fix bugs in these calls
        CheckPosition();
        CheckZoom();
    }

    void CheckPosition() {
        transform.position += Vector3.left * player.transform.position.x; 
        transform.position += Vector3.up * player.transform.position.y;   
    }

    void CheckZoom() {
        // calculate the zoom
        float newZoomDiff = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.position += Vector3.forward * newZoomDiff;
        // apply min and max to zoom
        if(transform.position.z > zoomMinDistance) {
            transform.position += Vector3.forward * zoomMinDistance;
        }
        else if(transform.position.z < zoomMaxDistance) {
            transform.position += Vector3.forward * zoomMaxDistance;
        }
    }

}
