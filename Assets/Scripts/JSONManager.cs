using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;

public class JSONManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public float time;
        public int score;
    }
     
    public static List<List<PlayerData>> _rankData;

    private string[] fileName = { "/desert.json", "/mountain.json", "/city.json" };
    string filePath;

    void Start()
    {
        DataSetting();
    }

    void DataSetting()
    {
        filePath = Application.persistentDataPath;
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(filePath + fileName[i]))
            {
                string json = File.ReadAllText(filePath+ fileName[i]);
                _rankData[i] = JsonUtility.FromJson<List<PlayerData>>(json);
            } else
            {
                for(int j = 0; j < 5; j++)
                {
                    _rankData.Add(new List<PlayerData>());
                }

                File.WriteAllText(filePath + fileName[i], JsonUtility.ToJson(_rankData[i]));
            }
        }
    }
}
