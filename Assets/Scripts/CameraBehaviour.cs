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
        if (GameManager.GetStage() == GameManager.Stage.playershot || GameManager.GetStage() == GameManager.Stage.EnemyShot)
        {
            //Se estiver em um dos turnos de tiro, a camera seguirá a flecha disparada
            transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, ZCAMERA);
            inPosition = false;
        }
        
        //se esta em um dos turnos de mirar do player ou do inimigo a camera vai para as posições pre-definidas
        if (GameManager.GetStage() == GameManager.Stage.PlayerAim && !inPosition)
        {
            SmoothFollow(startPointPlayer.transform.position);
        }
        if (GameManager.GetStage() == GameManager.Stage.EnemyAim && !inPosition)
        {
            SmoothFollow(startPointEnemy.transform.position);
        }
    }
    void SmoothFollow(Vector2 position)
    {
        moveTimer += Time.fixedDeltaTime;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, moveTimer / 20);
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, position.x, moveTimer / 50),
                Mathf.Lerp(transform.position.y, position.y + 1, moveTimer / 50), ZCAMERA);//offset nara camera, para não mostrar tanto chão
        if (moveTimer > 4)
        {
            moveTimer = 0;
            GameManager.cameraInPosition = true;
            inPosition = true;
        }
    }
}
