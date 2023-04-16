using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pets : MonoBehaviour
{
    void OnDisable()
    {
        Cat cat = new Cat();
        Dog dog = new Dog();

        cat.Speak();
        dog.Speak();
    }

}
