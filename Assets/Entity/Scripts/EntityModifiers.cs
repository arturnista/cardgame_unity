using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModifiers : MonoBehaviour, IModifiersHolder
{

    public delegate void ChangeModifiersHandler(GameObject enemy);
    public ChangeModifiersHandler OnChangeModifiers;

    private Dictionary<Type, BaseCardModifier> _modifiers;
    public Dictionary<Type, BaseCardModifier> Modifiers { get => _modifiers; }

    void Awake()
    {
        _modifiers = new Dictionary<Type, BaseCardModifier>();
    }

    public void AddModifier(BaseCardModifier modifier, int amount)
    {
        Type key = modifier.GetType();
        if (_modifiers.ContainsKey(key))
        {
            _modifiers[key].AddAmount(amount);
        }
        else
        {
            BaseCardModifier mod = modifier.Clone();
            mod.Construct(amount);
            _modifiers.Add(mod.GetType(), mod);
        }

        if (OnChangeModifiers != null)
        {
            OnChangeModifiers(gameObject);
        }
    }

    public void StartTurn()
    {
        Dictionary<Type, BaseCardModifier> modifiersAux = new Dictionary<Type, BaseCardModifier>(_modifiers);
        foreach (var key in modifiersAux.Keys)
        {
            bool finished = modifiersAux[key].OnStartTurn(gameObject);
            if (finished)
            {
                _modifiers.Remove(key);
            }
        }

        if (OnChangeModifiers != null)
        {
            OnChangeModifiers(gameObject);
        }
    }

    public void EndTurn()
    {
        Dictionary<Type, BaseCardModifier> modifiersAux = new Dictionary<Type, BaseCardModifier>(_modifiers);
        foreach (var key in modifiersAux.Keys)
        {
            bool finished = modifiersAux[key].OnEndTurn(gameObject);
            if (finished)
            {
                _modifiers.Remove(key);
            }
        }

        if (OnChangeModifiers != null)
        {
            OnChangeModifiers(gameObject);
        }
    }

}
