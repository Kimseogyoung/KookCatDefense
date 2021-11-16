using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{   //���������� �� �ε�
    //���� �� 
    //�÷��̾� ���ӿ��� ���°���
    //ui ����
    public GameObject currentTowerObj;

    public StageManager[] maps;
    public int mapIdx = 0;
    private StageManager stage;
    public int coin;

    private bool isTowerSettingMode=false;

    [SerializeField]
    private int waveNum = -1;
    [SerializeField]
    private int enemyCnt;

    private bool isWaveFinish = false;//���̺갡 �ٳ�������
    private bool isGameOver = false;


    //���� Ÿ�� �����ʷ� �ű� �ڵ�
    int maxTowerCnt=4;
    [SerializeField]
    private GameObject[] towerPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        //Ÿ�������ʷ� �ű��
        towerPrefabs = new GameObject[maxTowerCnt];
        for(int i=0; i<maxTowerCnt; i++) { 
            towerPrefabs[i] = Resources.Load<GameObject>("Prefabs/Tower/Tower"+GameData.Instance.selectedTowers[i]);
        }
        //

        InitMap();
    }
    void InitMap()
    {
        stage = Instantiate(maps[mapIdx]);
        stage.LoadMap();
        coin = stage.startCoin;
        

    }
    public void ClearGame()
    {
        Debug.Log("GameClear");
    }
    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("GameOver");    
        }
    }
    public void NextWave()
    {
        if (++waveNum>=stage.waves.Count)
        {
            //��� ���̺갡 ����. ���� ���� �� ���ָ� Ŭ����
            isWaveFinish = true;
            return;
        }

        //���� ���̺�
        stage.StartWave(waveNum);
        enemyCnt += stage.currentWave.enemyCnt;

        Debug.Log("in GameManager.cs NextWave() - wave Num :" + waveNum+" enemyCnt :"+enemyCnt);
        //ui����
    }
    public void UpdateEnemyDeath(Enemy enemy)
    {
        coin += enemy.coin;
        enemyCnt--;
        if (isWaveFinish == true && enemyCnt<=0)
        {
            ClearGame();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //�׽�Ʈ�� �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextWave();
        }
    }
}
