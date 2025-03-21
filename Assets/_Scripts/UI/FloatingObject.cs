using DG.Tweening;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] private Vector3 scale;
    private void Awake()
    {
        scale = transform.localScale;
    }
    private void OnEnable()
    {
        transform.localScale = scale;
        transform.DOMoveY(transform.position.y + 1f, 1f).SetEase(Ease.OutQuad);
        transform.DOScale(transform.localScale * 1.1f, 1f).SetEase(Ease.OutQuad);
    }
}
