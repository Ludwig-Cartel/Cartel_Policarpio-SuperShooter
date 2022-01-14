using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Bullet bullet;
    Vector2 direction;

    public bool autoFire = false;
    public float fireInterval = 0.5f;
    public float fireDelaySeconds = 0.0f;
    public float fireTimer = 0f;
    public float delayFire = 0f;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!isActive) {
            return;
        }
        direction = (transform.localRotation * Vector2.right).normalized;
        if (autoFire) {
            if (delayFire >= fireDelaySeconds)
            {
                if (fireTimer >= fireInterval)
                {
                    fireBullet();
                    fireTimer = 0;
                }
                else {
                    fireTimer += Time.deltaTime;   
                }
            }
            else {
                delayFire += Time.deltaTime;
            }
            
        }
    }

    public void fireBullet() {
        GameObject fire = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Bullet fireBullet = fire.GetComponent<Bullet>();
        fireBullet.directionBullet = direction;  
    }
}
