using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool canBeDestroyed = false;
    public int scoreValue = 100;
    // Start is called before the first frame update
    void Start()
    {
        Level.instance.addEnemy();   
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -2)
        {
            Destroy(gameObject);
        }

        if (transform.position.x < 17.0f && !canBeDestroyed){
            canBeDestroyed = true;
            Laser[] lasers = transform.GetComponentsInChildren<Laser>();

            foreach (Laser laser in lasers) {
                laser.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                Level.instance.addScore(scoreValue);
                Level.instance.removeEnemy();
                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        
    }
}
