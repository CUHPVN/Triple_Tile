using Unity.Burst.CompilerServices;
using UnityEngine;

public class HideOnCollision : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask; 
    [SerializeField] private float castRadius = 0.45f; 
    [SerializeField] private bool isHidden = false;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckCircleCastAll();
        ChangeColor();

    }
    void ChangeColor()
    {
        if (isHidden)
        {
            Darken();
        }
        else
        {
            ResetColor();
        }
    }
    private void CheckCircleCastAll()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, castRadius, layerMask);
        foreach (var collider in colliders)
        {
            if (collider != null)
            {
                var otherSprite = collider.GetComponent<SpriteRenderer>();
                if (otherSprite != null && otherSprite.sortingOrder > spriteRenderer.sortingOrder)
                {
                    this.isHidden = true;
                    GetComponent<NodeTile>().SetCanClick(false);
                    return;
                }
            }
        }
        this.isHidden = false;
        GetComponent<NodeTile>().SetCanClick(true);
    }
    private void Darken()
    {
        Color targetColor = HexToColor("#7F7F7F");
        spriteRenderer.color = targetColor;
    }
    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        Debug.LogWarning("Invalid hex color string");
        return Color.white;
    }
    private void ResetColor()
    {
        Color targetColor = HexToColor("#FFFFFF");
        spriteRenderer.color = targetColor;
    }
}
