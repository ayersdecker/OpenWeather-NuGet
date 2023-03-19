using Newtonsoft.Json;

namespace OpenWeatherNuGet;

public class Call
{

    public static async Task<dynamic> OpenWeather(string ApiKey)
    {

        // Get Location from device (Latitude and Longitude) using manifest
        var location = await Geolocation.GetLocationAsync();
        double latitude = location.Latitude;
        double longitude = location.Longitude;

        // Key and URL Build (Imperial)
        string key = ApiKey;
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={key}&units=imperial";

        // Build and Connect to OpenWeather API
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (platform; rv:geckoversion) Gecko/geckotrail Firefox/firefoxversion");
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        // Get JSON Data
        string content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<dynamic>(content);
    
    }

    public static ImageSource OpenWeatherIconSource(dynamic content)
    {
        // Uses the dynamic provided to return image source at specific part of content's json tree
        return $"https://openweathermap.org/img/wn/{content.weather[0].icon.ToString()}.png";
    }
        

}

