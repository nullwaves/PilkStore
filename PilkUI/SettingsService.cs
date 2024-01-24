namespace PilkUI
{
    public static class SettingsService
    {
        public static string GetPilkServer() => Preferences.Default.Get("server", "http://localhost:8000");

        public static void SetPilkServer(string server) => Preferences.Default.Set("server", server);
    }
}
