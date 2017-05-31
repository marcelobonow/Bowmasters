using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameObject arrow;
    public static bool canShoot;
    public static float angle;
    public static float shotPower;
    public static bool inAir; //mudar depois para a maquina de estados;
    [SerializeField]
    private ShootingBehaviour shootBehaviour;
    [SerializeField]
    private BowBehaviour bowBehaviour;
    [SerializeField]
    private static int zoom;
    [SerializeField]
    private float timer;

    private void Awake()
    {
        canShoot = true;
        inAir = false;
        zoom = 0;
        arrow = GameObject.Find("Arrow"); //Apagar esta linha e descomentar a próxima
        //ChangeArrow(0); //Precisa instanciar pelo codigo para que isso seja possivel
    }
    public void Update()
    {
        if (canShoot)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                shootBehaviour.Shot(shotPower, angle, arrow);
                canShoot = false;
                zoom = 1;
            }

        }
    }
    private void FixedUpdate()
    {
        if (InputManager.hasSnap)
        {
            if (shotPower < 100)
            {
                bowBehaviour.SetBow(0);
            }
            else if (shotPower >= 100 && shotPower < 600)
                bowBehaviour.SetBow(1);
            else if (shotPower >= 600 && shotPower < 1100)
                bowBehaviour.SetBow(2);
            else if (shotPower >= 1100 && shotPower < 1600)
                bowBehaviour.SetBow(3);
            else
                bowBehaviour.SetBow(4);
            bowBehaviour.SetBowRotation(angle);
        }
        if(zoom == -1 && Camera.main.orthographicSize == 3)
        {
            zoom = 0;
        }
        if(zoom == 1)
        {
            timer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 10, timer / 15);
        }
        if(zoom == -1)
        {
            timer += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, timer / 15);
        }
    }

    public static void SetinAir(bool _value)
    {
        inAir = _value;
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
        if (_shotPower > 2000)
            shotPower = 2000;
        else
            shotPower = _shotPower;
    }
    public static void SetCameraAnimatorState(int _value)
    {
        zoom = _value;
    }
}
