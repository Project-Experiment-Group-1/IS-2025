using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_bar : MonoBehaviour
{
    public Slider sli;

    public void setMaxMP(int MP)
    {
        sli.maxValue = MP;
        sli.value = MP;
    }
    public void setMP_bar(int MP)
    {
        sli.value = MP;
    }

}