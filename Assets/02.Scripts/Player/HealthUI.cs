using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Color _originColor = Color.white;
    [SerializeField] private Color _emptyColor;
    [SerializeField] private Image[] _hearts;
    private int _index = 0;

    public void OnHealthUI()
    {
        _hearts[--_index].color = _originColor;
    }

    public void OffHealthUI()
    {
        _hearts[_index++].color = _emptyColor;
    }
}
