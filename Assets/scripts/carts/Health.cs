using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int PlayerID; 
    public bool IsAlive = true;
    private int maxHealth = 4; 
    private int maxShield = 2;
    [SerializeField] private int _currentHealth; 
    [SerializeField] private int _currentShield;
    private Animator _animator; 

    private PlayerMovement _playerController; 
    private MeshCollider _meshCollider; 
    private Rigidbody _rigidbody; 

    private Gamepad _playerGamepad; 
    private PlayerInput _playerInput; 
    
    public static Health instance;
    public GameRoundManager gameRoundManager;

    public Image healthbarFill;
    
    
    private void Start()
    {
        _playerController = GetComponent<PlayerMovement>(); 
        _meshCollider = GetComponentInChildren<MeshCollider>(); 
        _rigidbody = GetComponent<Rigidbody>(); 

        _currentHealth = maxHealth; 
        _currentShield = 0;

        AssignGamepad(); 


        if (gameRoundManager == null)
        {
            gameRoundManager = FindObjectOfType<GameRoundManager>(); 
        }

        if (healthbarFill == null)
        {
            healthbarFill = GetComponentInChildren<Image>();
        }
        
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthbarFill.fillAmount = (float)_currentHealth / maxHealth;
    }
    private void AssignGamepad()
    {
        _playerInput = GetComponent<PlayerInput>(); // gets playerinput so set a controller to it

        if (_playerInput != null && _playerInput.currentControlScheme == "Gamepad")
        {
            _playerGamepad = _playerInput.devices[0] as Gamepad;
            Debug.Log("Assigned Gamepad: " + _playerGamepad?.name);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (_currentShield > 0)
        {
            _currentShield -= damageAmount;
            _currentShield = Mathf.Clamp(_currentShield, 0, maxShield);
        }
        else
        {
            _currentHealth -= damageAmount; 
            _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        }
        
        if (_currentHealth <= 0) 
        {
            Die();
        }
        UpdateHealthBar();
    }

    public void RegainHealth(int healthAmount)
    {
        _currentHealth += healthAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    public void RegainShield(int shieldAmount)
    {
        _currentShield += shieldAmount;
        _currentShield = Mathf.Clamp(_currentShield, 0, maxShield);
        UpdateHealthBar();
    }

    private void Die()
    {
        IsAlive = false; // sets is alive to fals
        
        if (_playerController != null)
            _playerController.enabled = false;

        if (_meshCollider != null)
            _meshCollider.enabled = false;

        if (gameRoundManager != null) 
        {
            gameRoundManager.CheckForRoundWinner();
        }
        else
        {
            Debug.LogWarning("GameRoundManager not found! Could not notify about player death.");
        }
    }
}
