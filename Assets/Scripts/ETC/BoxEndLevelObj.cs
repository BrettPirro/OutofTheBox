using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BoxEndLevelObj : MonoBehaviour
{
    InputSystem_Actions inputs;
    InputAction interact;

    [SerializeField] Slider loading;
    int loadedLevel=0;

    [SerializeField] float loadingAmountIncrement = .5f;
    bool loadingBox=false;
    bool pressed = false;
    [SerializeField] List<Sprite> sprites = new List<Sprite>();
    [SerializeField] SpriteRenderer renderer;

    private void Awake()
    {
        inputs = new InputSystem_Actions();
        interact = inputs.Player.Interact;


    }

    void Start()
    {
        loading.gameObject.SetActive(false);
        loadedLevel=BoxLevelManager.current.levelNum;
        renderer.sprite = sprites[loadedLevel];
    }


    private void OnEnable()
    {
        interact.Enable();
        interact.performed += InteractingWithBox;
        interact.canceled += StopInteractingWithBox;

    }


    private void OnDisable()
    {
        interact.Disable();
        interact.performed -= InteractingWithBox;
        interact.canceled -= StopInteractingWithBox;


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            loadingBox = true;
            loading.gameObject.SetActive(true);
           

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            loadingBox = false;
            loading.gameObject.SetActive(false);
            loading.value = 0;
        }
    }
    private void StopInteractingWithBox(InputAction.CallbackContext content) 
    {
        pressed = false;
        loading.value = 0;
    }
    private void InteractingWithBox(InputAction.CallbackContext content) 
    {
        pressed = true;
    
    }

    private void FixedUpdate()
    {
        if (!pressed) { return; }
        loading.value = (loadingAmountIncrement * Time.deltaTime) + loading.value;


        if (loading.value == loading.maxValue) 
        {
            BoxLevelManager.current.levelNum = loadedLevel + 1;
        
            if(loadedLevel != 4) 
            {
                 FindObjectOfType<BinaryRoomGen>().NextLevel();

            }
        }

        if (loadedLevel >= 4)
        {
            SceneLoader.loadScenebyName("GameWon");
        }


    }



}
