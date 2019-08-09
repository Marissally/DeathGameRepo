using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Yarn.Unity.Example
{
    public class PlayerScript : MonoBehaviour
    {

        public bool controllable;
        public List<string> inventory;
        public GameObject TalkUI;
        private NavMeshAgent navAgent;
        private ChangeMouse MouseStuff;

        // Use this for initialization
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            MouseStuff = GetComponent<ChangeMouse>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(MouseStuff.CurrentSelection == "Character")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit))
                    {
                        Talk(hit.collider.gameObject.GetComponent<NPC>());
                    }
                }
                if(MouseStuff.CurrentSelection == "Item")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit))
                    {
                        Take(hit.collider.gameObject);
                    }
                }
                if(MouseStuff.CurrentSelection == "Detail")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit))
                    {
                        //Look();
                    }
                }

                else if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        navAgent.destination = hit.point;
                    }
                }
            }
        }

        void Talk(NPC target)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
        }

        void Take(GameObject item)
        {
            //Take item and give dialogue
            inventory.Add(item.name);
            Destroy(item);
        }

        void Look()
        {
            //Give description of item in textbox
        }

        //How to do commands?
        //Commands could be consolidated into pressing E next to things?
    }

}
