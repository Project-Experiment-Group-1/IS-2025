using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_bar : MonoBehaviour
{
    public Slider sli;

    public void setMaxMP(float MP)
    {
        sli.maxValue = MP;
        sli.value = MP;
    }
    public void setMP_bar(float MP)
    {
        sli.value = MP;
    }

}