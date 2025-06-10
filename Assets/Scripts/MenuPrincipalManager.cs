using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private Button ButtonPlay;
    [SerializeField] private Button ButtonQuit;

    private void Awake() {

        ButtonPlay.onClick.AddListener(OnButtonPlayClick);
        ButtonQuit.onClick.AddListener(OnButtonQuitClick);
    }
    
    private void OnButtonPlayClick() {
        Debug.Log("JOGAR");
        SceneManager.LoadScene("MenuTutorial");
    }

    public void OnButtonQuitClick() {

         Debug.Log("Saindo do jogo...");
         Application.Quit();
    }
}
