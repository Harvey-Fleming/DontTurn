using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion 
{
    public string Name;
    
        public Minion(string name)
    {
        this.Name = name;
    }

    ~Minion()
    {
        Debug.Log(Name);
    }
    
}
