using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameObject arrow;
    public static bool canShoot;
    public static float angle;
    public GameObject[] arrowArray;
    public int arrowSelect;
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
        if(CrossPlatformInputManager.GetButtonUp("Fire1")&&canShoot)
        {
            shootBehaviour.Shoot(angle, arrow);
            canShoot = false;
        }
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
}
