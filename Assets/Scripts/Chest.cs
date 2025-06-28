using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestUI; 

    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenChest()
    {
        if (!isOpen)
        {
            chestUI.SetActive(true);
            GameModeManager.instance.SetGameMode(GameModeManager.GameMode.Inventory);
            isOpen = true;
        }
    }
    public void CloseChest()
    {
        if (isOpen)
        {
            chestUI.SetActive(false);
            GameModeManager.instance.SetGameMode(GameModeManager.GameMode.Gameplay);
            isOpen = false;
        }
    }
}
