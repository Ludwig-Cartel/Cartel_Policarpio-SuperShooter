using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 directionBullet = new Vector2(1,0);
    public float speedBullet = 2;

    public bool isEnemy = false;
    public Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = directionBullet * speedBullet;   
    }

    public void FixedUpdate()
    {
        Vector2 positionBullet = transform.position;
        positionBullet += velocity * Time.fixedDeltaTime;
        transform.position = positionBullet;
    }
}
