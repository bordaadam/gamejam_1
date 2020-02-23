using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _wood;
    [SerializeField] private int _stone;
    [SerializeField] private int _javelin;
    [SerializeField] private int _runes;
    [SerializeField] private int _humanResources;
    [SerializeField] private int _maxEnemy;
    [SerializeField] private int _remainingEnemy;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private int _needToSpawn;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject infoPanel, button;
    [SerializeField] private AudioClip gameOver;
    private bool dead = false;
    [SerializeField] private AudioSource music;



    private bool levelFinished = false;

    #region Properties
    public int CurrentLevel
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public int NeedToSpawn
    {
        get { return _needToSpawn; }
        set { _needToSpawn = value; }
    }

    public int Wood
    {
        get { return _wood; }
        set { _wood = value; }
    }

    public int Stone
    {
        get { return _stone; }
        set { _stone = value; }
    }
    public int Javelin
    {
        get { return _javelin; }
        set { _javelin = value; }
    }

    public int Runes
    {
        get { return _runes; }
        set { _runes = value; }
    }

    public int HumanResources
    {
        get { return _humanResources; }
        set { _humanResources = value; }
    }

    public int MaxEnemy
    {
        get { return _maxEnemy; }
        set { _maxEnemy = value; }
    }

    public int RemainingEnemy
    {
        get { return _remainingEnemy; }
        set { _remainingEnemy = value; }
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    #endregion

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        RemainingEnemy = MaxEnemy;
        CurrentHealth = MaxHealth;
        NeedToSpawn = MaxEnemy;
        music.volume = 0.3f;
    }

    void Update()
    {
        // Számontartja h elfogytak-e az ellenségek 
        
        if(RemainingEnemy == 0)
        {
            LevelUp();
        }

        if(CurrentHealth < 1 && !dead)
        {
            GameOver();
        }
    }

    private void LevelUp()
    {
        CurrentLevel++;
        MaxEnemy *= 2; //TODO kiegyensúlyozottabb legyen
        RemainingEnemy = MaxEnemy;
        NeedToSpawn = MaxEnemy;

    }

    private void GameOver()
    {
        deathPanel.SetActive(true);
        infoPanel.SetActive(false);
        button.SetActive(false);
        dead = true;
        music.volume = 0;
        GetComponent<AudioSource>().PlayOneShot(gameOver);
        Destroy(GameObject.Find("MenuMaster"));
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
    }
}
