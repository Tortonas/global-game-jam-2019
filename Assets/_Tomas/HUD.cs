using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public Text foodText;
    public Text scoreText;
    public Text stoneText;
    public Text woodText;

    void Start()
    {
        if(instance == null)
        {
            //DontDestroyOnLoad(this);
            instance = this;
        }
    }

    void Update()
    {
        scoreText.text = Mathf.RoundToInt(Resources.Score).ToString();
        stoneText.text = Resources.Stone.ToString();
        woodText.text =  Resources.Wood.ToString();
        foodText.text = Resources.Food.ToString();
    }

}
