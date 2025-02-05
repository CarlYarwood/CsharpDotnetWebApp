namespace API.Roles
{
    public class RecommendationType {
        private RecommendationType(string value) { Value = value; }

        public string Value { get; private set; }

        public static RecommendationType AllergyCheck { get { return new RecommendationType("ALLERGY_CHECK"); } }
        public static RecommendationType Screening { get { return new RecommendationType("SCREENING"); } }
        public static RecommendationType FollowUp { get { return new RecommendationType("FOLLOW_UP"); } }
        public static RecommendationType PerscriptionConsultation { get { return new RecommendationType("PERSCRIPTION_CONSULTATION"); } }
        public static RecommendationType Other { get { return new RecommendationType("OTHER"); } }

    public override string ToString()
        {
            return Value;
        }
    }
}
