using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
   public int Health=20;
    public void Execute(int damage)
    {
        Health -= damage;
        if (Health<=0)
        {
            Destroy(gameObject);
        }
    }
}
