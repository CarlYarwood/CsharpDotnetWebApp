namespace API.Roles.Utils
{
    public class Sets
    {
        public static HashSet<string> UserTypes { get { return new HashSet<string> { UserType.Admin.ToString(), UserType.CareTeamMember.ToString() }; } }
        public static HashSet<string> RecommendationTypes { get{ return new HashSet<string> {RecommendationType.AllergyCheck.ToString(), RecommendationType.Screening.ToString(), RecommendationType.PerscriptionConsultation.ToString(), RecommendationType.Other.ToString() }; } }
    }
}