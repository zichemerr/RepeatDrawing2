using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _level;
    public ButtonManager backButton;
    public ButtonManager nextButton;
    
    public Sprite background;
    public Sprite backgroundBlur;

    public void Init()
    {
        _background.sprite = background;
    }
    
    public void DrawLevel()
    {
        _background.sprite = backgroundBlur;
        _mainMenu.SetActive(false);
        _level.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    public void DrawMenu()
    {
        _background.sprite = background;
        _mainMenu.SetActive(true);
        _level.SetActive(false);
        backButton.gameObject.SetActive(false);
    }
}