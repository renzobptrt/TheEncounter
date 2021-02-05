using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemsManager : MonoBehaviour
{
    //Singleton
    public static ItemsManager instance;

    [Header("Components")]
    [SerializeField] private HorizontalLayoutGroup BoxItems = null;
    [SerializeField] private Button ButtonBox = null;

    [Header("Features")]
    [SerializeField] private List<Item> CurrentItems = null;
    [SerializeField] private int LenghtChild = 0;
    [SerializeField] private int CurrentChild = 0;
    [SerializeField] private bool isExpand = false;
    [SerializeField] private Item Key = null;
    [SerializeField] private Sprite KeySprite;
   [SerializeField] private List<AudioClip> EfectBag;
    [SerializeField]private AudioSource audio;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        LenghtChild = BoxItems.transform.childCount;
        for(int i=0;i<BoxItems.transform.childCount;i++) BoxItems.transform.GetChild(i).GetComponent<Image>().enabled = false;
        BoxItems.transform.localScale = new Vector2(0f, 1f);
        AddListenerButtonBox();
    }

    public void AddListenerButtonBox()
    {
        ButtonBox.onClick.RemoveAllListeners();
        ButtonBox.onClick.AddListener(() => {
            if (isExpand)
            {
                BoxItems.transform.DOScaleX(0, 0.25f);
                audio.PlayOneShot(EfectBag[0]);
                isExpand = false;
            }
            else
            {
                BoxItems.transform.DOScaleX(1, 0.25f);
                audio.PlayOneShot(EfectBag[1]);
                isExpand = true;
            }
        });
    }

    public void AddItem(Item _newItem)
    {
        if (CurrentChild <= LenghtChild - 1)
        {
            Transform LastChild = BoxItems.transform.GetChild(CurrentChild);
            CurrentChild++;
            CurrentItems.Add(_newItem);
            LastChild.GetComponent<Image>().sprite = _newItem.FindItem;
            LastChild.GetComponent<Image>().enabled = true;
        }
        else
        {
            Debug.Log("Esta lleno la caja de items");
        }
    }

    public bool UseItem(int _idItem)
    {
        int positionItem = -1;
        for(int i=0; i< CurrentItems.Count; i++)
        {
            if(CurrentItems[i].IdItem == _idItem)
            {
                positionItem = i;
                break;
            }
        }
        if (positionItem != -1)
        {
            return true;
        }
        return false;
    }

    public void IsGetThreeRightItems()
    {
        if(UseItem(0) && UseItem(1) && UseItem(2))
        {
            if (CurrentItems.Count >= 4)
            {
                int positionItem = -1;
                for (int i = 0; i < CurrentItems.Count; i++)
                {
                    if (CurrentItems[i].IdItem == 3)
                    {
                        positionItem = i;
                    }
                }
                Item tempItem = CurrentItems[positionItem];
                RemoveAllChilds();
                AddItem(tempItem);
            }
            else
            {
                RemoveAllChilds();
            }
            AddItem(Key);
            CurrentChild = 1;
        }
    }

    public void RemoveAllChilds()
    {
        for (int i = 0; i < BoxItems.transform.childCount; i++)
            BoxItems.transform.GetChild(i).GetComponent<Image>().enabled = false;
        CurrentItems.Clear();
        CurrentChild = 0;
    }

    public void UseItemRemove(int _idItem)
    {
        int positionItem = -1;
        for (int i = 0; i < CurrentItems.Count; i++)
        {
            if (CurrentItems[i].IdItem == _idItem)
            {
                positionItem = i;
                break;
            }
        }
        Debug.Log("Este item tiene posicion:" + positionItem);
        if (positionItem != -1)
        {
            if (positionItem == CurrentItems.Count - 1)
            {
                BoxItems.transform.GetChild(positionItem).GetComponent<Image>().enabled = false;
            }
            else
            {
                for (int i = positionItem; i < LenghtChild; i++)
                {
                    if (positionItem != LenghtChild - 1)
                    {
                        BoxItems.transform.GetChild(i).GetComponent<Image>().sprite = CurrentItems[i + 1].ImageItem;
                    }
                    else
                    {
                        BoxItems.transform.GetChild(i).GetComponent<Image>().enabled = false;
                    }
                }
            }
            CurrentItems.RemoveAt(positionItem);
            CurrentChild--;
        }
    }
}
