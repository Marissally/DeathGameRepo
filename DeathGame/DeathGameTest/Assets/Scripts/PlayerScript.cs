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
            inventory = GetComponent<Inventory>();
            navAgent = GetComponent<NavMeshAgent>();
            MouseStuff = GetComponent<ChangeMouse>();
            selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(selectedItem.name);
                if(MouseStuff.CurrentSelection == "Character")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit) && selectedItem != null)
                    {
                        Use();
                    }
                    else if (Physics.Raycast(nameRay, out hit))
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
                if (MouseStuff.CurrentSelection == "Detail")
                {
                    RaycastHit hit;
                    var nameRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(nameRay, out hit) && selectedItem != null)
                    {
                        Use();
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

        void Talk(NPC target)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(target.talkToNode);
        }

        void Take(GameObject item)
        {
            //Take item and give dialogue
            print(item.name);
            inventory.GiveItem(item.name);
            Destroy(item);
        }

        void Look(NPC thingToLookAt)
        {
            //Give description of item in textbox
            Talk(thingToLookAt);
        }

        void Use()
        {
            Debug.Log("A use!");
            Talk(selectedItem.GetComponent<NPC>());
            selectedItem.UpdateItem(null);
        }
    }
}
