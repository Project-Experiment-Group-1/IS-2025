using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    Animator anim;

    public HP_bar HP_bar;

    public int maxHP = 10;

    public int currentHP;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
    }

    private void playerDamaged(int damage)
    {
        currentHP -= damage;
        HP_bar.setHP_bar(currentHP);
    }
}