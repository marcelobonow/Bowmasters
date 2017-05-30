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
        angle = Mathf.Atan2(rigidBody.velocity.y, gameObject.GetComponent<Rigidbody2D>().velocity.x);
        gameObject.transform.Rotate(0, 0, angle);
	}
}
