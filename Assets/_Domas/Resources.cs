using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources instance;

    [Header("Resources")]
    [SerializeField]
    private int _wood = 0;
    public static int Wood { get { return instance._wood; } }

    [SerializeField]
    private int _stone = 0;
    static public int Stone { get { return instance._stone; } }

    [SerializeField]
    private int _food = 0;
    public static int Food { get { return instance._food; } }

    [Header("Score")]
    [SerializeField]
    private float _score = 0;
    static public float Score { get { return instance._score; } }


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
    }

    /// <summary>
    /// Adds the specified amount of wood to the player.
    /// </summary>
    /// <param name="wood">Amount of wood to add (pass negative number to subtract)</param>
    private void _addWood(int wood)
    {
        _wood += wood;
        if (_wood < 0)
        {
            Debug.Log(string.Format("Resources::Wood = {0} - something bad must have happened! Setting Wood to 0",_wood));
            _wood = 0;
        }
    }

    /// <summary>
    /// Adds the specified amount of wood to the player.
    /// </summary>
    /// <param name="wood">Amount of wood to add (pass negative number to subtract)</param>
    static public void AddWood(int wood)
    {
        instance._addWood(wood);
    }

    /// <summary>
    /// Adds the specified amount of stone to the player.
    /// </summary>
    /// <param name="stone">Amount of stone to add (pass negative number to subtract)</param>
    private void _addStone(int stone)
    {
        _stone += stone;
        if (_stone < 0)
        {
            Debug.Log(string.Format("Resources::Stone = {0} - something bad must have happened! Setting stone to 0",_stone));
            _stone = 0;
        }
    }

    /// <summary>
    /// Adds the specified amount of stone to the player.
    /// </summary>
    /// <param name="stone">Amount of stone to add (pass negative number to subtract)</param>
    public static void AddStone(int stone)
    {
        instance._addStone(stone);
    }

    /// <summary>
    /// Adds the specified amount of score to the player.
    /// </summary>
    /// <param name="score">Amount of score to add (pass negative number to subtract)</param>
    private void _addScore(float score)
    {
        _score += score;
        if (_score < 0)
        {
            Debug.Log(string.Format("Resources::Score = {0} - something bad must have happened! Setting score to 0", _score));
            _score = 0;
        }
    }

    /// <summary>
    /// Adds the specified amount of score to the player.
    /// </summary>
    /// <param name="score">Amount of score to add (pass negative number to subtract)</param>
    static public void AddScore(float score)
    {
        instance._addScore(score);
    }

    /// <summary>
    /// Adds the specified amount of food to the player.
    /// </summary>
    /// <param name="food">Amount of food to add (pass negative number to subtract)</param>
    static public void AddFood(int food)
    {
        instance._addFood(food);
    }

    /// <summary>
    /// Adds the specified amount of food to the player.
    /// </summary>
    /// <param name="food">Amount of food to add (pass negative number to subtract)</param>
    private void _addFood(int food)
    {
        _food += food;
        if (_food < 0)
        {
            Debug.Log(string.Format("Resources::Food = {0} - something bad must have happened! Setting food to 0", _food));
            _food = 0;
        }
    }



}
