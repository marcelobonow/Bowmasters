//esse script cuida do Input e passa as informações para o GameManager tratar
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Vector3 snapPosition;
    public static bool hasSnap;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]

    void Start () {
        hasSnap = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.staticStage == GameManager.Stage.Player)
        {
            if (!hasSnap && CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                snapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//varia a força e angulo baseado
                hasSnap = true;                                                    //em onde a pessoa clicou
            }
            if (hasSnap)
            {
                //Angulo é calculado pela diferença no eixo y e a força na diferença do eixo x
                Vector3 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = (snapPosition.y - newMousePosition.y)/5; //mudar depois
                float shotPower = Mathf.Abs((snapPosition.x - newMousePosition.x)*4f);
                if (shotPower > 20)
                    shotPower = 20;
                Camera.main.orthographicSize = (shotPower/8f)+ 3;
                GameManager.SetAngle(angle);
                GameManager.SetShotPower(shotPower);
            }
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                hasSnap = false;
                if(GameManager.shotPower >= 2)
                {
                    gameManager.SetStage(GameManager.Stage.playershot); // Passagem da rodada de jogador atirar para tiro do jogador
                    GameManager.cameraInPosition = false;
                    ShootingBehaviour.Shot(GameManager.shotPower, GameManager.angle, GameManager.arrow);
                }
            }
        }
    }
}
