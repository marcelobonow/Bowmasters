using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameObject arrow;
    public static bool canShoot;
    public static float angle;
    public GameObject[] arrowArray;
    public int arrowSelect;
    public static float shotPower;
    [SerializeField]
    private ShootingBehaviour shootBehaviour;
    public static bool inAir; //mudar depois para a maquina de estados;

    private void Awake()
    {
        canShoot = true;
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
            }

        }
    }
    private void FixedUpdate()
    {
        if (shotPower < 200)
        {
            shootBehaviour.SetBow(0);
        }
        else if (shotPower >= 200 && shotPower < 600)
            shootBehaviour.SetBow(1);
        else
            shootBehaviour.SetBow(2);
    }

    void ChangeArrow(int id)
    {
        arrow = arrowArray[id];
    }
    public static void SetinAir(bool _value)
    {
        inAir = _value;
    }
    public static void SetAngle(float _angle)
    {
        angle = _angle;
       
    }
    public static void SetShotPower(float _shotPower)
    {
        shotPower = _shotPower;
        
    }
}
