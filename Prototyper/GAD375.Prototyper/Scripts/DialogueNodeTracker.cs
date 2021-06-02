﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
namespace GAD375.Prototyper
{
// Field ... is never assigned to and will always have its default value null
#pragma warning disable 0649

    public class DialogueNodeTracker : MonoBehaviour
    {

        // The dialogue runner that we want to attach the 'visited' function to
#pragma warning disable 0649
    [SerializeField] Yarn.Unity.DialogueRunner dialogueRunner;
#pragma warning restore 0649

        private HashSet<string> _visitedNodes = new HashSet<string>();

        void Awake()
        {
            // Register a function on startup called "visited" that lets Yarn
            // scripts query to see if a node has been run before.
            dialogueRunner.AddFunction("visited", delegate (string nodeName)
            {
                return _visitedNodes.Contains(nodeName);
            });

        }

        // Called by the Dialogue Runner to notify us that a node finished
        // running. 
        public void NodeComplete(string nodeName) {
            // Log that the node has been run.
            _visitedNodes.Add(nodeName);
        }  

        // Called by the Dialogue Runner to notify us that a new node
        // started running. 
        public void NodeStart(string nodeName)
        {
            // Log that the node has been run.

            var tags = new List<string>(dialogueRunner.GetTagsForNode(nodeName));

            Debug.Log($"Starting the execution of node {nodeName} with {tags.Count} tags.");
        } 
    }
}
