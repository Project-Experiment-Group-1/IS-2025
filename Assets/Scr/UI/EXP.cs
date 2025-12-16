using UnityEngine;

public class EXP : MonoBehaviour
{
    Animator anim;

    public EXP_bar EXP_bar;
    public PlayerStatus playerStatus;

    void Start()
    {
        anim = GetComponent<Animator>();

        EXP_bar.setMaxEXP(playerStatus.maxEXP);
        EXP_bar.setEXP_bar(playerStatus.currentEXP);
    }
}
