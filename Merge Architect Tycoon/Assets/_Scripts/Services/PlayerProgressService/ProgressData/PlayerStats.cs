using System;
using System.Globalization;

[Serializable]
public class PlayerStats
{
    public string quitDateTime;

    public void SaveDateTime()
    {
        quitDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
    }

    public int GetAfkTime()
    {
        if (string.IsNullOrEmpty(quitDateTime))
            return 0;

        DateTime quitTime = DateTime.Parse(quitDateTime, CultureInfo.InvariantCulture);
        return (int)(DateTime.Now - quitTime).TotalSeconds;
    }
}