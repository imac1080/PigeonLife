using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStates : MonoBehaviour
{
    public GameObject hudContainer;
    public GameObject titleContainer;
    public GameObject LoginContainer;
    public GameObject RegisterContainer;
    public GameObject RankingContainer;
    float startTime;
    private DataBase databaseAcces;

    public AudioSource m_AudioSource;
    public AudioSource[] voces;
    public TextMesh recordMesh ;
    public static bool resetLvl = false;
    public static bool gameActive = false;
    public static int lvl = 1;
    public static int cochesDelvl = 6;
    public GameObject paloma;
    public static int coches = 2;
    public static int v_speed = 100;
    public static int v_acceleration = 8;
    public static bool SwitchingLvl;
    public List<GameObject> allCars = new List<GameObject>();
    public static List<GameObject> allCars2 = new List<GameObject>();
    public List<Transform> waypoints = new List<Transform>();
    public static List<Transform> waypoints2 = new List<Transform>();
    public static List<GameObject> car = new List<GameObject>();
    public static GameObject[] car2;
    public static int ChanceOfDrop;
    public delegate void enemyEventHandler(int scoreMod);
    public static event enemyEventHandler enemyDied;
    bool camaraSube = false;
    public enum displayStates
    {
        titleScreen = 0,
        hudScreen,
        LoginScreen,
        RegisterScreen,
        RankingScreen
    }
    void Update()
    {
        if (cochesDelvl == ScoreWatcher.currScore)
        {
            SwitchingLvl = true;
            aceraAbajo.CologarParaCambiarLvl = true;
            ScoreWatcher.currScore = -1;
            //aceraAbajo.col.enabled = true;
        }
        
        if (resetLvl)
        {
            resetLvl = false;
            m_AudioSource.Stop();
            voces[Random.Range(0, 9)].Play();
            coches = 2;
            Debug.Log(lvl + "ssssssss"+ databaseAcces.recordSave);
            if(databaseAcces.recordSave < lvl)
            {
                databaseAcces.SaveDataToFileAndMongo(lvl);
                recordMesh.text = "Récord: lvl " + lvl;
            }
            lvl = 1;
            cochesDelvl = 6;
            v_speed = 100;
            v_acceleration = 8;
            SwitchingLvl = false;
            ScoreWatcher.updateScorre(0);
            Start();
        }
        if(!gameActive)
        {
            if(transform.position.y < 3.5 && camaraSube)
            {
                float t = (Time.time - startTime) * 1.0f;
                transform.position = transform.position + new Vector3(0, +0.3f * Time.deltaTime, 0);
                if (transform.position.y > 3.5)
                {
                    camaraSube = false;
                }
            }else if(transform.position.y > 2.5 && !camaraSube)
            {
                float t = (Time.time - startTime) * 1.0f;
                transform.position = transform.position + new Vector3(0, -0.3f * Time.deltaTime, 0);
                if (transform.position.y < 2.5)
                {
                    camaraSube = true;
                }
            }
        }
        else
        {
            if(transform.position.y != 1.62)
            {
                float t = (Time.time - startTime) * 1.0f;
                transform.position = transform.position + new Vector3(0, -0.3f * Time.deltaTime, 0);
                if (transform.position.y < 1.62)
                {
                    transform.position= new Vector3(transform.position.x, 1.62f, transform.position.z);
                }
            }
        }
    }
    public void Start()
    {
        ChangeDisplayState(displayStates.titleScreen);
        databaseAcces = GameObject.FindGameObjectWithTag("DatabaseAccess").GetComponent<DataBase>();
        allCars2 = allCars;
        waypoints2 = waypoints;
    }
    public void ChangeDisplayState(displayStates newState)
    {
        hudContainer.SetActive(false);
        LoginContainer.SetActive(false);
        RegisterContainer.SetActive(false);
        RankingContainer.SetActive(false);
        switch (newState)
        {
            case displayStates.titleScreen:
                gameActive = false;
                camaraSube = true;
                titleContainer.SetActive(true);
                break;
            case displayStates.hudScreen:
                gameActive = true;
                hudContainer.SetActive(true);
                titleContainer.SetActive(false);
                //generar coches
                StartCoroutine(ExampleCoroutine());
                break;
            case displayStates.LoginScreen:
                LoginContainer.SetActive(true);
                titleContainer.SetActive(true);
                break;
            case displayStates.RegisterScreen:
                RegisterContainer.SetActive(true);
                titleContainer.SetActive(true);
                break;
            case displayStates.RankingScreen:
                RankingContainer.SetActive(true);
                titleContainer.SetActive(true);
                break;

        }
    }
    public void startGame()
    {
        WaypointPatrol.muerto = false;
        GameObject newPaloma = Instantiate(paloma) as GameObject;
        ChangeDisplayState(displayStates.hudScreen);
        m_AudioSource.Play();
    }

    public void loginGame()
    {
        ChangeDisplayState(displayStates.LoginScreen);
    }

    public void RegisterGame()
    {
        ChangeDisplayState(displayStates.RegisterScreen);
    }

    public void RankingGame()
    {
        ChangeDisplayState(displayStates.RankingScreen);
    }

    public void TitleGame()
    {
        ChangeDisplayState(displayStates.titleScreen);
    }
    public static IEnumerator ExampleCoroutine()
    {
        //Debug.Log("coches_: "+coches);
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);
        for (int i = coches; i > 0; i--)
        {
            ChanceOfDrop = Random.Range(1, 3);
            if (ChanceOfDrop == 1)
            {
                car.Add(allCars2[ChanceOfDrop - 1]);
                GameObject newEnemy = Instantiate(car[0]) as GameObject;
                newEnemy.transform.position = waypoints2[0].position;
                car.Remove(car[0]);
                //Debug.Log("!!!!!!!!!!!!!!!!HUD1 " + ChanceOfDrop + " lenght: " + waypoints2.Count);
            }
            else if (ChanceOfDrop == 2)
            {
                car.Add(allCars2[ChanceOfDrop - 1]);
                GameObject newEnemy = Instantiate(car[0]) as GameObject;
                newEnemy.transform.position = waypoints2[1].position;
                car.Remove(car[0]);
                //Debug.Log("!!!!!!!!!!!!!!!!HUD2 " + ChanceOfDrop + " lenght: " + waypoints2.Count);
            }
            yield return new WaitForSeconds(1);
        }
        //yield on a new YieldInstruction that waits for 5 seconds.
      

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}