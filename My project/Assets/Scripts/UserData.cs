using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string Name = "±è´Ù»ù";
    public int Balance = 50000;
    public int Cash = 100000;

    public UserData(string name, int balance, int cash)
    {
        Name = name;
        Balance = balance;
        Cash = cash;
    }
}


