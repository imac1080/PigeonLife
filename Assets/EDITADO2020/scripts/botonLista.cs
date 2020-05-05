using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botonLista : MonoBehaviour
{
    public Image iconImage;

    // Start is called before the first frame update
    void Start()
    {
        //iconImage = transform.Find("Image");
        //Destroy(iconImage);
    }
    public void Setup()
    {
        Destroy(iconImage);
    }
    
}
