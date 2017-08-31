using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour {

    public float moveSpeed = 10f;

    void Update() {

        Vector3 position = GetBaseInput();
        position = position * moveSpeed * Time.deltaTime;
        transform.Translate(position);
    }

    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S)) {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A)) {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
