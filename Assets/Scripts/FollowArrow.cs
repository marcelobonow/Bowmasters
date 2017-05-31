using UnityEngine;

public class FollowArrow : MonoBehaviour {

    private const float ZCAMERA = -10;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private Transform startPointPlayer;
    [SerializeField]
    private Transform startPointEnemy;

    private void Start()
    {
        arrow = GameManager.arrow;
        gameObject.transform.position = startPointPlayer.position;
    }
    void FixedUpdate ()
    {
        if (GameManager.actualStage == GameManager.Stages.playershot || GameManager.actualStage == GameManager.Stages.enemyshot)
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
        else if(GameManager.actualStage == GameManager.Stages.Player)
            transform.position = startPointPlayer.position;
        else if(GameManager.actualStage == GameManager.Stages.enemy)
            transform.position = startPointEnemy.position;
        else
            Debug.Log("Erro, Actual stage: " + GameManager.actualStage);
	}
}
