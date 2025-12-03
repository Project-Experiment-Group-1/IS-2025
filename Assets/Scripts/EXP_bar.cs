using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EXP_bar : MonoBehaviour
{
    public Slider sli;

    public void setMaxEXP(int EXP)
    {
        sli.maxValue = EXP;
        sli.value = EXP;
    }
    public void setEXP_bar(int EXP)
    {
        sli.value = EXP;
    }

}