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
    private GameManager gameManager;
    public GameObject arrowPlayerPrefab;
    public GameObject arrowEnemyPrefab;

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
        SetPlayer();
        //ChangeArrow(0); //Precisa instanciar pelo codigo para que isso seja possivel
    }
    public void SetPlayer()
    {
        enemyCanShoot = false;
        arrow = Instantiate(arrowPlayerPrefab);
        GameObject Player_Bow_Sprite= GameObject.Find("Player_Bow_Sprite");
        Player_Bow_Sprite.transform.eulerAngles = new Vector3(0, 0, 0);
        arrow.transform.SetParent(Player_Bow_Sprite.transform);
        arrow.GetComponent<ArrowBehaviour>().gameManager = this;
        bowBehaviour.SetBow(0);
        InputManager.hasSnap = false;
        timer = 0;
    }
    private void FixedUpdate()
    {
        iaBehaviour.enabled = stage == Stage.Enemy ? true : false;
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

        if(stage == Stage.Enemy && Camera.main.orthographicSize < 3.01)
        {
            enemyCanShoot = true;
        }
            SetZoom(stage);
        
    }
    public void SetZoom(Stage stage)
    {
        if (stage == Stage.playershot)
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
