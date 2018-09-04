using Foundation;
using Newtonsoft.Json;
using TwitterSafari.Models;

namespace TwitterSafari.iOS.Services
{
    public class iOSSettings : ISettings
    {
        public int TweetCount
        {
            get { return (int)NSUserDefaults.StandardUserDefaults.IntForKey(nameof(TweetCount)); }
            set { NSUserDefaults.StandardUserDefaults.SetInt(value, nameof(TweetCount)); }
        }

        static T DeserializeSetting<T>(string key)
           where T : class
        {
            string json = NSUserDefaults.StandardUserDefaults.StringForKey(key);
            if (string.IsNullOrEmpty(json))
                return null;
            return JsonConvert.DeserializeObject<T>(json);
        }

        static void SerializeSetting(string key, object value)
        {
            if (value == null)
            {
                NSUserDefaults.StandardUserDefaults.SetString(string.Empty, key);
            }
            else
            {
                NSUserDefaults.StandardUserDefaults.SetString(JsonConvert.SerializeObject(value), key);
            }
        }

        public void Save()
        {
            NSUserDefaults.StandardUserDefaults.Synchronize();
        }
    }
}