using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingBtnControler : MonoBehaviour
{
    /*public GameObject upSprite;
    public GameObject downSprite;
    public float downTime = 0.1f;*/
    private DataBase databaseAcces;
    public GameStates stateManager = null;
    private int counter = 0;
    /*private enum buttonStates
    {
        up = 0,
        down
    }
    private buttonStates currentState = buttonStates.up;
    private float nextStateTime = 0.0f;
    void Start()
    {
        databaseAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DataBase>();
        //upSprite.SetActive(true);
        //downSprite.SetActive(false);
    }
    /*void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == RankingBtnControler.buttonStates.up)
        {
            nextStateTime = Time.time + downTime;
            upSprite.SetActive(false);
            downSprite.SetActive(true);
            currentState = RankingBtnControler.buttonStates.down;
        }
    }
    void Update()
    {
        if (nextStateTime > 0.0f)
        {
            if (nextStateTime < Time.time)
            {
                nextStateTime = 0.0f;
                upSprite.SetActive(true);
                downSprite.SetActive(false);
                currentState = RankingBtnControler.buttonStates.up;
                // Retornar el botó a estat “no polsat”
                if (stateManager.RankingContainer.active)
                {
                    stateManager.TitleGame();
                }
                else
                {
                    stateManager.RankingGame();
                    databaseAcces.PonerListaRanking();
                }            
                
            }
        }
    }*/

    public void rankingBtnfunc()
    {
        databaseAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DataBase>();
        counter++;
        if (counter % 2 == 0)
        {
            stateManager.TitleGame();
        }
        else
        {
            stateManager.RankingGame();
            databaseAcces.PonerListaRanking();
        }
    }
}