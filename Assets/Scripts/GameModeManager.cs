using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;

    public GameObject inventory;
    public GameObject trashBin;

    public enum GameMode
    {
        Gameplay,
        Building,
        Inventory
    }
    public GameMode currentMode = GameMode.Gameplay;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SetGameMode(currentMode);
    }
    public void ToggleBuildMode(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(currentMode == GameMode.Building)
            {
                Debug.Log("Gameplay");
                SetGameMode(GameMode.Gameplay);
            }
            else
            {
                Debug.Log("Building");
                SetGameMode(GameMode.Building);
            }
        }
    }
    public void ToggleInventory(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(currentMode == GameMode.Inventory)
            {
                Debug.Log("Gameplay");
                SetGameMode(GameMode.Gameplay);
            }
            else
            {
                Debug.Log("inventory");
                SetGameMode(GameMode.Inventory);
            }
        }
    }
    public void SetGameMode(GameMode mode)
    {
        currentMode = mode;

        switch (mode)
        {
            case GameMode.Gameplay:
                Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                trashBin.SetActive(false);
                inventory.SetActive(false);
                break;
            case GameMode.Building:
                inventory.SetActive(false);
                trashBin.SetActive(false);
                Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                break;
            case GameMode.Inventory:
                inventory.SetActive(true);
                trashBin.SetActive(true);
                Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                break;
        }
    }
    public bool IsInGameplayMode() => currentMode == GameMode.Gameplay;

}
