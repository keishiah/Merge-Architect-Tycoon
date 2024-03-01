namespace _Scripts.Services.StaticDataService
{
    public static class StaticMethods
    {
        public static string FormatTimerText(int totalSeconds)
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            string formattedTime = $"{hours:00}:{minutes:00}:{seconds:00}";
            return formattedTime;
        }
    }
}