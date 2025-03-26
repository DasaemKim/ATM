using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public UserData userData;

    public Text nameText;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI cashText;
    public GameObject LoginUI;
    public GameObject ATMUI;

    public string FileSave;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FileSave = Path.Combine(Application.persistentDataPath, "GameData.json");

        LoginUI.SetActive(true);
        ATMUI.SetActive(false);
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(userData);

        File.WriteAllText(FileSave, json);
    }

    public void LoadData()
    {
        if (File.Exists(FileSave))
        {
            string json = File.ReadAllText(FileSave);

            UserData loadedData = JsonUtility.FromJson<UserData>(json);
        }

        else
        {
            userData = new UserData("ABC123", "qwe123", "±è´Ù»ù", 50000, 100000);
            SaveData();
        }
    }

    public bool Login(string id, string password)
    {
        LoadData();

        if (userData.ID == id && userData.Password == password)
        {
            LoginUI.SetActive(false);
            ATMUI.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }
}