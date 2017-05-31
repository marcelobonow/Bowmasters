using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Vector3 snapPosition;
    public static bool hasSnap;
    [SerializeField]
    private ShootingBehaviour shootBehaviour;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private float shotPower;
    [SerializeField]
    private float angle;

    void Start () {
        hasSnap = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.staticStage == GameManager.Stage.Player)
        {
            if (!hasSnap && CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                snapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hasSnap = true;
            }
            if (hasSnap)
            {
                /* O jogo original pega a diferença absoluta no eixo x entre a posição atual do mouse e o snap
                 * para dar a força, eu achei não muito intuitivo e preferi usar a distancia somando o eixo x
                 *  e eixo y */
                Vector3 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                angle = (snapPosition.y - newMousePosition.y) * 20;
                shotPower = Vector3.Distance(snapPosition, newMousePosition)*200;
                if (shotPower > 200)
                    shotPower = 200;
                GameManager.SetAngle(angle);
                GameManager.SetShotPower(shotPower);
            }
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                hasSnap = false;
                gameManager.SetStage(GameManager.Stage.playershot);
                shootBehaviour.Shot(shotPower, angle, GameManager.arrow);
            }
        }
    }
}
