namespace Aurora.User.Core
{
    public abstract class EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}