using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseSystem : MonoBehaviour
{
    public static LoseSystem instance;

    public GameObject LooseUI;
    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void PlayerDied()
	{
        score.text = "Final score: " + Mathf.Round(Resources.Score);
        LooseUI.SetActive(true);
	}
}
