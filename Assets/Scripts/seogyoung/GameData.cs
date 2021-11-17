using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public int[] stageLocks;
    public int[] stageStars;



    //game scene���� �Ѿ�� ������ ������
    public int selectedStage=-1;
    public int[] selectedTowers;  //����ڰ� ������ Ÿ�� id��
    public int[] selectedSkills;


    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        
    }
}
