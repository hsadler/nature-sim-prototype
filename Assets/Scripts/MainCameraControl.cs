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
        CheckPosition();
        CheckZoom();
    }

    void CheckPosition() {
        
    }

    void CheckZoom() {
        // calculate the zoom
        float newZoomDiff = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.localPosition += Vector3.forward * newZoomDiff;
        // apply min and max to zoom
        if(transform.localPosition.z > zoomMinDistance) {
            transform.localPosition = Vector3.forward * zoomMinDistance;
        }
        else if(transform.localPosition.z < zoomMaxDistance) {
            transform.localPosition = Vector3.forward * zoomMaxDistance;
        }
    }

}
