using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractTarget {
    void Interact();

    bool BusyCheck();
}

public class InteractTarget :MonoBehaviour, IInteractTarget
{
    public virtual void Interact()
    {

    }
    public virtual bool BusyCheck()
    {
        return false;
    }
}
