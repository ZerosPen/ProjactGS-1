using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Iinteracttable _intreactAble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Iinteracttable iinteracttable = collision.GetComponent<Iinteracttable>();
        if(iinteracttable != null)
        {
            _intreactAble = iinteracttable;
        }
    }

    public void IntreactWithObject(InputAction.CallbackContext contex)
    {
        if(contex.performed &&_intreactAble != null) _intreactAble.intreact();
    }
}
