using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    /*public Sprite left;
    public Sprite right;
    public Sprite top;
    public Sprite bottom;*/
    //public PickaceCollider pickace;
    public float speed;
    public GameObject slash;


    //public int number = 0;
    public GameObject projectile;



    private GamepadInput _input;
    private GamepadDevice gamepad;
    private bool focusing = false;
    private bool mining = false;
    private Projectile bullet;
    private bool canMove;
    private ControllerType type;
    private int number;
    private float tempsMine;
    private RockScript rockMined;
    [SerializeField]
    private List<ProjectileHolder> projectiles;
    private int projectilesIndex = 0;

    private float pv;

    public float PV {
        get {
            return pv;
        }
    }

    public int CURSOR
    {
        get
        {
            return this.projectilesIndex;
        }
    }


    public GamepadInput input {
        get {
            if (!_input)
                _input = GetComponent<GamepadInput>();
            return _input;
        }
    }

    // Use this for initialization
    void Start() {
        this.canMove = true;
        //this.spriteRenderer.sprite = right;
        this.number = Int32.Parse(gameObject.name.Split(null)[1]) - 1;
        this.pv = 10;
        //this.projectiles = new List<ProjectileHolder>();
        //this.projectiles.Add(new ProjectileHolder(this.projectile, Mathf.Infinity));
        //this.projectiles.Add(new ProjectileHolder(item.projectile, item.utilization));
    }

    // Update is called once per frame
    void Update() {
        if (this.pv <= 0) {
            Destroy(gameObject);
        }

        if (mining) {
            if (Time.time - tempsMine > 0.5) {
                StopMine();
            }
        }
        if (input.gamepads.Count <= number) {
            //Debug.Log("No gamepad connected");
            this.type = ControllerType.KEYBORD;
        }
        else {

            this.type = ControllerType.CONTROLLER;
            gamepad = input.gamepads[number];
        }

        //Debug.Log(input.gamepads.Count + " gamepads connected");

        HandleMovement();

        switch (this.type) {
            case ControllerType.CONTROLLER:
                if (gamepad.GetButton(GamepadButton.Action3)) {
                    Mine();
                }
                else if (gamepad.GetTrigger(GamepadTrigger.Right) > 0.8) {

                    Focus();
                }
                else if ((gamepad.GetTrigger(GamepadTrigger.Right) <= 0.8) && focusing) {
                    focusing = false;
                    Shoot();
                }
                if (gamepad.GetButton(GamepadButton.DpadLeft)) {
                    this.ItemLeft();
                }
                else if (gamepad.GetButton(GamepadButton.DpadRight)) {

                    this.ItemRight();
                }


                break;
            case ControllerType.KEYBORD:
                if (Input.GetMouseButton(1)) {
                    Mine();

                }
                else if (Input.GetMouseButton(0)) {

                    Focus();
                }
                else if (Input.GetMouseButtonUp(0) && focusing) {
                    focusing = false;
                    Shoot();
                }



                if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
                    this.ItemRight();
                }
                else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
                    this.ItemLeft();
                }

                break;
        }
    }

    public int GetSpellRemaining(int spellID)
    {
        return (int) this.projectiles[spellID].remaining;
    }

    private void ItemRight() {
        this.projectilesIndex += 1;
        this.projectilesIndex = GetSafeValueForItems(projectilesIndex);
        if (this.projectiles[this.projectilesIndex].remaining == 0) {
            this.ItemRight();
        }
    }

    private void ItemLeft() {
        this.projectilesIndex -= 1;
        this.projectilesIndex = GetSafeValueForItems(projectilesIndex);
        if(this.projectiles[this.projectilesIndex].remaining == 0) {
            this.ItemLeft();
        }
    }

    private int GetSafeValueForItems(int value) {

        if (value >= this.projectiles.Count) {
            return 0;
        }
        else if (value < 0) {
            return this.projectiles.Count - 1;
        }
        else {
            return value;
        }
    }

    public void Damage(float value) {
        this.pv -= value;
    }

    private void Mine() {
        if (!mining) {
            

            RaycastHit2D hit = Physics2D.Raycast(transform.Find("Pickace").position, transform.Find("Pickace").up);
            Debug.DrawRay(transform.Find("Pickace").position, transform.Find("Pickace").up, Color.green);

            // If it hits something...
            if (hit.collider != null) {

                


                float distance = Vector3.Distance(hit.point, transform.position);

                //Debug.Log("Got " + distance);

                this.rockMined = hit.collider.gameObject.GetComponent<RockScript>();

                if (distance > 0.2f) {
                    this.rockMined = null;
                }

                if (rockMined != null) {

                    mining = true;

                    canMove = false;
                    tempsMine = Time.time;

                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;


                    GameObject go = Instantiate(this.slash, transform.Find("Pickace").position, transform.Find("Pickace").rotation) as GameObject;
                    Destroy(go, 0.4f);
                }

                //Debug.Log(this.rockMined);

                // Calculate the distance from the surface and the "error" relative
                // to the floating height.
                //
                //float heightError = floatHeight - distance;

                // The force is proportional to the height error, but we remove a part of it
                // according to the object's speed.
                //float force = liftForce * heightError - rb2D.velocity.y * damping;

                // Apply the force to the rigidbody.
                //rb2D.AddForce(Vector3.up * force);
            }


        }
        else {
            //Debug.Log(bullet);
            //this.bullet.Focus(Time.deltaTime);
        }


        //this.pickace.GetComponent<Collider2D>().enabled = true;
        //GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero;
        //GameObject go = Instantiate(this.slash, this.gameObject.transform.position, renderer.gameObject.transform.rotation) as GameObject;





    }

    private void StopMine() {
        //this.pickace.GetComponent<Collider2D>().enabled = false;
        canMove = true;
        mining = false;
        if (this.rockMined != null) {
            this.rockMined.pv -= 1;
        }
    }

    private void Focus() {

        if (this.projectiles[this.projectilesIndex].remaining == 0) {
            Debug.Log("No more projectile !");
            return;
        }

        if (!focusing) {
            focusing = true;

            GameObject go = Instantiate(projectiles[this.projectilesIndex].projectile, transform.Find("BulletSpawn").position, spriteRenderer.gameObject.transform.rotation) as GameObject;
            
            go.SetActive(false);
            this.bullet = go.GetComponent<Projectile>();
            
        }
        else {
            //Debug.Log(bullet);
            this.bullet.Focus(Time.deltaTime);
        }
    }

    private void Shoot() {

        /*Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        Debug.Log("DIR = " + dir);*/


        Vector2 XY = GetXYFromAnyController();

        float x = XY.x;
        float y = XY.y;

        if (x + y == 0) {
            //Debug.Log("We are here with " + x + "," + y);
            //x = 0;
            //y = 1;
            x = transform.up.x;
            y = transform.up.y;
        }

        //rigidbody2D.AddForce(dir * amount);

        /*

        float x = gamepad.GetAxis(GamepadAxis.RightStickX);
        float y = gamepad.GetAxis(GamepadAxis.RightStickY);

        if(x+y == 0) {
            x = gamepad.GetAxis(GamepadAxis.LeftStickX);
            y = gamepad.GetAxis(GamepadAxis.LeftStickY);

        }

        float angle = GetRadFromXY(x, y);

        Debug.Log("ANGLE = " + angle / Mathf.PI);


        x = Mathf.Cos(angle);
        y = Mathf.Sin(angle);*/


        this.bullet.gameObject.transform.position = transform.Find("BulletSpawn").position;
        this.bullet.gameObject.transform.rotation = spriteRenderer.gameObject.transform.rotation;
        this.bullet.gameObject.SetActive(true);
        Debug.Log(this.bullet.gameObject.GetComponent<Collider2D>() + " ignore " + GetComponent<Collider2D>());

        Physics2D.IgnoreCollision(this.bullet.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        this.bullet.Shoot(x, y);

        this.projectiles[this.projectilesIndex].remaining = this.projectiles[this.projectilesIndex].remaining - 1f;


        Debug.Log("Remaining " + this.projectiles[this.projectilesIndex].remaining);

        /*if (this.projectiles[this.projectilesIndex].remaining == 0f) {
            this.projectiles.RemoveAt(this.projectilesIndex);
        }*/





        //float angle = Mathf.Atan2(x, y);

    }


    private float GetRadFromXY(float x, float y) {
        float mulSin = 1;
        if (Mathf.Asin(y) < 0) {
            mulSin = -1;
        }
        float mulCos = Mathf.Asin(y);
        if (x < 0) {
            mulCos = Mathf.PI - Mathf.Asin(y);
        }

        float rad;

        if (Mathf.Abs(x) <= Mathf.Abs(y)) {
            rad = mulSin * Mathf.Acos(x);
            //Debug.Log("Y Win");
        }
        else {
            rad = mulCos;
            //Debug.Log("X Win");
        }


        return rad;
    }

    private Vector2 GetXYFromAnyController() {

        float x = 0;
        float y = 0;

        switch (this.type) {
            case ControllerType.CONTROLLER:

                x = gamepad.GetAxis(GamepadAxis.RightStickX);
                y = gamepad.GetAxis(GamepadAxis.RightStickY);

                if (x + y == 0) {
                    x = gamepad.GetAxis(GamepadAxis.LeftStickX);
                    y = gamepad.GetAxis(GamepadAxis.LeftStickY);
                }

                break;
            case ControllerType.KEYBORD:
                Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
                Vector3 dir = (Input.mousePosition - sp).normalized;
                x = dir.x;
                y = dir.y;
                break;
        }

        return new Vector2(x, y);
    }

    private void HandleMovement() {
        if (canMove) {
            float x = 0;
            float y = 0;

            switch (this.type) {
                case ControllerType.CONTROLLER:
                    x = gamepad.GetAxis(GamepadAxis.LeftStickX);
                    y = gamepad.GetAxis(GamepadAxis.LeftStickY);
                    break;
                case ControllerType.KEYBORD:
                    if (Input.GetKey(KeyCode.Z)) {
                        y += 1;
                    }
                    else if (Input.GetKey(KeyCode.Q)) {
                        x -= 1;
                    }
                    else if (Input.GetKey(KeyCode.S)) {
                        y -= 1;
                    }
                    else if (Input.GetKey(KeyCode.D)) {
                        x += 1;
                    }
                    break;
            }


            GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(x, y) * speed;


            //this.transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
            //this.transform.Translate(gamepad.GetAxis(GamepadAxis.LeftStickX) * speed * Time.deltaTime, gamepad.GetAxis(GamepadAxis.LeftStickY) * speed * Time.deltaTime, 0);
            //this.transform.Translate(Mathf.Abs(gamepad.GetAxis(GamepadAxis.LeftStickX)) * speed * Time.deltaTime, Mathf.Abs(gamepad.GetAxis(GamepadAxis.LeftStickY)) * speed * Time.deltaTime, 0);


            //Debug.Log(Mathf.Cos(gamepad.GetAxis(GamepadAxis.RightStickX)) + " vs " + Mathf.Sin(gamepad.GetAxis(GamepadAxis.RightStickY)));




            float rad = GetRadFromXY(x, y);
            float rotation = 0;


            //Debug.Log(rad / Mathf.PI);
            // Debug.Log("ANGLE = " + (Mathf.Rad2Deg * rad));


            if (x + y != 0) {
                /*if (rad <= Mathf.PI / 4 && rad > Mathf.PI / -4) {
                    this.renderer.sprite = right;
                    rotation = (Mathf.Rad2Deg * rad);
                }
                else if (rad <= Mathf.PI * 3 / 4 && rad > Mathf.PI / -4) {
                    this.renderer.sprite = top;

                    rotation = (Mathf.Rad2Deg * rad) - 90;
                }
                else if (rad <= Mathf.PI / -4 && rad > Mathf.PI * -3 / 4) {
                    this.renderer.sprite = bottom;
                    rotation = (Mathf.Rad2Deg * rad) + 90;
                }
                else {
                    this.renderer.sprite = left;
                    rotation = (Mathf.Rad2Deg * rad) - 180;
                }*/

                rotation = (Mathf.Rad2Deg * rad) - 90;

                // rotation = (Mathf.Rad2Deg * rad);
                this./*renderer.*/gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);

            }
            else {

                //Debug.Log("Joystick in the middle");
            }

            Vector2 XY = GetXYFromAnyController();

            x = XY.x;
            y = XY.y;

            /*switch (this.type) {
                case ControllerType.CONTROLLER:
                    x = gamepad.GetAxis(GamepadAxis.RightStickX);
                    y = gamepad.GetAxis(GamepadAxis.RightStickY);
                    break;
                case ControllerType.KEYBORD:
                    Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
                    Vector3 dir = (Input.mousePosition - sp).normalized;
                    x = dir.x;
                    y = dir.y;
                    break;
            }*/

            rad = GetRadFromXY(x, y);
            rotation = 0;


            if (x + y != 0) {
                /* if (rad <= Mathf.PI / 4 && rad > Mathf.PI / -4) {
                     this.renderer.sprite = right;
                     rotation = (Mathf.Rad2Deg * rad);
                 }
                 else if (rad <= Mathf.PI * 3 / 4 && rad > Mathf.PI / -4) {
                     this.renderer.sprite = top;

                     rotation = (Mathf.Rad2Deg * rad) - 90;
                 }
                 else if (rad <= Mathf.PI / -4 && rad > Mathf.PI * -3 / 4) {
                     this.renderer.sprite = bottom;
                     rotation = (Mathf.Rad2Deg * rad) + 90;
                 }
                 else {
                     this.renderer.sprite = left;
                     rotation = (Mathf.Rad2Deg * rad) - 180;
                 }*/

                rotation = (Mathf.Rad2Deg * rad) - 90;

                // rotation = (Mathf.Rad2Deg * rad);
                this./*renderer.*/gameObject.transform.eulerAngles = new Vector3(0, 0, rotation);

            }
            else {

                //Debug.Log("Joystick in the middle");
            }


            /*mul = 1;
            if (Mathf.Asin(gamepad.GetAxis(GamepadAxis.RightStickY)) < 0) {
                mul = -1;
            }

            rad = mul * Mathf.Acos(gamepad.GetAxis(GamepadAxis.RightStickX));

            if(rad != Mathf.PI/2) {
                if (rad <= Mathf.PI / 4 && rad > Mathf.PI / -4) {
                    this.renderer.sprite = right;
                }
                else if (rad <= Mathf.PI * 3 / 4 && rad > Mathf.PI / -4) {
                    this.renderer.sprite = top;
                }
                else if (rad <= Mathf.PI / -4 && rad > Mathf.PI * -3 / 4) {
                    this.renderer.sprite = bottom;
                }
                else {
                    this.renderer.sprite = left;
                }
            }*/



            /*if (Input.GetKey(KeyCode.Z)) {
                this.renderer.sprite = top;
                this.transform.Translate(0, speed*Time.deltaTime, 0);
                Debug.Log(speed * Time.deltaTime);
            }else if (Input.GetKey(KeyCode.Q)) {
                this.renderer.sprite = left;
                this.transform.Translate(-speed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.S)) {
                this.renderer.sprite = bottom;
                this.transform.Translate(0, -speed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.D)) {
                this.renderer.sprite = right;
                this.transform.Translate(speed * Time.deltaTime, 0, 0);
            }*/
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("entered with " + GetComponent<BoxCollider2D>().GetComponent<Rigidbody2D>());

        if (other.gameObject.tag == "Rock") {
            RockScript rock = other.gameObject.GetComponent<RockScript>();
            rock.pv--;
        }

    }

    private void OnTriggerStay() { }

    private void OnTriggerExit() { }*/

    public void PickUpItem(Item item) {
        foreach (ProjectileHolder holder in this.projectiles) {
            if (holder.projectile.GetComponent<Projectile>().GetType().Equals(item.projectile.GetComponent<Projectile>().GetType())) {
                holder.remaining += item.utilization;
            }
        }
        //this.projectiles.Add(new ProjectileHolder(item.projectile, item.utilization));
    }



}
