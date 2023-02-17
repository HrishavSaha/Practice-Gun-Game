using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  private PlayerInput playerInput;
  public PlayerInput.OnFootActions onFoot;

  private PlayerMotor motor;
  private PlayerLook look;
  
  private PlayerHealth playerHealth;
    void Awake()
    {
      playerInput = new PlayerInput();
      onFoot = playerInput.OnFoot;
      motor = GetComponent<PlayerMotor>();
      look = GetComponent<PlayerLook>();
      playerHealth = GetComponent<PlayerHealth>();
      onFoot.Jump.performed += ctx => motor.Jump();
      onFoot.Crouch.performed += ctx => motor.Crouch();
      onFoot.Sprint.performed += ctx => motor.Sprint();
      onFoot.TempTakeDamage.performed += ctx => playerHealth.TakeDamage(Random.Range(5, 10));
      onFoot.TempRestoreHealth.performed += ctx => playerHealth.RestoreHealth(Random.Range(5, 10));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    void LateUpdate(){
      look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
      onFoot.Enable();
    }

    private void OnDisable()
    {
      onFoot.Disable();
    }
}
