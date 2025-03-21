using Unity.VisualScripting;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            if (hits.Length > 0 && !TripleManager.Instance.GetPause())
            {
                System.Array.Sort(hits, (h1, h2) => h2.collider.GetComponent<SpriteRenderer>().sortingOrder.CompareTo(h1.collider.GetComponent<SpriteRenderer>().sortingOrder));
                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        NodeTile tile = hit.collider.GetComponent<NodeTile>();
                        if (tile.GetCanClick()&&!tile.GetIsClick())
                        {
                            GameObject.FindFirstObjectByType<LevelManager>().Remove(tile);
                            HandManager.Instance.AddUndo(tile.GetID(), tile);
                            tile.posToMove = HandManager.Instance.AddTilePos(tile.GetID());
                            tile.Move();
                            //Destroy(hit.collider.gameObject,0.51f);
                            break;
                        }
                    }
                }
            }
        }
    }
}
