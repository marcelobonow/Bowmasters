using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Vector3 snapPosition;
    public static bool hasSnap;

	void Start () {
        hasSnap = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            hasSnap = false;
        }
        if (GameManager.canShoot)
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
                float angle = (snapPosition.y - newMousePosition.y) * 20;
                float shotPower = Vector3.Distance(snapPosition,newMousePosition) * 1000;
                GameManager.SetAngle(angle);
                GameManager.SetShotPower(shotPower);
            }
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);   
        }
        
    }
}
