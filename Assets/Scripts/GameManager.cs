using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Stage
    {
        Player, playershot, Enemy, EnemyShot
    }
    public static GameObject arrow;
    public static float angle;
    public static float shotPower;
    public static bool cameraInPosition; //É colocada como verdadeira quando a camera esta em posição e quando o player estiver setado
    public static bool enemyCanShot;
    [SerializeField]
    private GameObject playerArrow;   //Pode ser um vetor de flechas diferentes, com danos diferentes, por exemplo
    [SerializeField]
    private GameObject enemyArrow;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private float timer;
    [SerializeField]
    private BowBehaviour bowBehaviour; //Muda entre o bow do player e do inimigo
    [SerializeField]
    private IABehaviour iaBehaviour;
    public static Stage staticStage; //Static é apenas para leitura
    private Stage stage; //enquanto este é para a escrita


    private void Awake()
    {
        stage = Stage.Player;
        SetPlayer();
    }
    public void SetPlayer()
    {
        arrow = playerArrow;
        cameraInPosition = false;
        arrow = Instantiate(arrow);
        GameObject playerBow = GameObject.Find("PlayerBow");
        playerBow.transform.eulerAngles = new Vector3(0, 0, 0);
        arrow.transform.SetParent(playerBow.transform);
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        arrow.GetComponent<ArrowBehaviour>().enabled = true;
        playerBow.GetComponent<BowBehaviour>().SetBowPosition(0);
        InputManager.hasSnap = false;
        timer = 0;
    }
    public void SetEnemy()
    {
        arrow = enemyArrow;
        arrow = Instantiate(arrow);
        GameObject enemyBow = GameObject.Find("EnemyBow");
        enemyBow.transform.eulerAngles = new Vector3(0, 0, 180);
        arrow.transform.SetParent(enemyBow.transform);
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        arrow.GetComponent<ArrowBehaviour>().enabled = true;
        enemyBow.GetComponent<BowBehaviour>().SetBowPosition(0);
        InputManager.hasSnap = false;
        timer = 0;
    }
    private void FixedUpdate()
    {
        if (InputManager.hasSnap && stage == Stage.Player)
        {
            if (shotPower < 1)
                bowBehaviour.SetBowPosition(0);
            else if (shotPower >= 1 && shotPower < 5)
                bowBehaviour.SetBowPosition(1);
            else if (shotPower >= 5 && shotPower < 10)
                bowBehaviour.SetBowPosition(2);
            else if (shotPower >= 10 && shotPower < 15)
                bowBehaviour.SetBowPosition(3);
            else
                bowBehaviour.SetBowPosition(4);
            bowBehaviour.SetBowRotation(angle);
        }
    }
    public static void SetAngle(float _angle)
    {
        angle = _angle;
    }
    public static void SetShotPower(float _shotPower)
    {
        shotPower = _shotPower;
    }
    public void SetStage(Stage _stage)
    {
        stage = _stage;
        staticStage = stage;
    }
    public Stage GetStage()
    {
        return stage;
    }
}
