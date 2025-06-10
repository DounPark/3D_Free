using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    
    public GameObject floorTilePrefab;
    public GameObject obstaclePrefab;
    public int mapSizeX = 10;
    public int mapSizeZ = 10;
    public float tileSpacing = 1f;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateStageMap(int stage)
    {
        ClearMap();

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * tileSpacing, 0, z * tileSpacing);
                Instantiate(floorTilePrefab, pos, Quaternion.identity);
                
                // // 스테이지 높아질수록 장애물 확률 증가
                // float chance = Mathf.Clamp01(0.05f * stage);
                // if (Random.value < chance && !(x == mapSizeX / 2 && z == mapSizeZ / 2)) // 중앙은 비워둠
                // {
                //     Vector3 obstaclePos = pos + Vector3.up * 0.5f;
                //     Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity);
                // }
            }
        }
    }

    private void ClearMap()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MapTile"))
        {
            Destroy(obj);
        }
    }
}
