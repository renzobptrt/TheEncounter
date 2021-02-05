using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MENU : ME
{
    [SerializeField] private Button ButtonPlay = null;
    [SerializeField] private Button ButtonExit = null;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        ButtonPlay.onClick.RemoveAllListeners();
        ButtonPlay.onClick.AddListener(() =>
        {
            GoToNextScene();
        });

        ButtonExit.onClick.RemoveAllListeners();
        ButtonExit.onClick.AddListener(() =>
        {
            GoToExitGame();
        });
    }


}
