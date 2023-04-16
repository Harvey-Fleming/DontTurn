using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Beginning.CSharp;
using Jdeakin.CSharp;


public class Hello : MonoBehaviour
{
    Player playerOne;
    Alien alien;
    private void Start()
    {
        playerOne = new Player(3, "Barney", 100);

        alien = new Alien();
        alien.IsAlive= true;
        alien.HitPoints = 1;
        alien.PointValue = 100;
    }
    private void OnDisable()
    {
      Debug.Log ("Name: " + playerOne.Name + ", Score: " + playerOne.Score + ", Lives:" + playerOne.Lives);

        Debug.Log("Is Alive: " + alien.IsAlive + ", Hit Points: " + alien.HitPoints + ", PointValue: " + alien.PointValue);
    }
}
