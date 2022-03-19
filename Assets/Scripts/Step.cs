using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Step : MonoBehaviour
{
    public bool isPlayer = true;

    public bool IsPlayer {
        get {return isPlayer;}
    }

    public void ChangeText(string newText){
        TextMeshProUGUI rappelText = GameObject
            .Find("RappelText")
            .transform
            .GetComponent < TextMeshProUGUI > ();

            rappelText.text = newText;
    }

}
