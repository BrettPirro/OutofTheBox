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
        InputAction range;
        Action<Vector2> updatePlayerVelocity;
        Action playerMeleeAttacking;
        Action playerRangeAttacking;


        private void Awake()
        {
            inputs = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            move = inputs.Player.Move;
            melee = inputs.Player.Attack;
            range = inputs.Player.Range;
            melee.Enable();
            move.Enable();
            range.Enable();
            playerRangeAttacking += GetComponent<PlayerAttack>().RangeAttack;
            updatePlayerVelocity += GetComponent<PlayerMovement>().updatePlayerVelocity;
            playerMeleeAttacking += GetComponent<PlayerAttack>().MeleeAttack;

        }

        private void OnDisable()
        {
            melee.Disable();
            move.Disable();
            range.Disable();
            playerRangeAttacking -= GetComponent<PlayerAttack>().RangeAttack;
            updatePlayerVelocity -= GetComponent<PlayerMovement>().updatePlayerVelocity;
            playerMeleeAttacking -= GetComponent<PlayerAttack>().MeleeAttack;

        }

        private void FixedUpdate()
        {

            var inputVelocity = move.ReadValue<Vector2>();
            var tranformedVelocity = ((MathF.Abs(inputVelocity.x) + MathF.Abs(inputVelocity.y)) / 2) > .6 ? (inputVelocity / 1.5f) : inputVelocity;
            updatePlayerVelocity(tranformedVelocity);

            if (melee.IsPressed()) { playerMeleeAttacking(); }
            else if (range.IsPressed()) { playerRangeAttacking(); }
        }

    


    }

}