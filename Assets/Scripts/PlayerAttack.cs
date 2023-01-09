using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput _Input;

    private Animation _AttackAnim;

    private bool _canAttack = true;
    private float _attackCooldown = 1f;

    bool colidindo;

    GameObject colisor;

    private void Awake()
    {
        _Input = new PlayerInput();
        _AttackAnim = gameObject.GetComponent<Animation>();
    }

    private void OnEnable()
    {
        _Input.Enable();

        _Input.Gameplay.Attack.performed += OnAttack;
        _Input.Gameplay.Attack.canceled += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext obj)
    {
        if (_canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnDisable()
    {
        _Input.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        
        if (collision.gameObject.tag == "Enemy")
        {
            colidindo = true;
            colisor = collision.gameObject;
        }

        //Debug.Log(colisor);
        //Destroy(colisor);
    }

    IEnumerator Attack()
    {
        _canAttack = false;
        Debug.Log("Atacando");
        _AttackAnim.Play("AttackTest");

        if (colidindo)
        {
            Destroy(colisor);
        }
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
}
