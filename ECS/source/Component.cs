namespace ECS
{
	// Components are small operations but mostly they contain only data.
    public interface IComponent
    {
        Entity entity { set; get; }

		// Different classes can have same matchers
        Matcher matcher { get; }
    }

    // Add new type per component
    public enum Matcher
    {
         Move,
         Health
        // Attack
		// OtherComponent
        // ecc...
    }
}