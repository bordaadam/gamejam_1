using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _wood;
    [SerializeField] private int _stone;
    [SerializeField] private int _humanResources;
    [SerializeField] private int _maxEnemy;
    [SerializeField] private int _remainingEnemy;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private int _needToSpawn;

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
    }

    void Update()
    {
        // Számontartja h elfogytak-e az ellenségek 
        
        if(RemainingEnemy == 0)
        {
            LevelUp();
        }

        if(false)
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
        Debug.Log("You died.. lol");
        // TODO halál képernyő
    }
}
