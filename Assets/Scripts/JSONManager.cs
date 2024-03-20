using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;


[System.Serializable]
public class PlayerData
{
    public float time;
    public int score;
}
public class WriteData
{
    public PlayerData[] rank;
}

public class JSONManager : MonoBehaviour
{


    public static List<List<PlayerData>> _rankData = new List<List<PlayerData>>();

    private string[] fileName = { "/desert.json", "/mountain.json", "/city.json" };
    string filePath;


    //private void Awake()
    //{
    //    PlayerData eekta = new PlayerData();

    //    eekta.time = 5;
    //    eekta.score = 5;
    //    _rankData.Add(new List<PlayerData>() { eekta });
    //    SaveRank();
    //}
    void Start()
    {
        //DataSetting();

        PlayerData _testPlayerData = new PlayerData();

        _testPlayerData.time = 0.5f;
        _testPlayerData.score = 5;

        for (int i = 0; i < 3; i++)
        {
            _rankData.Add(new List<PlayerData>());
            for (int j = 0; j < 5; j++)
            {
                _rankData[i].Add(_testPlayerData);
            }
        }

        DataSetting();
    }

    private void Update()
    {
        
    }

    void DataSetting()
    {
        filePath = Application.persistentDataPath;
        for (int i = 0; i < 3; i++)
        {
            if(File.Exists(filePath + fileName[i]))
            {
                string json = File.ReadAllText(filePath+ fileName[i]);

                WriteData rankDataList = JsonUtility.FromJson<WriteData>(json);
                
                for(int j = 0; j < 5; j++)
                {
                    _rankData[i][j] = rankDataList.rank[j];
                }
            } else
            {
                SaveRank();
                break;
            }
        }
    }

    void SaveRank()
    {
        filePath = Application.persistentDataPath;
        string _tmpStr = "[";
        for (int i = 0; i < 3; i++)
        {
            _tmpStr = "{ \"rank\": [";
            for (int j = 0; j < 5; j++)
            {
                _tmpStr += JsonUtility.ToJson(_rankData[i][j]) + ((j < 4) ? "," : "]");
            }
            _tmpStr += "}";
            File.Delete(filePath + fileName[i]);
            File.WriteAllText(filePath + fileName[i], _tmpStr);
        }
    }
}
