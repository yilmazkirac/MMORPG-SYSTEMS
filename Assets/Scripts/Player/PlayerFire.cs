using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform _head;
    RaycastHit hit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_head.position, _head.forward, out hit, 4))
            {
                ICommand command = hit.collider.GetComponent<ICommand>();

                if (command != null)
                {
                    command.Execute();
                }
            }
        }
    }
}
