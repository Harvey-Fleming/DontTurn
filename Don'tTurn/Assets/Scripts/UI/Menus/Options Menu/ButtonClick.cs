using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour, IDataPersistence
{
    private int buttonClicked = 0;

    public void onButtonClick()
    {
        buttonClicked++;
    }

    public void LoadData(GameData data)
    {
        this.buttonClicked = data.buttonclicked;
    }

    public void SaveData(GameData data)
    {
        data.buttonclicked = this.buttonClicked;
    }
}
