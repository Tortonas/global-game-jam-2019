using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnExit()
    {
        Application.Quit();
	}

	public void OnLoadScene(string name)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OnEnableLight(Light light)
	{
		light.enabled = true;
	}

	public void OnDisableLight(Light light)
	{
		light.enabled = false;
	}
}
