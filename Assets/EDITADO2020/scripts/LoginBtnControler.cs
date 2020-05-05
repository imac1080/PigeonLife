using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginBtnControler : MonoBehaviour
{
    //public GameObject upSprite;
    //public GameObject downSprite;
    //public float downTime = 0.1f;
    public GameStates stateManager = null;
    private DataBase databaseAcces;
    public Sprite Image1;
    public Sprite Image2;
    public Button v_boton;
    private int counter = 0;
    /*private Boton botonAcces;
    private enum buttonStates
    {
        up = 0,
        down
    }
    private buttonStates currentState = buttonStates.up;
    private float nextStateTime = 0.0f;*/
    void Start()
    {
        //v_boton = GetComponent<Button>();
        databaseAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DataBase>();
        /*botonAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<Boton>();
        upSprite.SetActive(true);
        downSprite.SetActive(false);*/
    }
    /*void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == LoginBtnControler.buttonStates.up)
        {
            nextStateTime = Time.time + downTime;
            upSprite.SetActive(false);
            downSprite.SetActive(true);
            currentState = LoginBtnControler.buttonStates.down;
        }
    }
    void Update()
    {
        if (nextStateTime > 0.0f)
        {
            if (nextStateTime < Time.time)
            {
                // Retornar el botó a estat “no polsat”
                nextStateTime = 0.0f;
                upSprite.SetActive(true);
                downSprite.SetActive(false);
                currentState = LoginBtnControler.buttonStates.up;
                // Començar el joc
                //stateManager.loginGame();
                botonAcces.ButtonShowMenu();
            }
        }
    }*/
    public void registerScreenMethod()
    {
        stateManager.RegisterGame();
    }

    public void LoginScreenMethod()
    {
        counter++;
        if (counter % 2 == 0)
        {
            //v_boton.image.overrideSprite = Image1;
            stateManager.TitleGame();
        }
        else
        {
            //v_boton.image.overrideSprite = Image2;
            stateManager.loginGame();
        }
    }
    public void RankingScreenMethod()
    {
        databaseAcces.PonerListaRanking();
        stateManager.RankingGame();
    }
}