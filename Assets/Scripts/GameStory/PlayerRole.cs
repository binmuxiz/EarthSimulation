namespace GameStory
{
    public class PlayerRole
    {
        private string name;
        private string role;
        private string description;
        private bool isAssigned;

        public PlayerRole(string name, string role, string description)
        {
            this.name = name;
            this.role = role;
            this.description = description;
            isAssigned = false;
        }
    }
}