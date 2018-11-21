using UnityEngine;
using UnityEngine.Networking;

public class PlayerTestNetwork : NetworkBehaviour {
    void Update() {
        if (!isLocalPlayer) {
            return;
        }
        var x = 0;
        var z = 0;

        if (Input.GetKey(KeyCode.Z)) {
            x += 1;
        }
        else if (Input.GetKey(KeyCode.Q)) {
            z -= 1;
        }
        else if (Input.GetKey(KeyCode.S)) {
            x -= 1;
        }
        else if (Input.GetKey(KeyCode.D)) {
            z += 1;
        }

        transform.Translate(0, x/10, 0);
        transform.Rotate(0, 0, z*2);
    }
}