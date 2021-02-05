using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1S2 : L1
{
    [SerializeField] private List<string> FirstDialog = null;
    [SerializeField] private List<Sprite> FirstSpriteDialog = null;
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.isInGame;
        UIManager.instance.SetCamera(GameObject.Find("Main Camera").GetComponent<Camera>());
    }

    protected override void Start()
    {
        base.Start();
        MakeFirstDialog(0, FirstDialog.Count);
        ItemsManager.instance.AddListenerButtonBox();
    }


    private void MakeFirstDialog(int i, int maxLenght)
    {
        if (i < maxLenght - 1)
        {
            DialogManager.instance.SetTextDialog(FirstDialog[i], FirstSpriteDialog[i],
                () =>
                {
                    MakeFirstDialog(i + 1, maxLenght);
                });
        }
        else
        {
            DialogManager.instance.SetTextDialog(FirstDialog[i], FirstSpriteDialog[i], null, true);
        }
    }
}
