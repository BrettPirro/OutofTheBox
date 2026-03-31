using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;



namespace Box.Player 
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerAttack))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] TMP_Text boxText;
        [SerializeField] TMP_Text potionText;



        InputSystem_Actions inputs;
        InputAction move;
        InputAction melee;
        InputAction range;
        Action<Vector2> updatePlayerVelocity;
        Action playerMeleeAttacking;
        Action playerRangeAttacking;

        int potions=0;
        int boxes = 0;


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

    
        public void usePotions() 
        {
            if (potions <= 0) { return; }
            GetComponent<Health>().Heal(2);
            potions -= 1;
            potionText.text = potions.ToString();

        }

        public void useBox()
        {
            if (boxes <= 0) { return; }
            if (Random.Range(0, 10) > 9) { SceneLoader.current.loadSceneName("GameWon"); }
            boxes -= 1;
            boxText.text = boxes.ToString();

        }


        public void AddBox(int num) 
        {
            boxes += num;
            boxText.text = boxes.ToString();
        }

        public void AddPotions(int num)
        {
            potions += num;
            potionText.text = potions.ToString();
        }

    }

}