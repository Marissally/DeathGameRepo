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
        public Inventory inventory;
        public GameObject TalkUI;
        public UIItem selectedItem;
        private NavMeshAgent navAgent;
        private ChangeMouse MouseStuff;

        // Use this for initialization
        void Start()
        {
            controllable = true;
            inventory = GetComponent<Inventory>();
            navAgent = GetComponent<NavMeshAgent>();
            MouseStuff = GetComponent<ChangeMouse>();
            selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && controllable)
            {
                if(MouseStuff.CurrentSelection == "Character")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit) && selectedItem.item != null)
                    {
                        Use();
                    }
                    else if (Physics.Raycast(nameRay, out hit))
                    {
                        StartDialogue(hit.collider.gameObject.GetComponent<NPC>(), "Talk");
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
                if (MouseStuff.CurrentSelection == "Detail")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit) && selectedItem.item != null)
                    {
                        Use();
                    }
                }

                else if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && MouseStuff.CurrentSelection == null)
                {
                    string currentSelection = MouseStuff.CurrentSelection;
                    print(currentSelection);
                    print(MouseStuff.CurrentSelection);
                    Debug.Log("Move");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {
                        navAgent.destination = hit.point;
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (MouseStuff.CurrentSelection == "Character" || MouseStuff.CurrentSelection == "ItemInInventory" || MouseStuff.CurrentSelection == "Detail")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit))
                    {
                        Look(hit.collider.gameObject.GetComponent<NPC>());
                    }
                }
            }
        }

        void StartDialogue(NPC target, string dialogueType)
        {
            if(dialogueType == "Take" && target.takeNode != null)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(target.takeNode);
            }
            if(dialogueType == "Look" && target.lookAtNode != null)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(target.lookAtNode);
            }
            if(dialogueType == "Talk" && target.talkToNode != null)
            {
                FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
            }
            if(dialogueType == "Use" && target.useOnNode != null)
            {
                print(target.useOnNode + target.name);
                FindObjectOfType<DialogueRunner>().StartDialogue(target.useOnNode);
            }
        }

        void Take(GameObject item)
        {
            //Take item and give dialogue
            print(item.name);
            StartDialogue(item.transform.parent.GetComponent<NPC>(), "Take");
            inventory.GiveItem(item.name);
            Destroy(item);
        }

        void Look(NPC thingToLookAt)
        {
            //Give description of item in textbox
            StartDialogue(thingToLookAt, "Look");
        }

        void Use()
        {
            Debug.Log("A use!");
            print(selectedItem.name);
            print(selectedItem.GetComponent<Image>().sprite.name);
            GameObject itemUsed = GameObject.Find(selectedItem.GetComponent<Image>().sprite.name + "Dialogue");
            print(itemUsed.name);
            StartDialogue(itemUsed.GetComponent<NPC>(), "Use");
            itemUsed = null;
            selectedItem.UpdateItem(null);
        }
    }
}
