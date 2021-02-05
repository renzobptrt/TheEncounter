using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Callbacks;

public enum GameState
{
    isMenu,
    isInGame,
    isGameOver
}

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;
    [SerializeField] protected GameState gameState;

    [Header("Scenes")]
    [SerializeField] private string _menuScene = string.Empty;
    public string MenuScene { get => _menuScene; set => _menuScene = value; }
    [SerializeField] private string _nextScene = string.Empty;
    public string NextScene { get => _nextScene; set => _nextScene = value; }

    [Header("Audio")]
    private string _backgroundMusic = string.Empty;
    public string BackgroundMusic { get => _backgroundMusic; set => _backgroundMusic = value; }
    public string[] _sfxMusic = null;
    public string[] SfxMusic { get => _sfxMusic; set => _sfxMusic = value; }

    [SerializeField] private Animator _transitionAnim;

    private string ScriptName = string.Empty;

    [Header("Position Player")]
    [SerializeField] private Transform startPositionPlayer = null;
    public Transform StartPositionPlayer { get => startPositionPlayer; set => startPositionPlayer = value; }

    protected virtual void Awake()
    {
        ScriptName = this.GetType().Name;
    }

    protected virtual void Start()
    {
        PlayBackground();
        if (gameState == GameState.isInGame)
        {
            PlayerController.instance.transform.position = StartPositionPlayer.position;
            PlayerController.instance.ResetMovement();   
        }
        _transitionAnim = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }

    public void GoToMenuScene()
    {
        StartCoroutine(TransitionScene(()=> {
            gameState = GameState.isMenu;
            SceneManager.LoadScene(MenuScene);
        }));
    }

    public void GoToNextScene()
    {
        StartCoroutine(TransitionScene(()=> {
            SceneManager.LoadScene(NextScene);
        }));
    }

    public void PlayBackground()
    {
        string FolderBackgroundName = GetName("background");
        Debug.Log(FolderBackgroundName);
        SoundManager.instance.PlayBackground(FolderBackgroundName);
    }

    public void PlaySfx(string _nameSfx)
    {
        string FolderSfxName = GetName(_nameSfx);
        SoundManager.instance.PlaySfx(FolderSfxName);
    }

    protected void GoToExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }

    string GetName(string name)
    {
        string NameToGet = string.Empty;
        NameToGet = "sounds/";
        for (int i = 2; i <= ScriptName.Length; i += 2)
            NameToGet += ScriptName.Substring(0, i) + "/";
        NameToGet += name;
        return NameToGet;
    }

    public void GoToNextPosition(OnComplete onComplete)
    {
        StartCoroutine(TransitionScene(() => onComplete()));
    }

    IEnumerator TransitionScene(OnComplete onComplete)
    {
        _transitionAnim.SetTrigger("IsOut");
        yield return new WaitForSeconds(0.35f);
        onComplete();
    }

}
