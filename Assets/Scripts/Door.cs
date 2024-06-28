using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite _closedSprite;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] UnityEvent _onOpened;
    private void Awake()
    {

        _renderer.sprite = _closedSprite;

    }
    public void Open()
    {
        _onOpened?.Invoke();
        Destroy(gameObject);
    }
}
