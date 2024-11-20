namespace GrpcHttpService.Domain.Entities
{
    /// <summary>
    /// Organization entity
    /// </summary>
    public class Organization : BaseEntity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        // List to manage user
        private readonly List<User> _users = new();

        /// <summary>
        /// Public read-only access to the associated users.
        /// </summary>
        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        /// <summary>
        /// Constructor for creating a new organization.
        /// </summary>
        /// <param name="name">Name of the organization.</param>
        /// <param name="address">Address of the organization.</param>
        public Organization(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be empty");

            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the organization's details.
        /// </summary>
        /// <param name="name">New name of the organization.</param>
        /// <param name="address">New address of the organization.</param>
        public void Update(string name, string address)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address cannot be empty");

            Name = name;
            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks the organization as deleted.
        /// </summary>
        public void SoftDelete()
        {
            if (DeletedAt.HasValue) throw new InvalidOperationException("Organization is already deleted");

            DeletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Restores a soft-deleted organization.
        /// </summary>
        public void Restore()
        {
            if (!DeletedAt.HasValue) throw new InvalidOperationException("Organization is not deleted");

            DeletedAt = null;
        }

        /// <summary>
        /// Associates a user with the organization.
        /// </summary>
        /// <param name="user">The user to associate with the organization.</param>
        public void AddUser(User user)
        {
            if (_users.Contains(user)) throw new InvalidOperationException("User is already associated with this organization");

            _users.Add(user);
        }

        /// <summary>
        /// Removes a user association from the organization.
        /// </summary>
        /// <param name="user">The user to disassociate from the organization.</param>
        public void RemoveUser(User user)
        {
            if (!_users.Contains(user)) throw new InvalidOperationException("User is not associated with this organization");

            _users.Remove(user);
        }
    }
}
