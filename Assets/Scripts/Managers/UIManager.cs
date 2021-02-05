using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Singleton
    public static UIManager instance;
    public Canvas myCanvas;
    [SerializeField] private Transform ItemsManager = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetCamera(Camera camera)
    {
        myCanvas.worldCamera = camera;
    }

    public void SetMinItemsManager()
    {
        ItemsManager.localScale = Vector2.zero;
    }
}
