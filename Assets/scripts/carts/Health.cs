using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int PlayerID; 
    public bool IsAlive = true;
    public int maxHealth = 4; 
    public int maxShield = 2;
    private int _currentHealth; 
    private int _currentShield;
    private Animator _animator; 

    private PlayerMovement _playerController; 
    private MeshCollider _meshCollider; 
    private Rigidbody _rigidbody; 

    private Gamepad _playerGamepad; 
    private PlayerInput _playerInput; 
    
    public static Health instance;
    public GameRoundManager gameRoundManager;

    private void Start()
    {
        _playerController = GetComponent<PlayerMovement>(); 
        _meshCollider = GetComponentInChildren<MeshCollider>(); 
        _rigidbody = GetComponent<Rigidbody>(); 

        _currentHealth = maxHealth; 
        _currentShield = maxShield;

        AssignGamepad(); 


        if (gameRoundManager == null)
        {
            gameRoundManager = FindObjectOfType<GameRoundManager>(); 
        }
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
    }

    public void RegainHealth(int healthAmount)
    {
        _currentHealth += healthAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth);
    }

    public void RegainShield(int shieldAmount)
    {
        _currentShield += shieldAmount;
        _currentShield = Mathf.Clamp(_currentShield, 0, maxShield);
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
