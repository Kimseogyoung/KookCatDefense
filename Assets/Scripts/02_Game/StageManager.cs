using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{//enemyspowner�� �ٸ�����
    private Transform[] wayPoints;
    private TowerTile[] towerTiles;

    public Tilemap tileMap;
    public GameObject pointTilesParent;
    public GameObject towerTilesParent;
    public Wall lastWall;
    public int startCoin;

    public float waveTIme;
    public List<Wave> waves;
    public Wave currentWave;

    public  List<Enemy> enemies=new List<Enemy>();//������ ����


    void Start()
    {
        
    }
    private void Update()
    {
        if (lastWall == null)
        {
            StopAllCoroutines();
            GameManager.Instance.GameOver();
        }        
    }

    public void StartWave(int idx)
    {
        currentWave = waves[idx];
        for(int i=0; i<currentWave.enemyPrefabs.Length; i++)
        {
            currentWave.enemyCnt += currentWave.enemyPrefabs[i].amount;
        }
        StartCoroutine(WaitNextWave());
    }
    private IEnumerator WaitNextWave()
    {
        yield return new WaitForSeconds(waveTIme);
        StartCoroutine(SpawnEnemy());
    }
    public void LoadMap()
    {
        towerTiles = towerTilesParent.GetComponentsInChildren<TowerTile>();
        wayPoints = new Transform[pointTilesParent.transform.childCount];
        for (int i=0; i< pointTilesParent.transform.childCount; i++)
        {
            wayPoints[i] = pointTilesParent.transform.GetChild(i);
        }
    }
    public List<Transform> GetWayPoints()
    {
        return new List<Transform>(wayPoints);
    }
    private IEnumerator SpawnEnemy()
    {
        List<GameObject> enemyObjects = new List<GameObject>();
        for (int i = 0; i < currentWave.enemyPrefabs.Length; i++)
        {
            for (int j = 0; j < currentWave.enemyPrefabs[i].amount; j++)
                enemyObjects.Add(currentWave.enemyPrefabs[i].type);
        }

        int spawnEnemyCount = 0;
        while (spawnEnemyCount<currentWave.enemyCnt)
        {   
            //spawntime ��������
            //enemyCnt ��ŭ 
            //enemyPrefabs �߿� ���Ƿ� ��ȯ

            int enemyIndex = Random.Range(0, enemyObjects.Count);
            CreateEnemy(enemyObjects[enemyIndex]);
            enemyObjects.RemoveAt(enemyIndex);

            yield return new WaitForSeconds(currentWave.spawnTime);
            
            spawnEnemyCount++;
        }

    }
    private void CreateEnemy(GameObject enemy)
    {
        //���� ����
        GameObject cat = Instantiate(enemy,wayPoints[0].position, Quaternion.identity);
        Enemy _enemy = cat.GetComponent<Enemy>();
        _enemy.SetUp(new List<Transform>(wayPoints));

        //����Ʈ�� �߰�
        enemies.Add(_enemy);

        //����Ʈ���� ����
        _enemy.OnDeath += () => RemoveEnemy(_enemy);


        //
        //_enemy.OnSurvive += () =>


    }
    void RemoveEnemy(Enemy e)
    {   //���� �� ��,��ȭ ���� (gm�޼ҵ�)
        //����Ʈ���� ����
        enemies.Remove(e);
        GameManager.Instance.UpdateEnemyDeath(e);
    }

}
