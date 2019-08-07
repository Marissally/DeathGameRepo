using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMouse : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public Texture2D TalkCursor;
    public Texture2D LookCursor;
    public Texture2D GrabCursor;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "Character")
            {
                Cursor.SetCursor(TalkCursor, Vector2.zero, CursorMode.Auto);
            }

            if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(GrabCursor, Vector2.zero, CursorMode.Auto);
            }

            if (hit.collider.gameObject.tag == "Detail")
            {
                Cursor.SetCursor(LookCursor, Vector2.zero, CursorMode.Auto);
            }

            else if(hit.collider.gameObject.tag == "Untagged")
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }

    }
}
