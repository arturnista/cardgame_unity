using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifiersHolder
{

    Dictionary<Type, BaseCardModifier> Modifiers { get; }
    void AddModifier(BaseCardModifier modifier, int amount);
    
}
