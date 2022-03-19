using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card;

[CreateAssetMenu(fileName = "GodCard", menuName = "Assets/God")]
public class GodClass : Card
{
    #region Initialisation
    public int level = 5;
     [TextArea]
    public string level1;
     [TextArea]
    public string level2;
     [TextArea]
    public string level3;
     [TextArea]
    public string level4;
     [TextArea]
    public string level5;
    [TextArea]
    public string disciple;

    /* Initialisation de la carte
    Fonction qui ajoute toute les données à la carte Dieu
    */
    public GodClass() {}
    public GodClass(int Id, string Name, string Description, string Level5, string Level4, string Level3, string Level2, string Level1, string Disciple, int Level) {
        id = Id;
        name = Name;
        description = Description;
        level = Level;
        level1 = Level1;
        level2 = Level2;
        level3 = Level3;
        level4 = Level4;
        level5 = Level5;
        disciple = Disciple;

        type = TypeEnum.God;
    }
    #endregion

    #region Getters
    public int Level {
        get {return level;}
    }
    public string Level1 {
        get {return level1;}
    }
    public string Level2 {
        get {return level2;}
    }
    public string Level3 {
        get {return level3;}
    }
    public string Level4 {
        get {return level4;}
    }
    public string Level5 {
        get {return level5;}
    }
    public string Disciple {
        get {return disciple;}
    }
    #endregion

    #region Modifications
    // Edite le niveau de la carte
    public void SetLevel(int lvl) {
        level = lvl;
    }
    #endregion

    
    #region Effects
    /*
       switch(cardUsed.Level){
            case 5:
                break;
            case 4:
                break;
            case 3:
                break;
            case 2:
                break;
            case 1:
                break;
            default:
                break;
        }*/
    public void SorciereEffect(Card target, GodClass cardUsed){
        if(target.Type == TypeEnum.God){
            GodClass god = target as GodClass;
            if(god.Level > cardUsed.Level){
                Debug.Log("La carte n'a pas pû être neutralisé");
            }
        }
        target.SetEffect(false);
    }

    

    // Effet de la voyante 
    // La couleur ne marche pas encore 
    // La carte n'est pas encor chissit au hasard pour le level 1
    public void VoyanteEffect(Card target, GodClass cardUsed){
        switch(cardUsed.Level){
            case 5:
                Debug.Log("La carte sélectionné est " + target.Name);
                break;
            case 4 :
                Debug.Log("La carte sélectionné est un " + target.Type);
                break;
            case 3 :
                    if(target.Type == TypeEnum.God){
                        Debug.Log("La carte sélectionné appartient à la regigion de  " + target.Name);
                    }else if(target.Type == TypeEnum.Fidele){
                        FideleClass fidele = target as FideleClass;
                        Debug.Log("La carte sélectionné appartient à la regigion de  " + fidele.God.Name);
                    }else{
                        Debug.Log("La carte sélectionné n'appartient à aucune religion");
                    }
                    break;
            case 0 : 
                    Debug.Log("La sorcière n'a pas réussit à utiliser son pouoir");
                    break;
            default :
                    Debug.Log("La carte sélectionné appartient à la religion de couleur " + target.Color);
                    break;
        }
    }
    
    #endregion
}