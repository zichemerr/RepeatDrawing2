using UnityEngine;

public class Hover : MonoBehaviour
{
    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}