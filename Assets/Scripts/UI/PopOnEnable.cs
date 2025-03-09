using DG.Tweening;
using UnityEngine;

public class PopOnEnable : MonoBehaviour
{
    bool isPop = false;
    private void OnEnable()
    {
        if (!isPop)
        {
            isPop = true;
            transform.DOScale(new Vector3(0.25f, 0.25f, 1), 0f).OnComplete(() =>
            {
                transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.2f).OnComplete(() =>
                {
                    transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
                });
            });
        }
    }

    public void Disable()
    {
        isPop = false;
        transform.DOScale(new Vector3(1f, 1f, 1), 0f).OnComplete(() =>
        {
            transform.DOScale(new Vector3(1.1f, 1.1f, 1), 0.2f).OnComplete(() =>
            {
                transform.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.3f).OnComplete(()=>transform.parent.gameObject.SetActive(false));
            });
        });
    }
}
