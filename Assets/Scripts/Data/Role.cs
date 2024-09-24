

namespace Data
{
    public enum RoleType
    {
        Environment,
        Society,
        Technology,
        Economy
    }

    public class Role
    {
        public string Name;
        public string Description;

        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}