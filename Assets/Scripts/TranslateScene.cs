using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TranslateScene : MonoBehaviour
{
    [SerializeField] private Transform nextPosition;
    [SerializeField] private GameObject GameManagerScene;
    [SerializeField] private bool isAvailableToTranslate;
    [SerializeField] private Camera CameraScene;
    [SerializeField] private Transform nextPositionCamera;

    [SerializeField] private bool isOpen = false;
    [SerializeField] private List<int> conditionalToNextScene;
    [SerializeField] private List<string> dialogToNextScene;
    [SerializeField] private string _dialogToNotMeetItem = string.Empty;
    [SerializeField] private int CurrentDialog = 0;
    [SerializeField]private bool GoRight;

    [SerializeField] private bool isDoorInSecondScene = false;

    public Transform NextPosition { get => nextPosition; set => nextPosition = value; }
    public bool IsAvailableToTranslate { get => isAvailableToTranslate; set => isAvailableToTranslate = value; }

    public string DialogToNotMeetItem { get => _dialogToNotMeetItem; set => _dialogToNotMeetItem = value; }
    public Transform NextPositionCamera { get => nextPositionCamera; set => nextPositionCamera = value; }
    public List<int> ConditionalToNextScene { get => conditionalToNextScene; set => conditionalToNextScene = value; }
    public List<string> DialogToNextScene { get => dialogToNextScene; set => dialogToNextScene = value; }

    private void Start()
    {
        GameManagerScene = GameObject.FindGameObjectWithTag("GameController");
        CameraScene = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && isAvailableToTranslate)
        {
            PlayerController.instance.IsAvailableToMove = false;
            if (!isDoorInSecondScene)
            {
                if (!ItemsManager.instance.UseItem(ConditionalToNextScene[0]) &&
                    (!ItemsManager.instance.UseItem(ConditionalToNextScene[1]) || ItemsManager.instance.UseItem(ConditionalToNextScene[1])))
                {
                    DialogManager.instance.SetTextDialog(dialogToNextScene[0],
                        PlayerController.instance.PlayerFace);
                }
                else if (ItemsManager.instance.UseItem(ConditionalToNextScene[0]) && !ItemsManager.instance.UseItem(ConditionalToNextScene[1]))
                {
                    DialogManager.instance.SetTextDialog(dialogToNextScene[1],
                        PlayerController.instance.PlayerFace);
                }
                else if (ItemsManager.instance.UseItem(ConditionalToNextScene[0]) && ItemsManager.instance.UseItem(ConditionalToNextScene[1]))
                {
                    DialogManager.instance.SetTextDialog(dialogToNextScene[2],
                        PlayerController.instance.PlayerFace, () =>
                        {
                            DialogManager.instance.SetTextDialog(dialogToNextScene[3],
                                 PlayerController.instance.MorriganFace, () =>
                                 {
                                     ItemsManager.instance.RemoveAllChilds();
                                     GameManagerScene.GetComponent<GameManager>().GoToNextScene();

                                 });
                        }
                     );
                }
            }
            else
            {
                if(ItemsManager.instance.UseItem(ConditionalToNextScene[0])&&
                    ItemsManager.instance.UseItem(ConditionalToNextScene[1])&&
                    ItemsManager.instance.UseItem(ConditionalToNextScene[2])&&
                    ItemsManager.instance.UseItem(ConditionalToNextScene[3])){
                    DialogManager.instance.SetTextDialog(dialogToNextScene[0],
                        PlayerController.instance.PlayerFace, () =>
                        {
                            DialogManager.instance.SetTextDialog(dialogToNextScene[1],
                                 PlayerController.instance.MorriganFace, () =>
                                 {
                                     DialogManager.instance.SetTextDialog(dialogToNextScene[2],
                                        PlayerController.instance.PlayerFace, () =>
                                        {
                                            DialogManager.instance.SetTextDialog(dialogToNextScene[3],
                                                 PlayerController.instance.MorriganFace, () =>
                                                 {
                                                     ItemsManager.instance.RemoveAllChilds();
                                                     GameManagerScene.GetComponent<GameManager>().GoToNextScene();
                                                 }
                                            );
                                        }
                                     );
                                 }
                            );
                        }
                     );
                }
                else
                {
                    DialogManager.instance.SetTextDialog(DialogToNotMeetItem,
                        PlayerController.instance.PlayerFace);
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isOpen)
            {
                if (GoRight)
                {
                    PlayerController.instance.Inventario1.transform.position =
                                 new Vector2(PlayerController.instance.Inventario1.transform.position.x + 20,
                                   PlayerController.instance.Inventario1.transform.position.y);
                }
                else
                {
                    PlayerController.instance.Inventario1.transform.position =
                                 new Vector2(PlayerController.instance.Inventario1.transform.position.x -20 ,
                                   PlayerController.instance.Inventario1.transform.position.y);
                }
               
            
                GameManagerScene.GetComponent<GameManager>().GoToNextPosition(() =>
                {
                    PlayerController.instance.transform.position = nextPosition.position;
                    PlayerController.instance.ResetMovement();
                    CameraScene.transform.position = new Vector3(NextPositionCamera.position.x,
                                                                  NextPositionCamera.position.y,
                                                                  CameraScene.transform.position.z);
                });
            }
            else
            {
                isAvailableToTranslate = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!isOpen) isAvailableToTranslate = false;
        }
    }
}
