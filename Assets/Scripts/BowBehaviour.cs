﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowBehaviour : MonoBehaviour {

    [SerializeField]
    private Sprite[] bowPositions;
    [SerializeField]
    public float[] arrowPositions;
    [SerializeField]
    private float bowAngle;
    [SerializeField]
    private Sprite actualSprite;

    void Start () {
        actualSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
	}

    public void SetBow(int _position)
    {

        if (_position >= 0 && _position < bowPositions.Length)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bowPositions[_position];
            GameManager.arrow.transform.localPosition = new Vector2(arrowPositions[_position],0f);
        }
    }
    public void SetBowRotation(float _angle)
    {
        transform.eulerAngles = new Vector3(0, 0, _angle);
    }
}