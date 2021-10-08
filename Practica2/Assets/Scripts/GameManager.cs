using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int totalItems;
    private int playerItems = 0;
    public Text itemText;
    public GameObject winPanel;

    private void Start()
    {
        DesactivatePanel();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (winPanel.activeInHierarchy == false)
            {
                ActivatePanel();
            }
            else
            {
                DesactivatePanel();
            }
        }
    }
    public void InitialiceItemText()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Item").Length;
        itemText.text = playerItems + "/" + totalItems;
    }
    public void PickItem()
    {
        playerItems++;
        itemText.text = playerItems + "/" + totalItems;
        if(playerItems >= totalItems)
        {
            ActivatePanel();
        }
    }
    private void ActivatePanel()
    {
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void DesactivatePanel()
    {
        winPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Continue()
    {
        DesactivatePanel();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
