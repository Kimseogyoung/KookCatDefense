using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    private Transform[] wayPoints;
    public Tilemap tileMap;
    public GameObject pointTiles;

    public GameObject[] enemy;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = pointTiles.GetComponentsInChildren<Transform>();
    }
    
    public Transform[] GetWayPoints()
    {
        return wayPoints;
    }
    public void CreateEnemy()
    {
        //���� ����
        GameObject cat = Instantiate(enemy[0]);
        cat.GetComponent<Enemy>().SetUp(wayPoints);
        
    }

}
