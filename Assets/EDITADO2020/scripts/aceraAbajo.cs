using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aceraAbajo : MonoBehaviour
{
    bool yourVar;
    public static bool CologarParaCambiarLvl = false;
    bool recolocarAcera1vez = false;
    bool recolocarAcera1vezCambioLvl = false;
    public Material material;
    public Collider col2;
    public GameObject OtraCarretera;
    public List<GameObject> objetosCambiarLvl = new List<GameObject>();
    public static List<Collider> cols = new List<Collider>();
    //public static Collider col;
    //public GameObject carretera;
    //public GameObject camera;
    //public GameObject paredes;

    Renderer rend;
    int frames=0;

        public float speed = 1.0f;
    public Color startColor;
    public Color endColor;
    public bool repeatable = false;
    float startTime;
    int segundos=0;
    int segundosBakcup=0;

    void Start()
    {
        //col = col2;
        cols.Add(col2);
        rend = GetComponent<Renderer>();
        material.color = Color.green;
        startTime = Time.time;

        // Use the Specular shader on the material
        //rend.material.shader = Shader.Find("NormalMap");
        // rend.material.EnableKeyword("_NORMALMAP");
        //rend.material.EnableKeyword("_METALLICGLOSSMAP");

        //Set the Texture you assign in the Inspector as the main texture (Or Albedo)
        // rend.material.SetTexture("_MainTex", m_MainTexture);
        //Set the Normal map using the Texture you assign in the Inspector
        // rend.material.SetTexture("_BumpMap", m_Normal);
        //Set the Metallic Texture as a Texture you assign in the Inspector
        // rend.material.SetTexture("_MetallicGlossMap", m_Metal);
    }

    void OnTriggerEnter(Collider Colider2)
    {
        if (Colider2.tag == "Player")
        {
            //Debug.Log("Col.lisio amb DeathTrigger. Tag: " + col.tag);
            //collidedObject.SendMessage("hitByPlayerBullet", null, SendMessageOptions.DontRequireReceiver);
            yourVar = true;
            frames = 3;
        }
       
    }
    void OnTriggerExit(Collider Colider2)
    {
        if (Colider2.tag == "Player")
        {
            //Debug.Log("Col.lisio amb DeathTrigger. Tag: " + col.tag);
            segundosBakcup = segundos;
            col2.enabled = true;
            recolocarAcera1vez = true;
            Debug.Log("Collider.enabled = " + col2.enabled);
            //Debug.Log("Collider.enabled = " + cols[0].enabled);
            //Debug.Log("Collider.enabled = " + cols[1].enabled);
            //collidedObject.SendMessage("hitByPlayerBullet", null, SendMessageOptions.DontRequireReceiver);
            yourVar = false;
        }
    }

    void Update()
    {
        if (CologarParaCambiarLvl)
        {
            //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            //OtraCarretera.transform.position = new Vector3(OtraCarretera.transform.position.x, 0, OtraCarretera.transform.position.z);
            segundos = 0;
            //float t = (Time.time - startTime) * speed;
            //GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
            recolocarAcera1vez = false;
            CologarParaCambiarLvl = false;
        }
        if (GameStates.SwitchingLvl && transform.position.y > -23)
        {
            //cols[0].enabled = true;
            //cols[1].enabled = true;
            col2.enabled = true;
            float t = (Time.time - startTime) * speed;
                //carretera.transform.position = carretera.transform.position + new Vector3(0, +1f * Time.deltaTime, 0);
                //camera.transform.position = camera.transform.position + new Vector3(0, +1f * Time.deltaTime, 0);
                //paredes.transform.position = paredes.transform.position + new Vector3(0, +1f * Time.deltaTime, 0);
                transform.position = transform.position + new Vector3(0, -10f * Time.deltaTime, 0);
        }else if(GameStates.SwitchingLvl && transform.position.y <= -23)
        {
           transform.position = new Vector3(transform.position.x, 17.3f, transform.position.z);
           OtraCarretera.transform.position = new Vector3(OtraCarretera.transform.position.x, 17.3f, OtraCarretera.transform.position.z);
            int i=0;
            for (i = 0; i < objetosCambiarLvl.Count; i++)
            {
                objetosCambiarLvl[i].transform.position = new Vector3(objetosCambiarLvl[i].transform.position.x, 19, objetosCambiarLvl[i].transform.position.z);
            }
            GameStates.SwitchingLvl = false;
            recolocarAcera1vezCambioLvl = true;
        }
        else if (!GameStates.SwitchingLvl && transform.position.y > 0 && !recolocarAcera1vez && recolocarAcera1vezCambioLvl)
        {
            float t = (Time.time - startTime) * speed;
            transform.position = transform.position + new Vector3(0, -10f * Time.deltaTime, 0);
            OtraCarretera.transform.position = OtraCarretera.transform.position + new Vector3(0, -10f * Time.deltaTime, 0);

            //Debug.Log("aaaaaaaaa ");

        }else if (recolocarAcera1vezCambioLvl)
        {
            recolocarAcera1vezCambioLvl = false;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            OtraCarretera.transform.position = new Vector3(OtraCarretera.transform.position.x, 0, OtraCarretera.transform.position.z);
            //Debug.Log("aaaaaaaaa 22222");
            GameStates.coches++;
            GameStates.lvl++;
            GameStates.cochesDelvl = GameStates.cochesDelvl + 2;
            StartCoroutine(GameStates.ExampleCoroutine());
            ScoreWatcher.updateScorre(0);
            int i = 0;
            for (i = 0; i < cols.Count; i++)
            {
                cols[i].enabled = false;
            }

                
        }
        /*if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - startTime) * speed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }*/

        if (yourVar)
        {
            segundos++;
            //transform.position = transform.position + new Vector3(0, -5f * Time.deltaTime, 0);
            //rend.material.SetTexture("_MainTex", m_MainTexture);
            //rend.material.SetColor("_SpecColor", Color.red);
            //material.color = new Color(233, 79, 55);
        } 
        else {
            //material.color = Color.black;
            
            if (segundos != 0)
            {
                float t = (Time.time - startTime) * speed;
                GetComponent<Renderer>().material.color = Color.Lerp(endColor, startColor, t);
                transform.position = transform.position + new Vector3(0, -5f * Time.deltaTime, 0);
                segundos--;
            }
            else
            {
                if (transform.position.y < 0 && !GameStates.SwitchingLvl && !recolocarAcera1vezCambioLvl)
                {
                    transform.position = transform.position + new Vector3(0, 5f * Time.deltaTime, 0);
                    
                }else if (transform.position.y > 0 && recolocarAcera1vez && !GameStates.SwitchingLvl)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                    float t = (Time.time - startTime) * speed;
                    GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
                    col2.enabled = false;
                    recolocarAcera1vez = false;
                    //cols[0].enabled = false;
                    //cols[1].enabled = false;
                    Debug.Log("Col disabled ");
                }
            }
        }

        // Animate the Shininess value
        //float shininess = Mathf.PingPong(Time.time, 1.0f);
        //rend.material.SetFloat("_Shininess", shininess);
      
    }
    
    
}
