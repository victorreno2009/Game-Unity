using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
   [SerializeField] private Button ButtonPlay;

    private void Awake() {

        ButtonPlay.onClick.AddListener(OnButtonPlayClick);
       
    }

    private void OnButtonPlayClick() {
        Debug.Log("Inicio");
        SceneManager.LoadScene("SpaceLevel");
    }
}
