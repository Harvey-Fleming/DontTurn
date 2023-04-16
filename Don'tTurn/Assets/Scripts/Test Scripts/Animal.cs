using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    protected string sound;

    public void Speak()
    {
        Debug.Log(sound);
    }
}

public class Cat : Animal
{
    public Cat()
    {
        sound = "Meow";
    }
}


public class Dog : Animal
{
    public Dog()
    {
        sound = "Woof";
    }
}