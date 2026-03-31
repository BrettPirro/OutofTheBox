using Box.Player;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool paused = false;
    [SerializeField] GameObject pauseMenu;

    public void PauseGame() 
    {
        paused = (!paused);
        GetComponent<PlayerMovement>().toggleMovement(paused);
        pauseMenu.SetActive(paused);

    }


}
