using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifiersHolder
{

    Dictionary<Type, BaseModifier> Modifiers { get; }
    void AddModifier(BaseModifier modifier, int amount);
    
}
