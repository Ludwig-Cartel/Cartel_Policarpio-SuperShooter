using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRighLeft : MonoBehaviour

    
{
    public float movementSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x -= movementSpeed * Time.fixedDeltaTime;
        //if (position.x <-2) {
           // Destroy(gameObject);
       // }
        transform.position = position;
    }
}
