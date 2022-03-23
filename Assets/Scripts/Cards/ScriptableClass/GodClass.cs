using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Effet de la sorcière 
    // Neutralise les dieux inférieur ou égal à son niveau
    // Neutralise toutes autre cartes à effet
    // Renvoie un message si aucune des conditions n'est remplies 

    //Vérifier quel village on vise
    public void EnchanteresseEffect(Card target, GameObject cardInGame){
        // Prédéfinit le message d'erreur à envoyer
        string message = "La carte n'a pas pû être neutralisé car elle ne possède aucun pouvoir ou est trop forte pour vote sorcière.";
        if(target.Name != "Fidèle"){
            if(target.Type == TypeEnum.God){
                GodClass god = target as GodClass;
                if(god.Level > this.Level){
                    Return(message);
                }
            }
            target.SetEffect(false, cardInGame);

            // Modifie aussi la carte dans la main du joueur
            UIManagerField playerVisual = GameObject.Find("PlayerVisual(Clone)").transform.GetComponent<UIManagerField>();
            playerVisual.UpdateHand(target, "effect");
        }else{
            Return(message);
        }
    }

    // Effet de la voyante 
    // 
    //
    // La couleur ne marche pas encore (ressort le RGB)
    // La carte n'est pas encor choissit au hasard pour le level 1
    public void DivinatriceEffect(Card target, GameObject cardInGame){
        switch(this.Level){
            case >= 5 :
                Return("La carte sélectionné est " + target.Name);
                break;
            case 4 :
                Return("La carte sélectionné est un " + target.Type);
                break;
            case 3 :
                if(target.Type == TypeEnum.God){
                    Return("La carte sélectionné appartient à la regigion de  " + target.Name);
                }else if(target.Type == TypeEnum.Fidele){
                    FideleClass fidele = target as FideleClass;
                    Return("La carte sélectionné appartient à la regigion de  " + fidele.God.Name);
                }else{
                    Return("La carte sélectionné n'appartient à aucune religion");
                }
                break;
            case 0 : 
                Return("La sorcière n'a pas réussit à utiliser son pouvoir");
                break;
            default :
                Return("La carte sélectionné appartient à la religion de couleur " + target.Color );
                break;
        }
    }

    // Effet de la gardienne
    // Protège une ou plusieur cartes d'une même religion d'une attaque d'un apprenti
    //
    // Ajouter un effet visuel sur la carte
    // Manque l'effet au niveau 5 
    // Indiquer au joueur le possedant que le bouclier a sauté 
    public void GardienneEffect(Card card){
        switch(this.Level){
            case >= 5:/*
                List<Card> protectedCard = deck.FindAll(delegate(Card x){return x.Color == card.Color;});
                foreach(Card nonProtectedCard in protectedCard){
                    nonProtectedCard.SetProtection(true);
                }*/
                break;
            case 3:
                card.SetProtection(Protection.OneTime);
                break;
            case 0 :
                Return("La Gardienne n'a pas réussit à utiliser son pouvoir");
                break;
            default :
                    card.SetProtection(Protection.Protected);
                break;
        }
    }


    public void NecromancienEffect(){}
    public void InformateurEffect(){}
    public void LeaderEffect(){}
    public void MetamorpheEffect(Card target, GameObject cardInGame){
        if(target.Type == TypeEnum.God && this.Level == 5){
            name = target.Name;
        }
  }
    public void ProtecteurEffect(){}
    public void GiletEffect(){}
    public void EnsorceleuseEffect(){}
    public void EnfantEffect(){}
    public void ImitatriceEffect(){}
    public void ContagionEffect(){}
    public void SatanisteEffect(){}
    
    #endregion

    private void Return(string message){
        Debug.Log(message);
    }
}