using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField]
    float radius;
    [SerializeField]
    CircleCollider2D myAffectableRadius;
    bool isActivated = false;
    void Start()
    {
        if (myAffectableRadius == null)
        {
            Debug.LogError("Pressure plate is missing the CircleColllider2D Component or reference to it.");
        }
        myAffectableRadius.radius = radius;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        Debug.Log("Something entered my collider");
        if (collision.gameObject.GetComponent<Bomb>() != null && !isActivated)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
            isActivated = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isActivated)
        {
            var affectable = collision.GetComponent<IAffectable>();
            if (affectable != null)
            {
                affectable.OnActivate();
            }

        }
    }

}
