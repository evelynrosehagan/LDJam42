using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {

    void Interact();
	
}

public class Interactable : MonoBehaviour, IInteractable
{
    protected bool Busy;    

    [SerializeField]
    protected InteractTarget interactTarget;
    public virtual void Interact()
    {

    }
}
