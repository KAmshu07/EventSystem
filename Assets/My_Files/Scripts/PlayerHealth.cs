using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
	#region -------------------------Variables-------------------------

	public bool ResetHP;
	public FloatVariable HP;
	public FloatVariable currentSpeed;
	public FloatReference StartingHP;
	public UnityEvent DamageEvent;
	public UnityEvent DeathEvent;

	#endregion -------------------------Variables-------------------------

	#region ----------------------Unity Callbacks----------------------

	private void Start()
	{
		if (ResetHP)
			HP.SetValue(StartingHP);
	}

	private void OnTriggerEnter(Collider other)
	{
		DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
		if (damage != null && currentSpeed.Value > 5f)
		{
			HP.ApplyChange(-damage.DamageAmount);
			DamageEvent.Invoke();
		}

		if (HP.Value <= 0.0f)
		{
			DeathEvent.Invoke();
		}
	}

	#endregion ---------------------Unity Callbacks---------------------
}