using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    public int ranking;
    public String username;

    public Player(int ranking, String username)
    {
        this.ranking = ranking;
        this.username = username;
    }
}