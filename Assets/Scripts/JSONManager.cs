using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;

public class JSONManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public int time;
        public int score;
    }
    [System.Serializable]
    public class WriteData
    {
        public PlayerData[] rank;
    }


    public static List<List<PlayerData>> _rankData = new List<List<PlayerData>>();

    string[] fileName = { "/desert.json", "/mountain.json", "/city.json" };
    string filePath;


    //private void Awake()
    //{
    //    PlayerData eekta = new PlayerData();

    //    eekta.time = 5;
    //    eekta.score = 5;
    //    _rankData.Add(new List<PlayerData>() { eekta });
    //    SaveRank();
    //}

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

    //void SaveRank()
    //{
    //    filePath = Application.persistentDataPath;
    //
    //    for (int i = 0; i < 3; i++)
    //    {
    //       _tmpStr = "{ \"rank\": [";
    //        for (int j = 0; j < 5; j++)
    //        {
    //            _tmpStr += JsonUtility.ToJson(_rankData[i][j]) + ((j < 4) ? "," : "]");
    //        }
    //        _tmpStr += "}";
    //        File.Delete(filePath + fileName[i]);
    //        File.WriteAllText(filePath + fileName[i], _tmpStr);
    //    }
    //}

    void SaveRank()
    {
        filePath = Application.persistentDataPath;
        for (int i = 0; i < 3; i++)
        {
            WriteData _tmpDB = new WriteData();
            _tmpDB.rank = new PlayerData[5];

            for(int j = 0; j < 5; j++)
            {
                _tmpDB.rank[j] = _rankData[i][j];
            }
            File.WriteAllText(filePath + fileName[i], JsonUtility.ToJson(_tmpDB));
        }
    }


    void SortList()
    {
        for(int i = 0; i < 3; i++)
        {
            //_rankData[i] = _rankData[i].OrderBy(o => o.score).ToList();
            _rankData[i] = _rankData[i].OrderByDescending(o => o.score).ToList();
        }
    }
}
