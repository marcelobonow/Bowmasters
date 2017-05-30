using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameObject arrow;
    public GameObject[] arrowArray;
    public int arrowSelect;
    [SerializeField]
    private ShootingBehaviour shootBehaviour;
    public static bool inAir = false; //mudar depois para a maquina de estados;

    private void Awake()
    {
        arrow = GameObject.Find("Arrow");
        //ChangeArrow(0); //Precisa instanciar pelo codigo para que isso seja possivel
    }
    void Update () {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            shootBehaviour.Shoot(15, arrow);
            inAir = true;
        }
	}
    void ChangeArrow(int id)
    {
        arrow = arrowArray[id];
    }
}
