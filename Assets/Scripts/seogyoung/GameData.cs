using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public int[] stageLocks;
    public int[] stageStars;

    public int[] selectedTowers;  //����ڰ� ������ Ÿ�� id��
    public int[] selectedSkills;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
