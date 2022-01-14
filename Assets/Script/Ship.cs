using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    Laser[] lasers;
    SpriteRenderer spriteRenderer;
    Vector2 InitialPosition;

    float movementSpeed = 3;
    bool up;
    bool down;
    bool left;
    bool right;
    bool upSpeed;

    bool invincible = false;
    int hits = 3;
    float invincibleTimer = 0;
    float invincibleTime = 2;

    bool fireBullet;
    // Start is called before the first frame update
    private void Awake()
    {
        InitialPosition = transform.position;
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        lasers = transform.GetComponentsInChildren<Laser>();

        foreach (Laser laser in lasers) {
            laser.isActive = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        upSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        fireBullet = Input.GetKeyDown(KeyCode.LeftControl);

        if (fireBullet) {
            fireBullet = false;

            foreach (Laser laser in lasers) {
                laser.fireBullet();
            }
        }

        if (invincible) {
            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invincibleTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    
    }

    private void ResetShip()
    {
        transform.position = InitialPosition;
        hits = 3;
        Level.instance.resetLevel();
    }
    void hit(GameObject gameObjectHit) {
        if (!invincible) {
            hits--;
            if (hits == 0)
            {
                ResetShip();
            }
            else {
                invincible = true;
            }
            Destroy(gameObjectHit);
        }
    }

    private void FixedUpdate()
    {
      

        Vector2 position = transform.position;

        float movementAmount = movementSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        if (upSpeed)
        {
            movementAmount *= 3;
        }

        if (up)
        {
            move.y += movementAmount;
        }
        if (down)
        {
            move.y -= movementAmount;
        }
        if (left) {
            move.x -= movementAmount;
        }
        if (right) {
            move.x += movementAmount;
        }

        float movementMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);

        if (movementMagnitude > movementAmount) {
            float ratio = movementAmount / movementMagnitude;
            move *= ratio; 
        }

        position += move;

        if (position.x <= 0.80f) {
            position.x = 0.80f;
        }

        if (position.x >= 16.5f)
        {
            position.x = 16.5f;
        }

        if (position.y <= 0.60f)
        {
            position.y = 0.60f;
        }

        if (position.y >= 8.2f)
        {
            position.y = 8.2f;
        }

        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null) {
            if (bullet.isEnemy)
            {
                //Destroy(gameObject);
                hit(bullet.gameObject);
            }

        }


        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null) {
            hit(destructable.gameObject);
        }
    }
}
