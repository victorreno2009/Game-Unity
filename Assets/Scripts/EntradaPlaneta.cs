using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrada : MonoBehaviour
{
    [SerializeField] private string planeta1;

    private void OnTriggerEnter2D(Collider2D collision){
        IrPlaneta1();
    }

    private void IrPlaneta1(){
        SceneManager.LoadScene("Planet1Level");
    }

}
