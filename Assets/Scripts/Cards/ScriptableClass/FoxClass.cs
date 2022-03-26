using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using static MyExtensions;
using static Card;

[CreateAssetMenu(fileName="FoxCard", menuName="Assets/Fox")]
public class FoxClass : Card
{
    public FoxClass(){}
    public FoxClass(int Id, string Name, string Description) {
        id = Id;
        name = Name;
        description = Description;

        type = TypeEnum.Fox;
    }
}
