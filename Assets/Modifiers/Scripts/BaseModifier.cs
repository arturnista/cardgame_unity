using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModifier : ScriptableObject
{

    [SerializeField] private string m_Title = default;
    public string Title { get => m_Title; }

    [SerializeField] [TextArea] private string m_Description = default;
    public string Description { get => m_Description; }

    [SerializeField] private Sprite m_Icon = default;
    public Sprite Icon { get => m_Icon; }

    protected int m_Amount;
    public int Amount { get => m_Amount; }

    public BaseModifier Clone()
    {
        return ScriptableObject.Instantiate(this);
    }

    public void Construct()
    {
        Construct(1);
    }

    public void Construct(int initialAmount)
    {
        m_Amount = initialAmount;
    }

    public void AddAmount(int amount)
    {
        m_Amount += amount;
    }

    public virtual int ModifyTakeDamage(int damage)
    {
        return damage;
    }

    public virtual int ModifyDealDamage(int damage)
    {
        return damage;
    }

    public virtual int ModifyBlock(int block)
    {
        return block;
    }

    public virtual bool OnStartTurn(GameObject self)
    {
        return false;
    }

    public virtual bool OnEndTurn(GameObject self)
    {
        return false;
    }

}
