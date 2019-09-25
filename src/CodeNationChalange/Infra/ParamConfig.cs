namespace CodeNationChalange.Infra
{
    public static class ParamConfig
    {
        public static string URL_BASE { get; set; } = "https://api.codenation.dev/v1/challenge/dev-ps/";
        public static string GetUrl { get; set; } = "generate-data?token=";
        public static string PostUrl { get; set; } = "submit-solution?token=";
        public static string TOKEN { get; set; } = "77ddccd043a5ded3e50e4dd2977533f4c88f5911";
    }
}
