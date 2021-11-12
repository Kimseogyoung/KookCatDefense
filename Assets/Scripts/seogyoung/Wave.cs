using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveEnemy
{
    public int amount;
    public GameObject type;

}

[System.Serializable]
public class Wave 
{
    public float spawnTime; //�� �����ֱ�
    public int enemyCnt=0;//�� �� ����
    public WaveEnemy[] enemyPrefabs;//�� ����



}
