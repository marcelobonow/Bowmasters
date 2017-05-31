using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    [SerializeField]
    private GameManager gameManager;

    private void FixedUpdate()
    {
        if(GameManager.enemyCanShoot)
        {
            gameManager.SetStage(GameManager.Stage.Player);
        }
    }
}
