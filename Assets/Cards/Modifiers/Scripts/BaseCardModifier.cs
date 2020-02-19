using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCardModifier : ScriptableObject
{

    [SerializeField] private string m_Title;
    public string Title { get => Title; }

    [SerializeField] [TextArea] private string m_Description;
    public string Description { get => Description; }

    [SerializeField] private Sprite m_Icon;
    public Sprite Icon { get => m_Icon; }

    protected int m_Amount;
    public int Amount { get => m_Amount; }

    public BaseCardModifier Clone()
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
