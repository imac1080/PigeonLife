using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public static List<Transform> waypoints = new List<Transform>();
    public Transform[] waypoints2;
    public GameObject car;
    public static List<GameObject> carDelete = new List<GameObject>();
    public static bool muerto = false;
    int m_CurrentWaypointIndex;
    int ChanceOfDrop;
    public delegate void enemyEventHandler(int scoreMod);
    public static event enemyEventHandler enemyDied;


    void Start()
    {
        if (waypoints.Count == 0)
        {
            ChanceOfDrop = Random.Range(1, 3);
            if (ChanceOfDrop == 1)
            {
                waypoints.Add(waypoints2[ChanceOfDrop - 1]);
                waypoints.Add(waypoints2[ChanceOfDrop + 1]);
                //Debug.Log("!!!!!!!!!!!!!!!! INITIAL CAR " + ChanceOfDrop + " lenght: " + waypoints.Count);
            }
            else if (ChanceOfDrop == 2)
            {
                waypoints.Add(waypoints2[ChanceOfDrop - 1]);
                waypoints.Add(waypoints2[ChanceOfDrop + 1]);
                //Debug.Log("!!!!!!!!!!!!!!!! INITIAL CAR" + ChanceOfDrop + " lenght: " + waypoints.Count);
            }
        }
        navMeshAgent.SetDestination(waypoints[0].position);
    }
    void Update()
    {

        if (muerto || GameStates.SwitchingLvl)
        {
            navMeshAgent.speed = 0.03f;
            navMeshAgent.angularSpeed = 0.03f;
            navMeshAgent.acceleration = 0.03f;
            //if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            //{
                Destroy(car);
            //}
        }
        else
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                ChanceOfDrop = Random.Range(1, 3);
                if (ChanceOfDrop == 1)
                {
                    waypoints[0] = waypoints2[ChanceOfDrop - 1];
                    waypoints[1] = waypoints2[ChanceOfDrop + 1];
                    //Debug.Log("!!!!!!!!!!!!!!!! " + ChanceOfDrop + " lenght: " + waypoints.Count);
                }
                else if (ChanceOfDrop == 2)
                {
                    waypoints[0] = waypoints2[ChanceOfDrop - 1];
                    waypoints[1] = waypoints2[ChanceOfDrop + 1];
                    //Debug.Log("!!!!!!!!!!!!!!!! " + ChanceOfDrop + " lenght: " + waypoints.Count);
                }
                if (!GameStates.SwitchingLvl)
                {
                    navMeshAgent.speed = GameStates.v_speed;
                    navMeshAgent.acceleration = GameStates.v_acceleration;
                    GameStates.v_speed = GameStates.v_speed + 100;
                    GameStates.v_acceleration = GameStates.v_acceleration + 1;
                        GameObject newEnemy = Instantiate(car) as GameObject;
                        newEnemy.transform.position = waypoints[1].position;
                        carDelete.Add(newEnemy);
                }
               

                Destroy(car);
                if (enemyDied != null)
                    enemyDied(1);
            }
        }
         
    }
}