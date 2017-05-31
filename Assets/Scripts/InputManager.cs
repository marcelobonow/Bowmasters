using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField]
    private Vector3 snapPosition;
    [SerializeField]
    private bool hasSnap;

	void Start () {
        hasSnap = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(GameManager.canShoot)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire1"))
            {
                hasSnap = false;
            }
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
                Debug.Log("teste");
                Vector3 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float angle = (snapPosition.y - newMousePosition.y) * 20;
                float shotPower = Vector3.Distance(snapPosition,newMousePosition) * 1000;
                GameManager.SetAngle(angle);
                GameManager.SetShotPower(shotPower);
                Debug.Log("Shot power: " + shotPower);
                Debug.Log("angle: " + angle);
            }
        }
        
    }
}
