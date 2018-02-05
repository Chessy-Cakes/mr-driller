using UnityEngine;
using System.Collections;

public abstract class EnvironmentObject : MonoBehaviour
{
  protected Rigidbody2D rb;

  protected virtual void Start()
  {
    rb = GetComponent<Rigidbody2D> ();
  }

  public abstract void AttemptDestroy (); 
}

