//Script que controla a interface visual como um todo
//É possível exibir tela de vitoria e derrota
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Image logo;
    [SerializeField]
    private Image win;
    [SerializeField]
    private Image loose;
    
    public void StartGame()
    {
        SceneManager.LoadScene(3);
        //algum efeito sonoro talvez
    }
}
