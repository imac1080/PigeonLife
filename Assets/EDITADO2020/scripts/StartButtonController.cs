using UnityEngine;
using System.Collections;
public class StartButtonController : MonoBehaviour
{
    public GameObject upSprite;
    public GameObject downSprite;
    public float downTime = 0.1f;
    public GameStates stateManager = null;
    private Boton botonAcces;
    private enum buttonStates
    {
        up = 0,
        down
    }
    private buttonStates currentState = buttonStates.up;
    private float nextStateTime = 0.0f;
    void Start()
    {
        botonAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<Boton>();
        upSprite.SetActive(true);
        downSprite.SetActive(false);
        WaypointPatrol.muerto = false;
    }
    void OnMouseDown()
    {
        if (nextStateTime == 0.0f
         &&
         currentState == StartButtonController.buttonStates.up)
        {
            nextStateTime = Time.time + downTime;
            upSprite.SetActive(false);
            downSprite.SetActive(true);
            currentState = StartButtonController.buttonStates.down;
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
                currentState = StartButtonController.buttonStates.up;
                // Començar el joc
                stateManager.startGame();
                botonAcces.showMenu=false;
            }
        }
    }
}