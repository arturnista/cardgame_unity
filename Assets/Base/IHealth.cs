public interface IHealth
{
    
    int Health { get; }
    int Armor { get; set; }

    void DealDamage(Damage damage);

}
