using UnityEngine;

public class ClickTile : MonoBehaviour
{
    [SerializeField] private Tutorial tut;
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
                            if(tut!=null)
                            {
                                if (tut.tut1Done==false)
                                {
                                    tut.tut1Done = true;
                                }
                            }
                            SoundManager.Instance.PlayButtonSound();
                            GameObject.FindFirstObjectByType<LevelManager>().Remove(tile);
                            HandManager.Instance.AddUndo(tile.GetID(), tile);
                            tile.posToMove = HandManager.Instance.AddTilePos(tile.GetID())+new Vector3(0,0.75f,0f);
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
