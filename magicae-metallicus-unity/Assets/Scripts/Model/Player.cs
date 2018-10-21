using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SpriteRenderer renderer;

    public Sprite left;
    public Sprite right;
    public Sprite top;
    public Sprite bottom;

    public float speed;

    public int number = 0;

    private GamepadInput _input;
    private GamepadDevice gamepad;

    public GamepadInput input {
        get {
            if (!_input)
                _input = GetComponent<GamepadInput>();
            return _input;
        }
    }

    // Use this for initialization
    void Start () {
        this.renderer.sprite = right;
	}
	
	// Update is called once per frame
	void Update () {


        if (input.gamepads.Count == 0) {
            Debug.Log("No gamepad connected");
        }
        else {
            gamepad = input.gamepads[number];
            Debug.Log(input.gamepads.Count+" gamepads connected");
            this.transform.Translate(gamepad.GetAxis(GamepadAxis.LeftStickX) * speed * Time.deltaTime, gamepad.GetAxis(GamepadAxis.LeftStickY) * speed * Time.deltaTime, 0);
            //this.transform.Translate(Mathf.Abs(gamepad.GetAxis(GamepadAxis.LeftStickX)) * speed * Time.deltaTime, Mathf.Abs(gamepad.GetAxis(GamepadAxis.LeftStickY)) * speed * Time.deltaTime, 0);


            Debug.Log(Mathf.Cos(gamepad.GetAxis(GamepadAxis.RightStickX)) + " vs " + Mathf.Sin(gamepad.GetAxis(GamepadAxis.RightStickY)));


            float x = gamepad.GetAxis(GamepadAxis.LeftStickX);
            float y = gamepad.GetAxis(GamepadAxis.LeftStickY);

            float mulSin = 1;
            if(Mathf.Asin(y) < 0) {
                mulSin = -1;
            }
            float mulCos = Mathf.Asin(y);
            if (x < 0) {
                mulCos = Mathf.PI- Mathf.Asin(y);
            }

            float rad;
            float rotation = 0;
            if(Mathf.Abs(x) <= Mathf.Abs(y)){
                rad = mulSin * Mathf.Acos(x);
                Debug.Log("Y Win");
            }
            else {
                rad = mulCos;
                Debug.Log("X Win");
            }

            Debug.Log(rad / Mathf.PI);
            Debug.Log("ANGLE = " + (Mathf.Rad2Deg * rad));

            
            if (x+y != 0) {
                if (rad <= Mathf.PI / 4 && rad > Mathf.PI / -4) {
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
                    rotation = (Mathf.Rad2Deg * rad) -180;
                }

               // rotation = (Mathf.Rad2Deg * rad);
                this.transform.eulerAngles = new Vector3(0, 0, rotation);

            }
            else {
                
                Debug.Log("Joystick in the middle");
            }

            

            x = gamepad.GetAxis(GamepadAxis.LeftStickX);
            y = gamepad.GetAxis(GamepadAxis.LeftStickY);

            mulSin = 1;
            if (Mathf.Asin(y) < 0) {
                mulSin = -1;
            }
            mulCos = Mathf.Asin(y);
            if (x < 0) {
                mulCos = Mathf.PI - Mathf.Asin(y);
            }
            
            if (Mathf.Abs(x) <= Mathf.Abs(y)) {
                rad = mulSin * Mathf.Acos(x);
            }
            else {
                rad = mulCos;
            }



            if (x + y != 0) {
                if (rad <= Mathf.PI / 4 && rad > Mathf.PI / -4) {
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
                }

                // rotation = (Mathf.Rad2Deg * rad);
                this.transform.eulerAngles = new Vector3(0, 0, rotation);

            }
            else {

                Debug.Log("Joystick in the middle");
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






            //input.gamepads.

        }



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
