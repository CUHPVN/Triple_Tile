using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    public float orthoSizeMultiplier = 1.0f;
    public float buffer = 1.0f;

    [SerializeField] private Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
        AdjustOrthoSize();
    }
    private void Update()
    {
        //AdjustOrthoSize();
    }
    private void Reset()
    {
        cam = GetComponent<Camera>();
        AdjustOrthoSize();
    }
    private void AdjustOrthoSize() {
        var (center, size) = CalculateOrthoSize();
        cam.transform.position = center;
        cam.orthographicSize = size;
    }
    private (Vector3 center, float size) CalculateOrthoSize()
    {
        var bounds = new Bounds();

        foreach (var col in FindObjectsOfType<Collider2D>())
            bounds.Encapsulate(col.bounds);

        bounds.Expand(buffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * (Screen.height-100) / Screen.width;
        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0,0,-10);
        return (center, size);
    }
}
