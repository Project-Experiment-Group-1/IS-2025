using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_bar : MonoBehaviour
{
    public Slider sli;

    public void setMaxHP(int HP)
    {
        sli.maxValue = HP;
        sli.value = HP;
    }
    public void setHP_bar(int HP)
    {
        sli.value = HP;
    }
   
}
