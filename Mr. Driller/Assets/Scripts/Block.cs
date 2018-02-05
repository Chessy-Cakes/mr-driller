using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
  protected Rigidbody2D rb;
  bool toBeDestroyed = false;

  void Start()
  {
    rb = GetComponent<Rigidbody2D> ();
  }

  public void DestroyBlock()
  {
    toBeDestroyed = true;
    CheckHit (Vector2.down);
    CheckHit (Vector2.right);
    CheckHit (Vector2.left);
    CheckHit (Vector2.up);
    Object.Destroy (this.gameObject);
  }

  public bool willBeDestroyed() {
    return toBeDestroyed;
  }

  private void CheckHit(Vector2 vect) {
    Debug.DrawRay (rb.position, vect, Color.green);
    RaycastHit2D hit = Physics2D.Raycast (rb.position + vect, vect, 0f);
    if (hit.collider != null) {
      if (hit.collider.gameObject.name == this.gameObject.name) {
        Block otherBlock = hit.collider.gameObject.GetComponent<Block>();
        if (otherBlock.willBeDestroyed() == false) {
          otherBlock.DestroyBlock ();
        }
      }
    }
  }
}

