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

    public int width;
    public int height;
    public bool isResolutionChanged = false;


    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ClearSelectedThings()
    {
        selectedStage = -1;
        for(int i=0; i<4; i++)
        {
            selectedTowers[i] = -1;
            selectedSkills[i] = -1;
        }
    }

    void Update()
    {
        
    }
}
