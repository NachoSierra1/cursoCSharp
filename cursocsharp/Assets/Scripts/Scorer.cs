using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int hits = 0;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Hit")
        {
            //++ es los mismo q decir = hits + 1
            hits ++;
            Debug.Log("You bumped into a wall this many times: " + hits);
            
        }
        
        
    }
    
}
