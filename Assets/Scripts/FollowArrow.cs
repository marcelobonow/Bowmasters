using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArrow : MonoBehaviour {

    private const float ZCAMERA = -10;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Transform startPoint;

    private void Start()
    {
        arrow = GameManager.arrow;
        gameObject.transform.position = startPoint.position;
    }
    void Update ()
    { 
        if (GameManager.inAir)
        {
            transform.position = new Vector3(arrow.transform.position.x,arrow.transform.position.y,ZCAMERA);
        }
        //else
            //transform.position = new Vector3(0, 0, 0); //mudar para um start point talvez
	}
}
