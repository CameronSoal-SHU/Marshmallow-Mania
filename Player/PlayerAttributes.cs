using UnityEngine;

public class PlayerAttributes : HealthHandler
{
    [Header("Score")]
    protected float scoreDrainDelay = 1f;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private int scoreDrainPerSec = 0;

    [Header("Projectile/Attack Stats")]
    [SerializeField] private int damagePoints = 0; // Damage done per shot
    [SerializeField] private float fireRate = 0;   // Shots fired per second
    [SerializeField] private float projectileSpeed = 0; // Speed shots travel
    //[SerializeField] private float projectileRange = 0; // Shots range (m)

    [Header("Movement Stats")]
    [SerializeField] private float movementSpeed = 0; // Players movement speed

    [Header("Screen Shake Stats")]
    [SerializeField] [Range(0, 1)] float shake = 0.0f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip[] hurtSounds = null;
    [SerializeField] AudioClip deathSound = null;
    [SerializeField] AudioClip powerupSound = null;
    [SerializeField] AudioClip winSoundEffect = null;
    private bool deathSoundPlayed = false;

    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void ChangeHp(float healthChange, bool isPowerup)
    {
        base.ChangeHp(healthChange, isPowerup);

        if (healthChange < 0 && isAlive)
        {
            Camera.main.GetComponent<CameraControl>().AddShake(shake);

            audioSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Length - 1)]);
        }

        if (!isAlive && !deathSoundPlayed) { audioSource.PlayOneShot(deathSound); deathSoundPlayed = true; }
    }

    public void PlayPowerUpSound() { audioSource.PlayOneShot(powerupSound); }
    public void PlayWinSound() { audioSource.PlayOneShot(winSoundEffect); }

    private void Update()
    {
        if (playerScore != 0) scoreDrainDelay -= Time.deltaTime;
        if (scoreDrainDelay <= 0) { DecrementScore(scoreDrainPerSec); scoreDrainDelay = 1f; };
    }

    public void DecrementScore(int scoreDecrease) { playerScore -= scoreDecrease; }
    public void IncreaseScore(int scoreIncrease) { playerScore += scoreIncrease; }

    public int GetCurDamage() { return damagePoints; }
    public float GetCurProjSpeed() { return projectileSpeed; }
    public float GetCurFireRate() { return fireRate; }
    public float GetCurMoveSpeed() { return movementSpeed; }
    public int GetCurPlayerScore() { return playerScore; }
    public int GetCurPlayerDamage() { return damagePoints; }

    public void SetProjSpeed(float newProjSpeed) { projectileSpeed = newProjSpeed; }
    public void SetFireRate(float newFireRate) { fireRate = newFireRate; }
    public void SetMoveSpeed(float newMoveSpeed) { movementSpeed = newMoveSpeed; }
    public void ChangeDamage(int damageChange) { damagePoints += damageChange; }
}
