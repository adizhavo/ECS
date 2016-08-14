public interface IComponent
{
    Entity entity {set;get;}
    Matcher matcher {get;}
}

// Add new element when a new component is added
public enum Matcher
{
    // Move
    // Health
    // Attack
    // ecc...
}