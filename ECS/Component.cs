namespace ECS
{
	// Components are small operations but mostly they contain only data.
    public interface IComponent
    {
        Entity entity { set; get; }
    }
}