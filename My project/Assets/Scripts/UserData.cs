using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public string ID;
    public string Password;
    public string Name = "��ٻ�";
    public int Balance = 50000;
    public int Cash = 100000;

    public UserData(string id, string passward, string name, int balance, int cash)
    {
        ID = id;
        Password = passward;
        Name = name;
        Balance = balance;
        Cash = cash;
    }
}