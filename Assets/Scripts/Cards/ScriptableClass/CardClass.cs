using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Edite la couleur de la carte 
    public void SetColor(Color coul) {
        color = coul;
    }
    
    // remonter a la description pour recuperer aussi l'meplacemetn sur le terre le gameobject
    public void SetEffect(bool newEffect){  
        if(newEffect == false){
            //le gameobject
            //cardInGame.SetColor(new Color(180, 180 , 180 ,255));
        }
    }

    // Supprime la carte du village
    private void Die() {}
}
