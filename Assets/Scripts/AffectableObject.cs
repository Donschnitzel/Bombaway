using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectableObject : MonoBehaviour,IAffectable
{
    bool isAffected = false;
    public void OnActivate()
    {
        isAffected = true;
    }
    private void Update()
    {
        Debug.Log("I " + gameObject.name + " am affected: " + isAffected);
    }

}
