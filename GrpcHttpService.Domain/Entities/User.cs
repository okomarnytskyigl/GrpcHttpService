namespace GrpcHttpService.Domain.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }

        // List to manage organization
        private readonly List<Organization> _organizations = new();

        /// <summary>
        /// Public read-only access to the associated organizations.
        /// </summary>
        public IReadOnlyCollection<Organization> Organizations => _organizations.AsReadOnly();

        /// <summary>
        /// Constructor for creating a new user.
        /// </summary>
        /// <param name="name">Full name of the user.</param>
        /// <param name="username">Unique username of the user.</param>
        /// <param name="email">Email address of the user.</param>
        public User(string name, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email");

            Id = Guid.NewGuid();
            Name = name;
            Username = username;
            Email = email;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates the user's details.
        /// </summary>
        /// <param name="name">New name of the user.</param>
        /// <param name="username">New username of the user.</param>
        /// <param name="email">New email address of the user.</param>
        public void Update(string name, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email");

            Name = name;
            Username = username;
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks the user as deleted.
        /// </summary>
        public void SoftDelete()
        {
            if (DeletedAt.HasValue) throw new InvalidOperationException("User is already deleted");

            DeletedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Restores a soft-deleted user.
        /// </summary>
        public void Restore()
        {
            if (!DeletedAt.HasValue) throw new InvalidOperationException("User is not deleted");

            DeletedAt = null;
        }

        /// <summary>
        /// Associates the user with an organization.
        /// </summary>
        /// <param name="organization">The organization to associate with the user.</param>
        public void JoinOrganization(Organization organization)
        {
            if (_organizations.Contains(organization)) throw new InvalidOperationException("Already associated with this organization");

            _organizations.Add(organization);
        }

        /// <summary>
        /// Removes the user's association with an organization.
        /// </summary>
        /// <param name="organization">The organization to disassociate from the user.</param>
        public void LeaveOrganization(Organization organization)
        {
            if (!_organizations.Contains(organization)) throw new InvalidOperationException("Not associated with this organization");

            _organizations.Remove(organization);
        }
    }
}
