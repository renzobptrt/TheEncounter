using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morrigan : MonoBehaviour
{
    [SerializeField] private List<string> FirstDialog = null;
    [SerializeField] private List<Sprite> FirstSpriteDialog = null;

    private int count ;
    bool PuedeCambiaTexto;
  

   

    private void MakeFirstDialog(int i)
    {
       

            DialogManager.instance.SetTextDialog(FirstDialog[i], FirstSpriteDialog[0]
           );



        

    }
    private void Update()
    {
        NextDialogue();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") )
        {

            PuedeCambiaTexto = true;
        }
    }
    public void NextDialogue()
    {
       if(Input.GetKeyDown(KeyCode.A)&&PuedeCambiaTexto)
        {
            if (count < FirstDialog.Count)
            {
                MakeFirstDialog(count);
                count += 1;
                PuedeCambiaTexto = false;
            }
        }
      
    }
}
