using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightEnabler : MonoBehaviour
{
	public Light lightO;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	}

	//Do this when the mouse is clicked over the selectable object this script is attached to.
	public void OnMouseOver()
	{
		lightO.gameObject.SetActive(true);
	}
	
	public void OnMouseExit()
	{
		lightO.gameObject.SetActive(false);
	}
}
