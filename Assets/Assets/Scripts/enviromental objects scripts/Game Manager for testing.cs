using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManagerfortesting : MonoBehaviour
{
    [SerializeField] private GameObject returnToMain;
    [SerializeField] private GameObject player;
    private bool panelOpen = false;

    private void Start()
    {
        returnToMain.SetActive(false);
    }

    void Update()
    {
        if (panelOpen == true)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelOpen = true;
            returnToMain.SetActive(true);
            player.SetActive(false);
        }
    }

    public void CloseMP()
    {
        returnToMain.SetActive(false);
        player.SetActive(true);
        panelOpen = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void ReturnM()
    {
        SceneManager.LoadScene(0);
    }
}
