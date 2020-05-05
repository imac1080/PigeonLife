using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int ranking;
    public String username;
    //public float[] position;

    public PlayerData (Player player)
    {
        ranking = player.ranking;
        username = player.username;

        /*position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;*/
    }
}


