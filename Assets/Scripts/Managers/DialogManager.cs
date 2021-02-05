using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Callbacks;

public class DialogManager : MonoBehaviour
{
    //Singleton
    public static DialogManager instance;

    [SerializeField] private TextMeshProUGUI dialogText = null;
    [SerializeField] private Image myImage = null;
    [SerializeField] private string Dialogo;

    [SerializeField] private List<GameObject> Butons;
    [SerializeField] private GameObject TextoDialog;

    [SerializeField]private bool Termino;
    [SerializeField] private List<AudioClip> EfectVoice;
    [SerializeField] private AudioSource audioM;
    private float WaitLetter = 0.05f;
    [SerializeField]private Button nextTeext;
    private OnComplete _onComplete;
    
   

    public List<GameObject> _Butons{
       get{return Butons;}set{Butons=value;}
   }

    public bool Termino1 { get => Termino; set => Termino = value; }

    private void Awake()
    {
        if (instance == null)
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
        TextoDialog.SetActive(false);

        for (int i = 0; i < Butons.Count; i++)
        {
           Butons[i].SetActive(false); 
        }
        nextTeext.onClick.AddListener(PasarBoton);
         
       
    }
    private void Update()
    {
        
    }
    public void SetTextDialog(string _newDialog,Sprite _newImageDialog,OnComplete onComplete=null,bool isNotMove = false)
    {

        Termino = false;
        TextoDialog.SetActive(true);
        dialogText.text = "";
        Dialogo = _newDialog;
        myImage.sprite = _newImageDialog;

        StartCoroutine(WaitToOtherLetter(()=> {
            if (onComplete == null)
            {
                if (isNotMove)
                    StartCoroutine(TimeToRead(true));
                else
                    StartCoroutine(TimeToRead());
            }
            else
            {
                TextoDialog.SetActive(false);
                //PlayerController.instance.IsAvailableToMove = true;
                onComplete();
            }
        }));
    }

    IEnumerator WaitToOtherLetter(OnComplete onComplete)
    {
        for (int i = 0; i < Dialogo.Length; i++)
        {
            char LetraPorLetra = Dialogo[i];
            int R = Random.Range(0, EfectVoice.Count);
            dialogText.text += LetraPorLetra;

         
          
            if (audioM.isPlaying == false)
            {
                audioM.PlayOneShot(EfectVoice[R]);
            }
            yield return new WaitForSeconds(WaitLetter);


        }
      
        
        if (dialogText.text == Dialogo)
        {
            _onComplete = onComplete;
        }
        
    }

    IEnumerator TimeToRead(bool isFalse = false)
    {
        yield return new WaitForSeconds(1);

        Termino = true;
        TextoDialog.SetActive(false);
        if (isFalse)
        {
            FindObjectOfType<GameManager>().GoToNextPosition(() =>
            {
                PlayerController.instance.IsAvailableToMove = true;
            });
        }
        else
        {
            PlayerController.instance.IsAvailableToMove = true;
        }
    }
    public void PasarBoton()
    {
           if(_onComplete!=null) _onComplete();
    }
  
}
