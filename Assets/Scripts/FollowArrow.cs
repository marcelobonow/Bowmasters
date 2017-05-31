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
        gameObject.transform.position = startPointPlayer.position;
    }
    void FixedUpdate ()
    {
        if (GameManager.staticStage == GameManager.Stage.playershot || GameManager.staticStage == GameManager.Stage.EnemyShot)
        {
            arrow = GameManager.arrow;
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
        }
        else if(GameManager.staticStage == GameManager.Stage.Player)
        {
            arrow = GameManager.arrow;
            transform.position = startPointPlayer.position;
        }
        else if(GameManager.staticStage == GameManager.Stage.Enemy)
        {
            transform.position = startPointEnemy.position;
            arrow = GameManager.arrow;
        }
	}
}
