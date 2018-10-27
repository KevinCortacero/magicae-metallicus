using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SpriteRenderer renderer;

    public Sprite left;
    public Sprite right;
    public Sprite top;
    public Sprite bottom;
    public PickaceCollider pickace;
    public float speed;
    public GameObject slash;

    //public int number = 0;
    public GameObject projectile;

    private GamepadInput _input;
    private GamepadDevice gamepad;
    private bool focusing = false;
    private Projectile bullet;
    private bool canMove;
    private ControllerType type;
    private int number;
    private float tempsMine;

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
        this.renderer.sprite = right;
        this.number = Int32.Parse(gameObject.name.Split(null)[1]) - 1;
    }

    // Update is called once per frame
    void Update() {

        if (!canMove)
        {
            if(Time.time - tempsMine > 0.2)
            {
                stopMine();
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
                break;
        }

    }

    private void Mine() {
        this.pickace.GetComponent<Collider2D>().enabled = true;
        GetComponentInChildren<Rigidbody2D>().velocity = Vector2.zero;
        GameObject go = Instantiate(this.slash, this.gameObject.transform.position, renderer.gameObject.transform.rotation) as GameObject;
        canMove = false;
        tempsMine = Time.time;
    }

    private void stopMine()
    {
        this.pickace.GetComponent<Collider2D>().enabled = false;
        canMove = true;

    }

    private void Focus() {
        if (!focusing) {
            focusing = true;

            GameObject go = Instantiate(projectile, transform.Find("BulletSpawn").position, renderer.gameObject.transform.rotation) as GameObject;
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
        this.bullet.gameObject.transform.rotation = renderer.gameObject.transform.rotation;
        this.bullet.gameObject.SetActive(true);
        this.bullet.Shoot(x, y);


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
        if (canMove)
        {
            float x = 0;
            float y = 0;

            switch (this.type)
            {
                case ControllerType.CONTROLLER:
                    x = gamepad.GetAxis(GamepadAxis.LeftStickX);
                    y = gamepad.GetAxis(GamepadAxis.LeftStickY);
                    break;
                case ControllerType.KEYBORD:
                    if (Input.GetKey(KeyCode.Z))
                    {
                        y += 1;
                    }
                    else if (Input.GetKey(KeyCode.Q))
                    {
                        x -= 1;
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        y -= 1;
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
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


            if (x + y != 0)
            {
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
            else
            {

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


            if (x + y != 0)
            {
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
            else
            {

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




}
