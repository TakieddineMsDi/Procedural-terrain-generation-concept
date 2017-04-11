using UnityEngine;
using UnityEngine.UI;

public class Health_SP : MonoBehaviour
{
	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public RectTransform healthBar;
	public bool destroyOnDeath;

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		OnChangeHealth (currentHealth);
		if (currentHealth <= 0)
		{
			if (destroyOnDeath)
			{
				Destroy(gameObject);
			} 
			else
			{
				currentHealth = maxHealth;

				// called on the Server, will be invoked on the Clients
			}
		}
	}
	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}

}