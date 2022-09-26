using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dateui : MonoBehaviour
{
    public TMP_Text hourt, minutet, dayt, montht, yeart, weekdayt;

    public GlobalTime gt;


    void Update()
    {
        if(gt.minute < 10)
        {
            minutet.SetText($"0{gt.minute}");
        }
        else
        {
            minutet.SetText($"{gt.minute}");
        }

        if(gt.hour < 10)
        {
            hourt.SetText($"0{gt.hour}");
        }
        else
        {
            hourt.SetText($"{gt.hour}");
        }

        if(gt.day < 10)
        {
            dayt.SetText($"0{gt.day}");
        }
        else
        {
            dayt.SetText($"{gt.day}");
        }

        switch(gt.weekday)
        {
            case 1:
                weekdayt.SetText($"Montag");
                break;
            case 2:
                weekdayt.SetText($"Dienstag");
                break;
            case 3:
                weekdayt.SetText($"Mittwoch");
                break;
            case 4:
                weekdayt.SetText($"Donnerstag");
                break;
            case 5:
                weekdayt.SetText($"Freitag");
                break;
            case 6:
                weekdayt.SetText($"Samstag");
                break;
            case 7:
                weekdayt.SetText($"Sonntag");
                break;
        }

        switch(gt.month)
        {
            case 1:
                montht.SetText($"Jän");
                break;
            case 2:
                montht.SetText($"Feb");
                break;
            case 3:
                montht.SetText($"Mär");
                break;
            case 4:
                montht.SetText($"Apr");
                break;
            case 5:
                montht.SetText($"Mai");
                break;
            case 6:
                montht.SetText($"Jun");
                break;
            case 7:
                montht.SetText($"Jul");
                break;
            case 8:
                montht.SetText($"Aug");
                break;
            case 9:
                montht.SetText($"Sep");
                break;
            case 10:
                montht.SetText($"Okt");
                break;
            case 11:
                montht.SetText($"Nov");
                break;
            case 12:
                montht.SetText($"Dec");
                break;
        }

        yeart.SetText($"{gt.year}");

    }
}
