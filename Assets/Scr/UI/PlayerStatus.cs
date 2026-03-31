using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxHP = 100f;
    public float currentHP;

    public float maxEXP = 100f;
    public float currentEXP;

    void Start()
    {
        currentHP = Random.Range(30f, maxHP);
        currentEXP = Random.Range(0f, maxEXP);
    }
}

