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
        PlayerEntity playerEntity = GameObject.FindObjectOfType<PlayerEntity>();
    }

    public override string GetDescription()
    {
        return string.Format("Exaust");
    }

}
