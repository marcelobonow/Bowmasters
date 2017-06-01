/* Esse script posiciona os preview prefabs na posição que a flecha vai estar, para mostrar ao player um trecho da tragetoria
   que a flecha irá percorrer. Isto so acontece enquanto o player estiver mirando (representado por hasSnap)
 */
using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour {
    [SerializeField]
    private GameObject previewPrefab;
    [SerializeField]
    private Transform previewStartPosition;
    [SerializeField]
    private List<GameObject> previews = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject tempPreview = Instantiate(previewPrefab);
            tempPreview.name = i.ToString();
            previews.Add(tempPreview);
        }
    }

    void FixedUpdate () {
		if(InputManager.hasSnap)
        {
            float horizontalSpeed;
            float verticalSpeed;
            horizontalSpeed = GameManager.shotPower * (Mathf.Cos(GameManager.angle));
            verticalSpeed = GameManager.shotPower * (Mathf.Sin(GameManager.angle));
            for (int i = 0; i < previews.Count; i++)
            {
                float time = (float) i / 10;
                GameObject tempPreview = previews[i];
                tempPreview.SetActive(true);
                //Coloca o prefab na posição correta,há um offset, que é a posição previewStartPoistion
                tempPreview.transform.localPosition = new Vector2(horizontalSpeed * time + previewStartPosition.position.x,
                    (verticalSpeed * time - ((time * time * 10) / 2)) + previewStartPosition.position.y-0.1f);
                if (i == 0)
                    tempPreview.transform.position = previewStartPosition.position;
            }
        }
        else
        {
            for(int i = 0; i < previews.Count; i++)
            {
                previews[i].SetActive(false);
            }
        }
	}
}
