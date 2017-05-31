using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {

    private void Start()
    {
        Debug.Log("Started");
        Debug.Log(GameManager.actualStage);
        GameManager.actualStage = GameManager.Stages.Player;
    }
}
