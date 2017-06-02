//esse script cuida do Input e passa as informações para o GameManager tratar
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Vector3 snapPosition;
    public static bool hasSnap; //Forma de identificação de que o player esta mirando
    [SerializeField]
    HUDManager hud; //objeto do HUD para criar, atualizar e desabilitar popUp

    void Start () {
        hasSnap = false;
	}
	void Update () {

        if (GameManager.GetStage() == GameManager.Stage.PlayerAim)
        {
            if (hasSnap)
            {
                Vector3 newMousePosition = Input.mousePosition;
                //Angulo é calculado pela diferença no eixo y e a força na diferença do eixo y
                float angle = (snapPosition.y - newMousePosition.y)/200;
                if(angle > Mathf.PI)
                {
                    angle = Mathf.PI;
                }
                float shotPower = Mathf.Abs((snapPosition.x - newMousePosition.x)/20);//diferença no eixo x é a força do tiro
                if (shotPower > 20)
                    shotPower = 20;
                Camera.main.orthographicSize = (shotPower/8f)+ 3; //Afasta a camera de acordo com a força do tiro
                GameManager.SetAngle(angle);
                GameManager.shotPower = shotPower;//Guarda a força do tiro no GameManager
                if (shotPower >= 2)
                {
                    hud.EnablePopUp();
                    hud.UpdatePopUp(angle, shotPower);
                }
                else
                    hud.DisablePopUp();
            }
            if (!hasSnap && CrossPlatformInputManager.GetButtonDown("Fire1")) //Começou a mirar
            {
                snapPosition = Input.mousePosition;//varia a força e angulo baseado em onde a pessoa clicou
                hasSnap = true;                    //Player está mirando
                hud.CreatePopUp(snapPosition);
            }
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))//Player deixou de mirar
            {
                hasSnap = false;
                if(GameManager.shotPower >= 2)//Se o tiro for muito fraco a flecha não é disparada
                {
                    GameManager.SetStage(GameManager.Stage.playershot); // Passagem da rodada de jogador atirar para tiro do jogador
                    GameManager.cameraInPosition = false;
                    hud.DisablePopUp();
                    ShootingBehaviour.Shot(GameManager.shotPower, GameManager.angle, GameManager.arrow);
                }
            }
        }
    }
}
