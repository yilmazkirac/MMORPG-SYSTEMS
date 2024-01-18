using UnityEngine;

public class ToolItemInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       IEnemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Execute(10);
        }
    }
}
