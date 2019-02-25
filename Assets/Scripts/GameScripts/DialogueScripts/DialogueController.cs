﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class DialogueController {

    private static UIScript uiScript;
    private static readonly string fileName = "/tutorialDialogues.json";
    //private static string jsonString;
    private static string path = Application.streamingAssetsPath + fileName;

    private static DialogueClass dialogues;

    private static ushort currentDialogueID = 0;

    public static void StartDialogueController(GameManagerScript gameManager)
    {
        //path = Application.streamingAssetsPath + fileName;
        if (File.Exists(path))
        {
            Debug.Log("TUTORIAL DIALOGUE FILE FOUND");
            string jsonString = File.ReadAllText(path);

            dialogues = JsonUtility.FromJson<DialogueClass>(jsonString);

            uiScript = gameManager.uiScript;
            uiScript.SetBotDialoguePanelText("SYSTEM", dialogues.aiDialogues[currentDialogueID]);
            uiScript.ToggleBotDialoguePanel(true);

            string quest = "";
            for (int i = 0; i < dialogues.questDialogues.GetLength(0); i++)
            {
                quest += dialogues.questDialogues[i];
                quest += "\n";
            }
            uiScript.SetQuestPanelText("Starting Out", quest);
        }
        else
        {
            Debug.LogError("TUTORIAL DIALOGUE FILE CANT BE FOUND");
        }
    }

    public static void NextDialogue()
    {
        currentDialogueID++;
        if(currentDialogueID < dialogues.aiDialogues.GetLength(0))
        {
            uiScript.SetBotDialoguePanelText("SYSTEM", dialogues.aiDialogues[currentDialogueID]);
        }
    }
}


[System.Serializable]
class DialogueClass
{
    public string[] aiDialogues;
    public string[] questDialogues;
}