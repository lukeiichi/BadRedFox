using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static MyExtensions;

public class Step : MonoBehaviour
{
    public enum EtapeEnum{
        God,
        Fox,
        Target
    }

    public bool isPlayer = true;
    private EtapeEnum lastStep = EtapeEnum.God;
    public EtapeEnum etape;

    public bool IsPlayer {
        get {return isPlayer;}
    }
    public EtapeEnum Etape{
        get { return etape; }
    }

    // Passe le jeu autamitiquement à l'étape suivant // Dieu => Target => Fox => Target ... 
    public void NextStep(){
        string message = "";
        if(etape == EtapeEnum.Target){
            if(lastStep == EtapeEnum.God){
                etape = EtapeEnum.Fox;
                lastStep = EtapeEnum.Fox;
                message = "Choisis un Apprenti à utiliser";
            }else{
                etape = EtapeEnum.God;
                lastStep = EtapeEnum.God;
                message = "Choisis un Dieu à utiliser";
            }
        }else{
            etape = EtapeEnum.Target;
            message = "Choisis un carte à cibler";
        }
        ChangeText(message);
    }

    public void ChangeText(string newText){
        TextMeshProUGUI rappelText = GetText(GetGameObject("RappelText"));
            rappelText.text = newText;
    }

}
