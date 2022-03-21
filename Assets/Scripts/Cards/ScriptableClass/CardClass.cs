using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : ScriptableObject {
    #region Initialisation
    public enum TypeEnum {
        God,
        Fidele,
        Fox
    }

    public int id;
    public string name;
    [TextArea]
    public string description;
    public TypeEnum type;

    public bool effect = true;
    public Color color = Color.white;

    public bool isProtected = false;

    /* Initialisation de la carte
    Fonction qui donne toutes les informations à la carte
    */
    public Card(){}
    public Card(int Id, string Name, string Description, TypeEnum Type) {
        id = Id;
        name = Name;
        description = Description;
        type = Type;
    }
    #endregion

    #region Getters
    public bool Protected{
        get {return isProtected;}
    }
    public int Id {
        get {return id;}
    }
    public string Name {
        get {return name;}
    }
    public string Description {
        get {return description;}
    }
    public bool Effect {
        get {return effect;}
    }
    public TypeEnum Type {
        get {return type;}
    }
    public Color Color {
        get {return color;}
    }
    #endregion

    // Edite la protection de la carte
    public void SetProtection(bool protection){
        isProtected = protection;
    }
    // Edite la couleur de la carte 
    public void SetColor(Color coul) {
        color = coul;
    }
    
    // Modifie la couleur de la carte reçu en gris foncé
    public void SetEffect(bool newEffect, GameObject cardInGame){  
        if(newEffect == false){
            RawImage image = cardInGame.transform.GetComponent<RawImage>();
            image.color = new Color32(90,90,90,255);
        }
    }
}
