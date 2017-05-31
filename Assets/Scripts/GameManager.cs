using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum Stage {
        Player, playershot,Enemy,EnemyShot
    }
    public static GameObject arrow;
    public static float angle;
    public static float shotPower;
    public static bool enemyCanShoot;

    [SerializeField]
    private GameObject Player_Arrow;    //Pode ser um vetor de flechas diferentes, com danos diferentes, por exemplo
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private BowBehaviour bowBehaviour;
    [SerializeField]
    private float timer;
    [SerializeField]
    private IABehaviour iaBehaviour;
    public static Stage staticStage; //Static é apenas para leitura
    private Stage stage; //enquanto este é para a escrita

    private void Awake()
    {
        stage = Stage.Player;
        SetPlayer();
        //ChangeArrow(0); //Precisa instanciar pelo codigo para que isso seja possivel
    }
    public void SetPlayer()
    {
        arrow = Player_Arrow;
        enemyCanShoot = false;
        arrow = Instantiate(arrow);
        GameObject playerBow= GameObject.Find("PlayerBow");
        playerBow.transform.eulerAngles = new Vector3(0, 0, 0);
        arrow.transform.SetParent(playerBow.transform);
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        bowBehaviour.SetBow(0);
        InputManager.hasSnap = false;
        timer = 0;
    }
    private void FixedUpdate()
    {
        if(stage == Stage.Enemy && FollowArrow.inPosition) //Passagem do turno do tiro do player para inimigo atirar
        {
            enemyCanShoot = true;
        }
            SetZoom(stage);
        if (InputManager.hasSnap && stage == Stage.Player)
        {
            if (shotPower < 10)
                bowBehaviour.SetBow(0);
            else if (shotPower >= 10 && shotPower < 50)
                bowBehaviour.SetBow(1);
            else if (shotPower >= 50 && shotPower < 100)
                bowBehaviour.SetBow(2);
            else if (shotPower >= 100 && shotPower < 150)
                bowBehaviour.SetBow(3);
            else
                bowBehaviour.SetBow(4);
            bowBehaviour.SetBowRotation(angle);
        }

        
    }
    //passar para o inputManager
    public void SetZoom(Stage stage)
    {
        if (stage == Stage.playershot)//Provavelmente vá precisar de mudanças
        {
            timer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10, timer / 15);
        }
        if (stage == Stage.Enemy)
        {
            timer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, timer / 15);
        }
    }
    public static void SetAngle(float _angle)
    {
        if(_angle < 90)
        {
            angle = _angle;
        }
    }
    public static void SetShotPower(float _shotPower)
    {
        shotPower = _shotPower;
    }
    public void SetStage(Stage _stage)
    {
        if (_stage == Stage.Player)
            SetPlayer();
        //if(_stage == Stage.Enemy)
            //SetEnemy();
        stage = _stage;
        staticStage = stage;
    }
    public Stage GetStage()
    {
        return stage;
    }
}
