using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StageManager : MonoBehaviour
{//enemyspowner�� �ٸ�����
    private Transform[] wayPoints;

    public Tilemap tileMap;
    public GameObject pointTiles;
    public int startCoin;

    public float waveTIme;
    public List<Wave> waves;
    public Wave currentWave;

    public  List<Enemy> enemies=new List<Enemy>();//������ ����


    public event System.Action OnWaveFinish;
    void Start()
    {
        
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
        wayPoints = pointTiles.GetComponentsInChildren<Transform>();
    }
    public Transform[] GetWayPoints()
    {
        return wayPoints;
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


        ///
        if (OnWaveFinish != null)
        {
            OnWaveFinish();
        }


    }
    public void CreateEnemy(GameObject enemy)
    {
        //���� ����
        GameObject cat = Instantiate(enemy);
        Enemy _enemy = cat.GetComponent<Enemy>();
        _enemy.SetUp(wayPoints);

        //����Ʈ�� �߰�
        enemies.Add(_enemy);

        //����Ʈ���� ����
        _enemy.OnDeath += () => enemies.Remove(_enemy);
        //���� �� ��,��ȭ ���� (gm�޼ҵ�)
        _enemy.OnDeath += () => GameManager.Instance.UpdateEnemyDeath(_enemy);

        //
        //_enemy.OnSurvive += () =>


    }

}
