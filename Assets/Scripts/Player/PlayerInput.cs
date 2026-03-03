using System;
using UnityEngine;
using UnityEngine.InputSystem;



namespace Box.Player 
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerInput : MonoBehaviour
    {
        InputSystem_Actions inputs;
        InputAction move;
        InputAction melee;
        Action<Vector2> updatePlayerVelocity;
        Action playerMeleeAttacking;


        private void Awake()
        {
            inputs = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            move = inputs.Player.Move;
            melee = inputs.Player.Attack;
            melee.Enable();
            move.Enable();
            updatePlayerVelocity += GetComponent<PlayerMovement>().updatePlayerVelocity;
            playerMeleeAttacking += GetComponent<PlayerAttack>().playerMeleeAttack;

        }

        private void OnDisable()
        {
            melee.Disable();
            move.Disable();
            updatePlayerVelocity -= GetComponent<PlayerMovement>().updatePlayerVelocity;
            playerMeleeAttacking -= GetComponent<PlayerAttack>().playerMeleeAttack;

        }

        private void FixedUpdate()
        {
            var inputVelocity = move.ReadValue<Vector2>();
            var tranformedVelocity = ((MathF.Abs(inputVelocity.x) + MathF.Abs(inputVelocity.y)) / 2) > .6 ? (inputVelocity / 1.5f) : inputVelocity;
            updatePlayerVelocity(tranformedVelocity);

            if (melee.IsPressed()) { playerMeleeAttacking(); }
        }




    }

}