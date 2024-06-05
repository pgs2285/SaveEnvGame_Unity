using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{



    public void LookAt(Transform transform);
    public void Interact();
    public bool CanInteract();

}
