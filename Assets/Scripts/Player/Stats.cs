using UnityEngine;

public class Stats : MonoBehaviour
{
    public int Health;
    public int Armor;
    public int Mana;
    public int Damage;
    public int CritChange;
    private void Start()
    {
        UpdateUIStats();
    }

    public void UpdateStats(int health=0,int armor=0,int mana=0,int damage=0,int crit=0)
    {
        Health += health;
        Armor += armor;
        Mana += mana;
        Damage += damage;
        CritChange += crit;
        UpdateUIStats();
    }

    public void UpdateUIStats()
    {
        UIManager.Instance.HealthText.text=Health.ToString();
        UIManager.Instance.ArmorText.text = Armor.ToString();
        UIManager.Instance.ManaText.text = Mana.ToString();
        UIManager.Instance.DamageText.text = Damage.ToString();
        UIManager.Instance.CritText.text = CritChange.ToString();
    }
}
