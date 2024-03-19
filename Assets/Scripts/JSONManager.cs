using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class JSONManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public int playerLevel;
        public float playerHealth;
    }

    void Start()
    {
        // 저장할 데이터 생성
        PlayerData player = new PlayerData();
        player.playerName = "Player1";
        player.playerLevel = 10;
        player.playerHealth = 100f;

        // 데이터를 JSON 문자열로 직렬화
        string json = JsonUtility.ToJson(player);

        // 파일 경로 지정 (Application.persistentDataPath를 사용하면 플랫폼에 따라 저장 위치가 다르게 설정됩니다.)
        string filePath = Application.persistentDataPath + "/playerData.json";

        // JSON 파일로 저장
        File.WriteAllText(filePath, json);

        Debug.Log("Player data saved to: " + filePath);
    }
}
