using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class L1S3 : L1
{
    [SerializeField] private List<Transform> Grounds = null;
    [SerializeField] private int LengthGround = 0;
    [SerializeField] private Transform Temp = null;
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.isInGame;
        UIManager.instance.SetCamera(GameObject.Find("Main Camera").GetComponent<Camera>());
    }

    protected override void Start()
    {
        base.Start();
        ItemsManager.instance.AddListenerButtonBox();
        LengthGround = Grounds.Count;
        Temp.DOScale(0, 5f).SetDelay(5f).OnComplete(() =>
        {
            TranslateGrounds(0, LengthGround);
        });
        PlayerController.instance.IsAvailableToMove = true;
    }

    void TranslateGrounds(int i,int lenght)
    {
        if (i < lenght-1 )
        {
            Grounds[i].DOMoveY(-7, 0.5f).SetDelay(0.2f).OnComplete(() =>
            {
                TranslateGrounds(i + 1, lenght);
            });
        }
        else
        {
            Grounds[i].transform.DOMoveY(-7, 0.5f);
        }
    }
}
