using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool inMenu;


    [Header("UI Panels")]

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;


    private BunnyInput bunnyInput;
    private InputAction pause;


    private void Awake()
    {
        bunnyInput = new BunnyInput();
        Time.timeScale = 1;
        inMenu = false;
    }


    private void OnEnable()
    {
        pause = bunnyInput.Bunny.Pause;


        pause.Enable();
    }


    private void OnDisable()
    {
        pause.Disable();
    }


    public void OnEscape()
    {
        if (inMenu == false)
        {
            Time.timeScale = 0;


            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;


            pauseMenuPanel.SetActive(true);


            inMenu = true;
        }


        else if (inMenu == true)
        {
            Time.timeScale = 1;

            pauseMenuPanel.SetActive(false);
            settingsPanel.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;



            inMenu = false;
        }
    }


    public void OnResume()
    {
        Time.timeScale = 1;


        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        inMenu = false;
    }
}
