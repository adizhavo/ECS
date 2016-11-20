# ECS

|Languages|
---
|[C++](https://github.com/adizhavo/ECS)|
|[C#](https://github.com/adizhavo/ECS_Cpp)|

ECS is a simple [entity component system](https://en.wikipedia.org/wiki/Entity_component_system) framework.
It uses generics to achieve composition without having the need to write new code inside the container class which is called entity.

The class diagram below will make easier to understand how the system works. 

<div style="text-align:center"><img src ="/Diagrams/ECS_ClassDiagram.png?raw=true" /></div>


As said before, an entity is simply composed by small chunks of data or behaviours. 
Systems are listeners and they are ready to reach any modified entity which matches the defined filter of components.

# How it works

### Components
To create a component just implement ```IComponent``` interface :

```C#
public class Position : IComponent
{
  #region IComponent implementation
  public Entity { set; get; }
  #endregion
  
  public float x, y, z;
}
```

### Entities
Adding or removing component from an entity is event easier :

```C#
Entity rock = new Entity();

// Every add or replace component will notify systems
rock.AddComponent(new Position())
    .AddComponent(new Rigidbody());

rock.GetComponent<Position>().x += 10f;

rock.RemoveComponent<Position>();
```

### Filters
Filters are really useful to match or query specific group of entities.
They hold information about which components an entity should or shouldn't have.

```C#
// create filter
Filter rockFilter = new Filter().AllOf(typeOf(Position), typeOf(Rigidbody))
                                .NoneOf(typeOf(Health));

bool doesMatch = rock.DoesMatchFilter(rockFilter);
```

### Entity Matcher
When an entity is created, it will automatically subscribe itself to the entity matcher and unsubscribe once destroyed.
The entity matcher is a pool of entities and is possible to filter specific groups;

```C#
HashSet<Entity> rocks = EntityMatcher.GetMatchedEntities(rockFilter);

// perform some operations at will
foreach (Entity r in rock)
  r.GetComponent<Position>().x ++;
```
### Reactive Systems
Systems are simply listeners to new components added or replaced to an entity.
Implement ```IReactiveSystem``` interface to create one.

```C#
public class RockPlacement : IReactiveSystem
{
  #region IReactiveSystem implementation
  public filterMatch 
  {
    get { return new Filter().AllOf(typeOf(Position), typeOf(Rigidbody)); }
  }
  
  public void Execute(Entity rock)
  {
    rock.GetComponent<Position>().x = 320f; 
    rock.GetComponent<Position>().z = 100f; 
  }
  #endregion
}
```
# Conclusion
These are very simple operations, to better understand the system and what else is possible i suggest to have a look at the [test cases](/ECSTests/) of the project

