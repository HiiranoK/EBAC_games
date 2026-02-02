using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private InputAction _moveAction;
    
    
    private void Awake()
    {
        
        _moveAction = InputSystem.actions.FindAction("Move");
        if (_moveAction == null)
        {
            Debug.LogError("No action found with this name!");
        }
    }

    void Update()
    {
        if (_moveAction == null) return;
        
        float movementInput = _moveAction.ReadValue<Vector2>().y;      
        float movement= movementInput * speed * Time.deltaTime;
        float newY = transform.position.y + movement;
        
        newY = Mathf.Clamp(newY, -4f, 4f);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
