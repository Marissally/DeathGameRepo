using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Toggle(GameObject item)
    {
        if(item.activeSelf == true)
        {
            item.SetActive(false);
        }
        else
        {
            item.SetActive(true);
        }
    }
}
