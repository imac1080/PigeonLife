using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButtonControler : MonoBehaviour
{
    //public GameObject upSprite;
    //public GameObject downSprite;
    public Sprite Image1;
    public Sprite Image2;
    public Button v_boton;
    public Sprite Image1Options;
    public Button v_boton_options;
    //public float downTime = 0.1f;
    private int counter = 0;
    private int counter2 = 0;
    /*private enum buttonStates
    {
        up = 0,
        down
    }*/
    //private buttonStates currentState = buttonStates.up;
    //private float nextStateTime = 0.0f;
    void Start()
    {
        v_boton = GetComponent<Button>();
    }

    public void changeButton()
    {
        counter++;
        if (counter %2 == 0)
        {
            v_boton.image.overrideSprite = Image1;
            AudioListener.volume = 1;
        }
        else
        {
            v_boton.image.overrideSprite = Image2;
            AudioListener.volume = 0;
        }
    }

    public void changeButtonOptions()
    {
        counter2++;
        if (counter2 % 2 == 0)
        {
            v_boton.image.overrideSprite = Image1;
            AudioListener.volume = 1;
        }
        else
        {
            v_boton.image.overrideSprite = Image2;
            AudioListener.volume = 0;
        }
    }
}
