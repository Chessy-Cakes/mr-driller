using UnityEngine;
using System.Collections;

public class Block : EnvironmentObject
{
  bool toBeDestroyed = false;

  protected override void Start()
  {
    base.Start ();
  }

  public override void AttemptDestroy() {
    DestroyBlock ();
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

  public virtual bool willBeDestroyed() {
    return toBeDestroyed;
  }

  protected void CheckHit(Vector2 vect) {
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

