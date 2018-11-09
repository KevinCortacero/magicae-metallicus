using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIAScript : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

   
    public float speed;
    public GameObject slash;


    
    public GameObject projectile;



   
    private bool focusing = false;
    private bool mining = false;
    private Projectile bullet;
    private bool canMove;
    private ControllerType type;
    private int number;
    private float delay;
    private float delayMovement;
    private RockScript rockMined;
    private List<ProjectileHolder> projectiles;
    private int projectilesIndex = 0;

    private float pv;
    public float PV
    {
        get
        {
            return pv;
        }
    }


   

    // Use this for initialization
    void Start()
    {
        this.delay = Time.time;
        this.delayMovement = Time.time;
        this.canMove = true;
        //this.spriteRenderer.sprite = right;
        this.number = Int32.Parse(gameObject.name.Split(null)[1]) - 1;
        this.pv = 10;
        this.projectiles = new List<ProjectileHolder>();
        this.projectiles.Add(new ProjectileHolder(this.projectile, Mathf.Infinity));
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - this.delay > 0.5)
        {
            Shoot();
            this.delay = Time.time;
        }
        HandleMovement();
    }

    private void ItemRight()
    {
        this.projectilesIndex += 1;
        this.projectilesIndex = GetSafeValueForItems(projectilesIndex);
    }

    private void ItemLeft()
    {
        this.projectilesIndex -= 1;
        this.projectilesIndex = GetSafeValueForItems(projectilesIndex);
    }

    private int GetSafeValueForItems(int value)
    {

        if (value >= this.projectiles.Count)
        {
            return 0;
        }
        else if (value < 0)
        {
            return this.projectiles.Count - 1;
        }
        else
        {
            return value;
        }
    }

    public void Damage(float value)
    {
        this.pv -= value;
    }

   
    private void Focus()
    {
        if (!focusing)
        {
            focusing = true;

            GameObject go = Instantiate(projectiles[this.projectilesIndex].projectile, transform.Find("BulletSpawn").position, spriteRenderer.gameObject.transform.rotation) as GameObject;


            go.SetActive(false);
            this.bullet = go.GetComponent<Projectile>();


        }
        else
        {
            //Debug.Log(bullet);
            this.bullet.Focus(Time.deltaTime);
        }
    }

    private void Shoot()
    {

        

        float x = 1;
        float y = 0;

        if (x + y == 0)
        {   
            x = transform.up.x;
            y = transform.up.y;
        }

        this.bullet.gameObject.transform.position = transform.Find("BulletSpawn").position;
        this.bullet.gameObject.transform.rotation = spriteRenderer.gameObject.transform.rotation;
        this.bullet.gameObject.SetActive(true);
        Debug.Log(this.bullet.gameObject.GetComponent<Collider2D>() + " ignore " + GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(this.bullet.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        this.bullet.Shoot(x, y);

        this.projectiles[this.projectilesIndex].remaining = this.projectiles[this.projectilesIndex].remaining - 1f;


        Debug.Log("Remaining " + this.projectiles[this.projectilesIndex].remaining);

        if (this.projectiles[this.projectilesIndex].remaining == 0f)
        {
            this.projectiles.RemoveAt(this.projectilesIndex);
        }
    }


    private float GetRadFromXY(float x, float y)
    {
        float mulSin = 1;
        if (Mathf.Asin(y) < 0)
        {
            mulSin = -1;
        }
        float mulCos = Mathf.Asin(y);
        if (x < 0)
        {
            mulCos = Mathf.PI - Mathf.Asin(y);
        }

        float rad;

        if (Mathf.Abs(x) <= Mathf.Abs(y))
        {
            rad = mulSin * Mathf.Acos(x);
            //Debug.Log("Y Win");
        }
        else
        {
            rad = mulCos;
            //Debug.Log("X Win");
        }


        return rad;
    }
    

    private void HandleMovement()
    {
       if(Time.time - this.delayMovement > 0.2)
        {
            float x = 0;
            float y = 0;

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                y = 1;
            }
            else
            {
                y = -1;
            }

            GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(x, y) * speed;
        }
            
 
    }

}
