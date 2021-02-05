using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L1S1 : L1
{
    [SerializeField] private List<string> FirstDialog = null;
    [SerializeField] private List<Sprite> FirstSpriteDialog = null;
   
    private bool CanWrite=true;
    protected override void Awake()
    {
        base.Awake();
        gameState = GameState.isInGame;
    }

    protected override void Start()
    {
        base.Start();
        
        MakeFirstDialog(0, FirstDialog.Count);
    }

    private void MakeFirstDialog(int i, int maxLenght)
    {
        if (i < maxLenght-1)
        {
           
                DialogManager.instance.SetTextDialog(FirstDialog[i], FirstSpriteDialog[i],
               () =>
               {
                   MakeFirstDialog(i + 1, maxLenght);
               });
                
            
           
        }
        else
        {
            DialogManager.instance.SetTextDialog(FirstDialog[i], FirstSpriteDialog[i], null,true);
        }
    }
  
}
