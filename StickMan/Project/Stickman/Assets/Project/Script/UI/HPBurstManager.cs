using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

 
public class HPBurstManager : MonoBehaviour
{

    public  static HPBurstManager Instance = null;

    public int tt = 1234;
    public List<Sprite> PlayerHPDigits;
    public List<Sprite> EnemyHPDigits;
    public GameObject HpBurstBoard;

    private List<GameObject> Boards = new List<GameObject>() { };


    public void ShowHPBurst(Vector3 pos, int num, bool isCrit = false)
    {
        if (num <= 0)
            return;

        GameObject board = GetBoard();
        HPBurstBoard burstBoard = board.GetComponent<HPBurstBoard>();
        burstBoard.ClearBits();

        int[] digit = new int[4];

        int b = 1000;
        int noneZeroMaxBit = 3; //非零的最高位，默认是千位(3)
        bool findNoneZeroMaxBit = false;

        for (int i = 3; i >= 0; i--)
        {
            digit[i] = num % (b * 10) / b;
            b = b / 10;

            if(digit[i] == 0)
            {
                if(!findNoneZeroMaxBit)
                    noneZeroMaxBit--;                  
            }
            else
            {
                findNoneZeroMaxBit = true;
            }                
        }


        if (isCrit)
        {
            for(int k = noneZeroMaxBit; k >= 0; k--)
                burstBoard.SetBit(k, EnemyHPDigits[digit[k]]);
        }
        else
        {
            for (int k = noneZeroMaxBit; k >= 0; k--)
                burstBoard.SetBit(k, PlayerHPDigits[digit[k]]);
        }
           
        board.transform.position = pos;
        board.SetActive(true);
    }


    private GameObject GetBoard()
    {
        for (int i = 0; i < Boards.Count; i++)
        {
            if (Boards[i] == null)
            {
                Debugger.LogError("HP Burst Board is Null!");

                GameObject boardObj = Instantiate(HpBurstBoard) as GameObject;
                boardObj.transform.SetParent(this.transform);
                boardObj.SetActive(false);
                Boards[i] = boardObj;
            }

            if (!Boards[i].activeSelf)
                return Boards[i];
        }

        GameObject obj = Instantiate(HpBurstBoard) as GameObject;
        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
        Boards.Add(obj);
        return obj;
    }

    private Sprite GetSprite(int i, bool isCrit = false)
    {
        if (isCrit)
        {
            return EnemyHPDigits[i];
        }
        else
        {
            return PlayerHPDigits[i];
        }
    }

 

    private void InitBoards(int cloneCount)
    {
        for (int i = 0; i < cloneCount; i++)
        {
            GameObject obj = Instantiate(HpBurstBoard) as GameObject;
            obj.transform.SetParent(this.transform);
            obj.SetActive(false);
            Boards.Add(obj);
        }
    }

 
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {        
        InitBoards(5);
        // DontDestroyOnLoad(this.gameObject);
    }

 
    void Update()
    {

        if(Input.GetKeyUp(KeyCode.U))
        {
            tt++;
            ShowHPBurst(Vector3.zero, tt, true);
        }
    }
}

