using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum Stages {
        Player, playershot,enemy,enemyshot
    }
    public static Stages actualStage;
    public static GameObject arrow;
    public static float angle;
    public static float shotPower;
    public static bool enemyCanShoot;
    public GameObject arrowPlayerPrefab;
    public GameObject arrowEnemyPrefab;
    [SerializeField]
    private ShootingBehaviour shootBehaviour;
    [SerializeField]
    private BowBehaviour bowBehaviour;
    [SerializeField]
    private float timer;
    [SerializeField]
    private IABehaviour iaBehaviour;

    private void Awake()
    {
        actualStage = Stages.Player;
        enemyCanShoot = false;
        arrow = Instantiate(arrowPlayerPrefab);
        arrow.transform.SetParent(GameObject.Find("Player_Bow_Sprite").transform);
        bowBehaviour.SetBow(0);
        //ChangeArrow(0); //Precisa instanciar pelo codigo para que isso seja possivel
    }
    public void Update()
    {
        if (actualStage == Stages.Player)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                Debug.Log(shotPower);
                shootBehaviour.Shot(shotPower, angle, arrow);
                actualStage = Stages.playershot;
            }
        }
    }
    private void FixedUpdate()
    {
        iaBehaviour.enabled = actualStage == Stages.enemy ? true : false;
        if (InputManager.hasSnap && actualStage == Stages.Player)
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
        if(actualStage == Stages.enemy && Camera.main.orthographicSize > 3.01)
        {
            enemyCanShoot = true;
        }
            SetZoom(actualStage);
        
    }
    public void SetZoom(Stages stage)
    {
        if (stage == Stages.playershot)
        {
            timer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10, timer / 15);
        }
        if (stage == Stages.enemy)
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
        if (_shotPower > 200)
            shotPower = 200;
        else
            shotPower = _shotPower;
    }
}
