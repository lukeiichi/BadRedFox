using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FideleCard", menuName = "Assets/Fidele")]
public class FideleClass : Card
{
    public GodClass god;

    public GodClass God {
        get {return god;}
    }

    public FideleClass() {}
    public FideleClass(int Id, string Name, string Description) {
        id = Id;
        name = Name;
        description = Description; 

        type = TypeEnum.Fidele;
    }

    public void SetGod(GodClass newGod) {
        Debug.Log("Il croira en : " + newGod);
        god = newGod;
    }
}
