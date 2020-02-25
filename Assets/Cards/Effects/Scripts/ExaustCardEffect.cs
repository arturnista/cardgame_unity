using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaustCardEffect : BaseCardEffect
{

    public ExaustCardEffect(List<EntityType> castLayer) : base(castLayer)
    {
    }

    public override void OnPlay(BaseCard card, List<Vector3> castPositions)
    {
        Debug.Log("Aqui?");
        card.Exaust();
    }

    public override string GetDescription()
    {
        return string.Format("Exaust");
    }

}
