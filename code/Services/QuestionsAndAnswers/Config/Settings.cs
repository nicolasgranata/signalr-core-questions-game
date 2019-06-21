namespace QuestionsAndAnswers.Config
{
    public class Settings
    {
        public string HubName { get; set; }

        public CorsSettings CorsSettings {get; set; }
    }

    public class CorsSettings
    {
        public string[] Origins { get; set; }
    }     
}
