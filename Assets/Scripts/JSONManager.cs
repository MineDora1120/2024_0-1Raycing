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
        // ������ ������ ����
        PlayerData player = new PlayerData();
        player.playerName = "Player1";
        player.playerLevel = 10;
        player.playerHealth = 100f;

        // �����͸� JSON ���ڿ��� ����ȭ
        string json = JsonUtility.ToJson(player);

        // ���� ��� ���� (Application.persistentDataPath�� ����ϸ� �÷����� ���� ���� ��ġ�� �ٸ��� �����˴ϴ�.)
        string filePath = Application.persistentDataPath + "/playerData.json";

        // JSON ���Ϸ� ����
        File.WriteAllText(filePath, json);

        Debug.Log("Player data saved to: " + filePath);
    }
}
