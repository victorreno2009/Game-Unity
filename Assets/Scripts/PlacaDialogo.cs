using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacaDialogo : MonoBehaviour
{
    public string[] dialogueNpc;
    public int dialogueIndex;

    public GameObject dialoguePanel;
    public Text dialogueText;

    public bool readyToSpeak;
    public bool startDialogue;

    private RoboController robo;

    void Start()
    {
        dialoguePanel.SetActive(false);
        robo = FindObjectOfType<RoboController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && readyToSpeak)
        {
            if (!startDialogue)
            {
                StartDialogue();
            }
            else
            {
                ShowNextSentence();
            }
        }
    }

    void StartDialogue()
    {
        startDialogue = true;
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        robo.canMove = false;
        dialogueText.text = dialogueNpc[dialogueIndex];
    }

    void ShowNextSentence()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogueNpc.Length)
        {
            dialogueText.text = dialogueNpc[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        startDialogue = false;
        dialoguePanel.SetActive(false);
        robo.canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            readyToSpeak = false;
            if (startDialogue)
            {
                EndDialogue(); // encerra se o jogador sair no meio do diálogo
            }
        }
    }
}
