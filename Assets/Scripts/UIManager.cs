using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public void StartGame()
    {
        SceneManager.LoadScene(1);
        //algum efeito sonoro talvez
    }
}
