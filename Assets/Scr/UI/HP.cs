using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    Animator anim;

    public HP_bar HP_bar;
    public PlayerStatus playerStatus;

    void Start()
    {
        anim = GetComponent<Animator>();

        HP_bar.setMaxHP(playerStatus.maxHP);
        HP_bar.setHP_bar(playerStatus.currentHP);
    }

    public void PlayerDamaged(float damage)
    {
        playerStatus.currentHP -= damage;
        playerStatus.currentHP = Mathf.Clamp(playerStatus.currentHP, 0f, playerStatus.maxHP);

        HP_bar.setHP_bar(playerStatus.currentHP);
    }
}