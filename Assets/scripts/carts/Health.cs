using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int PlayerID; 
    public bool IsAlive = true;
    public int maxHealth = 100; 
    private int _currentHealth; 
    private Animator _animator; 

    private CartMovement _playerController; 
    private BoxCollider _boxCollider; 
    private Rigidbody _rigidbody; 

    private Gamepad _playerGamepad; 
    private PlayerInput _playerInput; 
    
    public static Health instance;
    public GameRoundManager gameRoundManager;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<CartMovement>(); 
        _boxCollider = GetComponent<BoxCollider>(); 
        _rigidbody = GetComponent<Rigidbody>(); 

        _currentHealth = maxHealth; 
        _animator.SetBool("IsDead", false);

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
        _currentHealth -= damageAmount; 
        _currentHealth = Mathf.Clamp(_currentHealth, 0, maxHealth); 
        
        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    public void Die()
    {
        IsAlive = false; // sets is alive to fals
        
        if (_playerController != null)
            _playerController.enabled = false;

        if (_boxCollider != null)
            _boxCollider.enabled = false;

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
