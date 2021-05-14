using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;


public class Dialogue : MonoBehaviour
{
    public string startNode = "";
    static List<YarnProgram> addedPrograms = new List<YarnProgram>();

    [Header("Optional")]
    public YarnProgram dialogueScript;

    public virtual void Start()
    {
        if (dialogueScript != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            if (!addedPrograms.Contains(dialogueScript))
            {
                dialogueRunner.Add(dialogueScript);
                addedPrograms.Add(dialogueScript);
            }

        }
    }

    public void StartDialogue()
    {
        // Kick off the dialogue at this node.
        FindObjectOfType<DialogueRunner>().StartDialogue(startNode);
    }
    public void StartDialogue(string node)
    {
        // Kick off the dialogue at this node.
        FindObjectOfType<DialogueRunner>().StartDialogue(node);
    }
}