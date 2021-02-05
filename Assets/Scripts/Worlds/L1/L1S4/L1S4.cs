using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using static Callbacks;
public class L1S4 : L1
{
    [SerializeField] private List<string> FirstDialog = null;
    [SerializeField] private List<Sprite> FirstSpriteDialog = null;
    [SerializeField] private List<string> SecondDialog = null;
    [SerializeField] private List<Sprite> SecondSpriteDialog = null;
    [SerializeField] private List<string> ThirdDialog = null;
    [SerializeField] private List<Sprite> ThirdSpriteDialog = null;
    [SerializeField] private Transform Camera;
    [SerializeField] private TextMeshProUGUI Text;
    [SerializeField] private Transform Temp;
    [SerializeField] private Image Connor = null;
    
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.isMenu;
        UIManager.instance.SetCamera(GameObject.Find("Main Camera").GetComponent<Camera>());

        PlayerController.instance.transform.localScale = Vector2.zero;
        Connor.transform.localScale = Vector2.zero;
        Text.DOFade(0, 0.1f);
    }

    protected override void Start()
    {
        base.Start();
        //UIManager.instance.transform.gameObject.SetActive(false);
        UIManager.instance.SetMinItemsManager();

        MakeDialog(0, FirstDialog.Count,FirstDialog,FirstSpriteDialog,()=>
        {
            Temp.DOScale(0, 2f).OnComplete(() =>
            {
                MakeDialog(0, SecondDialog.Count, SecondDialog, SecondSpriteDialog,() =>
                 {
                     Temp.DOScale(0, 2f).OnComplete(() =>
                     {
                         MakeDialog(0, ThirdDialog.Count, ThirdDialog, ThirdSpriteDialog,()=>
                         {
                             Temp.DOScale(0, 2f).OnComplete(() =>
                             {
                                 GameOver();
                             });
                         });
                     });
                 });
            });
        });
    }

    private void MakeDialog(int i, int maxLenght,List<string> _newDialog, List<Sprite> _newSprite, OnComplete onComplete=null)
    {
        if (i < maxLenght - 1)
        {
            DialogManager.instance.SetTextDialog(_newDialog[i], _newSprite[i],
                () =>
                {
                    MakeDialog(i + 1, maxLenght, _newDialog, _newSprite,onComplete);
                });
        }
        else
        {
            DialogManager.instance.SetTextDialog(_newDialog[i], _newSprite[i], onComplete, true);
        }
    }

    void GameOver()
    {
        Connor.transform.localScale = Vector2.one;
            GoToNextPosition(() =>
            {
                Camera.transform.position = new Vector2(20f, 0f);
                Text.DOFade(1, 4f);
            });
    }
}
