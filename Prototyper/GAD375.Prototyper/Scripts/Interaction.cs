using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Yarn.Unity.Example;
using Yarn.Unity;
using GAD375.Prototyper;

namespace GAD375.Prototyper
{
    public class Interaction : MonoBehaviour
    {
        public float interactionRadius = 1.0f;
        private DialogueRunner dialoguerunner;
        private DialogueUI dialogueui;
        private SimpleCharacterController controller;
        public KeyCode interactKey;

        void Awake()
        {
            //controller = GetComponent<SimpleCharacterController>();
            dialoguerunner = FindObjectOfType<DialogueRunner>();
            dialogueui = FindObjectOfType<DialogueUI>();
        }
        // Start is called before the first frame update
        void Start()
        {
            dialoguerunner.onNodeComplete.AddListener(FinishedNode);
            dialogueui.onDialogueStart.AddListener( StartedDialogue);
            dialogueui.onDialogueEnd.AddListener( FinishedDialogue );
        }

        // Update is called once per frame
        void Update()
        {
            // Detect if we want to start a conversation
            if (Input.GetKeyDown(interactKey)) 
            {
                Interactable i = CheckForNearbyInteractable();
                if (i != null)
                {
                    i.Interact(gameObject); //interact, passing ourself
                }
            }
        }

        [YarnCommand("respawn")]
        public void Respawn()
        {
            Checkpoint c = GameManager.instance.currentCheckPoint;
            if (c != null)
            {
                c.Respawn(gameObject);
            }
        }

        Interactable CheckForNearbyInteractable()
        {
            var allParticipants = new List<Interactable> (FindObjectsOfType<Interactable> ());
            var target = allParticipants.Find (delegate (Interactable p) 
            {
                return p.enabled && // is enabled 
                p.OnInteract.GetPersistentEventCount() > 0 && // has some interaction defined
                ((p.transform.position - this.transform.position)// is in range?
                .magnitude <= (interactionRadius + p.radius));
            });
            return (Interactable)target;
        }

        public void FinishedNode(string s)
        {
            Debug.Log("Finished node: " + s);
        }
        public void FinishedDialogue()//string s)
        {
            Debug.Log("Finished dialogue "); // + s);
            //controller.EnableFPSControls(true);
        }
        public void StartedDialogue()
        {
            Debug.Log("Started Dialogue");
            //controller.EnableFPSControls(false);
        }
    }
}
