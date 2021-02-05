using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Features")]
    [SerializeField] private int idItem;
    [SerializeField] private string nameItem;
    [SerializeField] private Sprite imageItem;
    [SerializeField] private Sprite _findItem;
    [SerializeField] private bool _justDialog;
  
    [SerializeField] private Sprite spriToChange;

    [SerializeField] private int _conditionalToGive=-1;

    public List<string> Dialogo;
    private int count = 0;

    public int IdItem { get => idItem; set => idItem = value; }
    public string NameItem { get => nameItem; set => nameItem = value; }
    public Sprite ImageItem { get => imageItem; set => imageItem = value; }
    public bool JustDialog { get => _justDialog; set => _justDialog = value; }
   
    public Sprite SpriToChange { get => spriToChange; set => spriToChange = value; }
    public Sprite FindItem { get => _findItem; set => _findItem = value; }
    public int ConditionalToGive { get => _conditionalToGive; set => _conditionalToGive = value; }

    private bool PuedoDesactivarBoton;
  
    private void Update()
    {

        float dist = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if (dist <= 0.3f)
        {
            PuedoDesactivarBoton = true;
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = SpriToChange;
            if (JustDialog)
            {
                DialogManager.instance._Butons[0].SetActive(true);
            }
            else
            {
                for (int i = 0; i < DialogManager.instance._Butons.Count; i++)
                {
                    DialogManager.instance._Butons[i].SetActive(true);
                }
            }


            if (Input.GetKeyDown(KeyCode.A) && count == 0 && JustDialog || Input.GetKeyDown(KeyCode.A) && count == 0 && !JustDialog)
            {
                count += 1;
                PlayerController.instance.IsAvailableToMove = false;
                if (ConditionalToGive != -1)
                {
                    if (!ItemsManager.instance.UseItem(ConditionalToGive))
                    {
                        DialogManager.instance.SetTextDialog(Dialogo[0], PlayerController.instance.PlayerFace);
                    }
                    else
                    {
                        DialogManager.instance.SetTextDialog(Dialogo[1], PlayerController.instance.PlayerFace);
                    }
                }
                else
                {
                    DialogManager.instance.SetTextDialog(Dialogo[0], PlayerController.instance.PlayerFace,()=>
                    {
                        DialogManager.instance.SetTextDialog(Dialogo[1], PlayerController.instance.MorriganFace);
                    }
                    );
                }
               
            }
            if (DialogManager.instance.Termino1)
            {
                count = 0;
            }

            if (Input.GetKeyDown(KeyCode.S) && !JustDialog)
            {
                ItemsManager.instance.AddItem(this);
                this.gameObject.SetActive(false);
                for (int i = 0; i < DialogManager.instance._Butons.Count; i++)
                {
                    DialogManager.instance._Butons[i].SetActive(false);
                }
                ItemsManager.instance.IsGetThreeRightItems();
            }
        }
        else if (PuedoDesactivarBoton)
        {
            PuedoDesactivarBoton = false;
            StartCoroutine(DesactiveButtons());
        }
    }
    IEnumerator DesactiveButtons()
    {
        yield return new WaitForSeconds(0.5f);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = imageItem;
        for (int i = 0; i < DialogManager.instance._Butons.Count; i++)
        {
            DialogManager.instance._Butons[i].SetActive(false);
        }
    }


}
