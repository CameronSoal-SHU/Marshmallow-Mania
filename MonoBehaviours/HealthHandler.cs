using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public delegate void VoidDel();
    public event VoidDel onDeath;

    protected bool isAlive = true;
    public bool IsAlive { get { return isAlive; } }

    [Header("Health Values")]
    [SerializeField] private int curMaxHealth = 100;
    [SerializeField] private int curHealth = 0;

    [Header("Score Awarded")]
    [SerializeField] private int scoreAwardedOnHit = 0;
    [SerializeField] private int scoreAwardedOnDeath = 0;
    // Armour
    protected const int minArmour = 0;
    protected const int maxArmour = 100;
    [Range(0, 100)] [SerializeField] private int curArmourValue = 0;

    Animator animator;
    private PlayerAttributes playerAttributes;

    private void Awake()
    {
        // Set the stat values initially
        curHealth = curMaxHealth;
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Changing stat values
    public virtual void ChangeHp(float healthChange, bool isPowerup)
    {
        if (!isAlive) return;

        if (healthChange < 0 && !isPowerup)
        {
            ChangeArmour((int)healthChange / 2); // reduce armour on damage taken
            healthChange *= (1 - ((float)curArmourValue / 100));

            if (animator != null) animator.SetTrigger("Hurt");
        }

        curHealth = Mathf.Clamp(curHealth + (int)healthChange, 0, curMaxHealth);

        playerAttributes.IncreaseScore(scoreAwardedOnHit); // give player score on a successful hit

        if (curHealth == 0) Die();
    }

    public void ChangeMaxHealth(int maxHealthChange)
    {
        curMaxHealth += maxHealthChange;
        if (maxHealthChange != 0) curHealth = curMaxHealth; // fully heal player
    }

    // For getting the current health of the game object
    public float GetCurMaxHealth() { return curMaxHealth; }

    public float GetCurHealth() { return curHealth; }

    public float GetCurArmour() { return curArmourValue; }

    public void ChangeArmour(int armourChange) { curArmourValue = Mathf.Clamp(curArmourValue + armourChange, minArmour, maxArmour); }

    private void Die()
    {
        isAlive = false;
        onDeath?.Invoke();
        playerAttributes.IncreaseScore(scoreAwardedOnDeath);
        if (animator != null) 
        {
            animator.ResetTrigger("Hurt");
            animator.SetTrigger("Death");
        }
    }
}
