using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _level;
    
    public void DrawLevel()
    {
        _mainMenu.SetActive(false);
        _level.SetActive(true);
    }
}