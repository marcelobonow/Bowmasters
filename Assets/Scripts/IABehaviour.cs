using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    private void FixedUpdate()
    {
        if(GameManager.enemyCanShoot)
        {
            GameManager.actualStage = GameManager.Stages.Player;
        }
    }
}
