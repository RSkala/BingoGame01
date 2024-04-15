using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BingoGame : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // Handle mouse click on Bingo Cell
        Mouse mouse = Mouse.current;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseWorldPos, Vector3.forward);
            if(raycastHit2D.collider != null)
            {
                Debug.Log("collider hit " + raycastHit2D.collider.gameObject.name);
            }
        }
    }

    void OnFire(InputValue inputValue)
    {
        //Debug.Log("OnFire - " + inputValue.Get().ToString());
    }
}
