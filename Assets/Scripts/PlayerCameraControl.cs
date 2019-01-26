using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    
    public GameObject player;

    public float zoomSpeed;
    public int zoomMinDistance;
    public int zoomMaxDistance;

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
        CheckZoom();
    }

    void CheckPosition() {
        // always keep camera centered on player
        Vector3 currPos = transform.position;
        float currPosZ = currPos[2];
        transform.position = new Vector3(
            player.transform.position.x, 
            player.transform.position.y, 
            currPosZ
        );
    }

    void CheckZoom() {
        // calculate the zoom
        float newZoomDiff = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        transform.position += Vector3.forward * newZoomDiff;
        // apply min and max to zoom
        if(transform.position.z > zoomMinDistance) {
            transform.position = new Vector3(
                transform.position[0],
                transform.position[1],
                zoomMinDistance
            );
        }
        else if(transform.position.z < zoomMaxDistance) {
            transform.position = new Vector3(
                transform.position[0],
                transform.position[1],
                zoomMaxDistance
            );
        }
    }

}
