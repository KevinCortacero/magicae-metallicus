using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaceCollider : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Pickace");
        if (col.gameObject.tag == "Rock")
        {
            RockScript rock = col.gameObject.GetComponent<RockScript>();
            rock.pv--;
        }
    }
}
