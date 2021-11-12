using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace GAD375.Prototyper
{

    public class Dialogue : MonoBehaviour
    {
        public string startNode = "";
        //static List<YarnProgram> addedPrograms = new List<YarnProgram>();

        //[Header("Optional")]
        //public YarnProgram dialogueScript;

        public virtual void Awake()
        {
            
        }

        public virtual void Start()
        {
            //No need for this anymore with the introduction of
            //Yarn Projects?
            /*
            if (dialogueScript != null)
            {
                DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
                if (!addedPrograms.Contains(dialogueScript))
                {
                    dialogueRunner.Add(dialogueScript);
                    addedPrograms.Add(dialogueScript);
                }

            }
            */
        }

        public virtual void StartDialogue()
        {
            // Kick off the dialogue at this node.
            StartDialogue(startNode);
        }
        public virtual void StartDialogue(string node)
        {
            // Kick off the dialogue at this node.
            FindObjectOfType<DialogueRunner>().StartDialogue(node);
        }
    }
}