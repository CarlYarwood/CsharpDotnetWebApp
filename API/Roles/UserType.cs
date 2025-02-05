namespace API.Roles
{
    public class UserType
    {
        private UserType(string value) { Value = value; }
        public string Value { get; private set; }

        public static UserType Admin { get { return new UserType("ADMIN"); } }
        public static UserType CareTeamMember { get { return new UserType("CARE_TEAM_MEMBER"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}
