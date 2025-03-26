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

    private string FileSave;

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
            userData = new UserData("±è´Ù»ù", 50000, 100000);
            SaveData();
        }
    }
}