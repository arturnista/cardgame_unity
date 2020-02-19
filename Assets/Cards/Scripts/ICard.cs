
public interface ICard
{

    string Title { get; }
    string Description { get; }
    int ManaCost { get; }
    bool HasTarget { get; }
    
    void Play(IHealth enemy);

}
