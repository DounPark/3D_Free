using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        Vector3 tileSize = floorTilePrefab.GetComponent<Renderer>().bounds.size;
        float spacingX = tileSize.x;
        float spacingZ = tileSize.z;

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                Vector3 pos = new Vector3(x * spacingX, 0, z * spacingZ);
                GameObject tile = Instantiate(floorTilePrefab, pos, Quaternion.identity);
                tile.tag = "MapTile"; //  ClearMap에 걸리도록 태그 지정
                
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
    
    public Vector3 GetRandomSpawnPosition()
    {
        float mapSizeX = MapManager.Instance.mapSizeX * MapManager.Instance.tileSpacing;
        float mapSizeZ = MapManager.Instance.mapSizeZ * MapManager.Instance.tileSpacing;

        float x = Random.Range(0f, mapSizeX);
        float z = Random.Range(0f, mapSizeZ);

        return new Vector3(x, 1, z);
    }
}
