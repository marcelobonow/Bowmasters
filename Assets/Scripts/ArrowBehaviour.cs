using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class ArrowBehaviour : MonoBehaviour {

    [SerializeField]
    private float angle;
    private Rigidbody2D rigidBody;
    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (GameManager.inAir)
        {
            if(rigidBody.velocity.y < 0.01 && rigidBody.velocity.y > -0.01)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else
            {
                Debug.Log("Velocity x: " + rigidBody.velocity.x);
                Debug.Log("Velocity y: " + rigidBody.velocity.y);
                angle = Mathf.Atan(rigidBody.velocity.y / rigidBody.velocity.x) * Mathf.Rad2Deg;
                Debug.Log("angle: " + angle);           
                gameObject.transform.eulerAngles = new Vector3(0, 0, angle-90);
            }
        }
	}
}
