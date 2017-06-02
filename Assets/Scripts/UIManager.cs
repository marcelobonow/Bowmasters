//Script que controla a interface visual como um todo
//É possível exibir tela de vitoria e derrota
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        //algum efeito sonoro talvez
    }
}
