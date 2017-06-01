using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{

    private const float ZCAMERA = -10;

    private bool inPosition;

    [SerializeField]
    private GameObject arrow; //referencia a arrow que o GameManager possui
    [SerializeField]
    private Transform startPointPlayer;
    [SerializeField]
    private Transform startPointEnemy;
    [SerializeField]
    private float moveTimer;

    private void Start()
    {
        gameObject.transform.position = new Vector3(startPointPlayer.position.x, startPointPlayer.position.y, ZCAMERA);
    }
    void FixedUpdate()
    {
        arrow = GameManager.arrow;
        if (GameManager.staticStage == GameManager.Stage.playershot || GameManager.staticStage == GameManager.Stage.EnemyShot)
        {
            //Se estiver em um dos turnos de tiro, a camera seguirá a flecha disparada
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
            inPosition = false;
        }
        if (inPosition == true && Camera.main.orthographicSize >= 3 && GameManager.staticStage == GameManager.Stage.Enemy)
        {
            GameManager.enemyCanShot = true;
        }
        //se esta em um dos turnos de mirar do player ou do inimigo a camera vai para as posições pre-definidas
        if (GameManager.staticStage == GameManager.Stage.Player && !inPosition)
        {
            SmoothFollow(startPointPlayer.transform.position);
        }
        if (GameManager.staticStage == GameManager.Stage.Enemy && !inPosition)
        {
            SmoothFollow(startPointEnemy.transform.position);
        }
    }
    void SmoothFollow(Vector2 position)
    {
        moveTimer += Time.fixedDeltaTime;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, moveTimer / 50),
                Mathf.Lerp(transform.position.y, position.y + 1, moveTimer / 50), ZCAMERA);//offset nara camera, para não mostrar tanto chão
        if (moveTimer > 5)
        {
            moveTimer = 0;
            GameManager.cameraInPosition = true;
            inPosition = true;
        }
    }
}
