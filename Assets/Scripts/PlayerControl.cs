using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    public float playerSpeed;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        CheckMovePlayer();
    }

    void CheckMovePlayer() {
        // check key ups first
        // up check
        if(Input.GetKeyUp(KeyCode.W)) {
            rb.velocity -= Vector2.up * 0;
        }
        // down check
        if(Input.GetKeyUp(KeyCode.S)) {
            rb.velocity -= Vector2.down * 0;
        }
        // right check
        if(Input.GetKeyUp(KeyCode.D)) {
            rb.velocity -= Vector2.right * 0;
        }
        // left check
        if(Input.GetKeyUp(KeyCode.A)) {
            rb.velocity -= Vector2.left * 0;
        }
        // check key downs last to override key ups
        // up check
        if(Input.GetKeyDown(KeyCode.W)) {
            rb.velocity += Vector2.up * playerSpeed;
        }
        // down check
        if(Input.GetKeyDown(KeyCode.S)) {
            rb.velocity += Vector2.down * playerSpeed;
        }
        // right check
        if(Input.GetKeyDown(KeyCode.D)) {
            rb.velocity += Vector2.right * playerSpeed;
        }
        // left check
        if(Input.GetKeyDown(KeyCode.A)) {
            rb.velocity += Vector2.left * playerSpeed;
        }
        // check stop
        if(Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(0, 0) * 0;
        }
    }

}
