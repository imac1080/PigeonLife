using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginBtn : MonoBehaviour
{
    public GameObject upSprite;
    public GameObject downSprite;
    public GameObject inputField;
    public TextMesh textDisplay;
    public DataBase v_dataBase = null;
    public float downTime = 0.1f;
    private DataBase databaseAcces;
    private enum buttonStates
    {
        up = 0,
        down
    }
    private buttonStates currentState = buttonStates.up;
    private float nextStateTime = 0.0f;
    void Start()
    {
        databaseAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DataBase>();
        upSprite.SetActive(true);
        downSprite.SetActive(false);
    }
    void OnMouseDown()
    {
            //Debug.Log(inputField.GetComponent<Text>().text);
            upSprite.SetActive(true);
            downSprite.SetActive(false);
        //Debug.Log(v_dataBase.GetScoresFromDataBase());
        //textDisplay.text = inputField.GetComponent<Text>().text;
        //textDisplay.text = "s";
        //DisplayHighScoreInTEXTMESH();
            //List<Ranking> ranking = new List<Ranking>();
            //ranking = v_dataBase.GetScoresFromDataBase();
    }
    void Update()
    {

    }
    private async void DisplayHighScoreInTEXTMESH()
    {
        var task = databaseAcces.GetScoresFromDataBase();
        var result = await task;
        var output = "";
        foreach (var score in result)
        {
            //output += score.UserName + " Score: " + score.Score;
        }
        textDisplay.text = output;
    }
}