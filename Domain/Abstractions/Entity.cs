namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;


public abstract class Entity
{
    public Guid Id { get; init; } 
    //Lista de eventos que luego seran publicados para que los handlers los capturen y se suscriban a ellos.
    private readonly List<IDomainEvent> _domainEvents = new();
    
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    //agregamos los metodos correspondientes al patron publish-suscriber
    
    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.ToList();
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}