using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static MyExtensions;

public class Step : MonoBehaviour
{
    #region Initialisation
    public enum EtapeEnum{
        God,
        Fox,
        Target
    }

    private EtapeEnum lastStep = EtapeEnum.God;
    public EtapeEnum etape;
    #endregion

    #region Getters
    public EtapeEnum Etape{
        get { return etape; }
    }
    #endregion

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

    // Modifie le texte
    public void ChangeText(string newText){
        TextMeshProUGUI rappelText = GetText(GetGameObject("RappelText"));
            rappelText.text = newText;
    }

}
