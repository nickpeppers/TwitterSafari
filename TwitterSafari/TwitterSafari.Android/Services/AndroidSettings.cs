using Android.Content;
using Android.Preferences;
using Newtonsoft.Json;
using TwitterSafari.Models;

namespace TwitterSafari.Droid.Services
{
    public class AndroidSettings : ISettings
    {
        readonly Context _context;
        ISharedPreferences _preferences;
        ISharedPreferencesEditor _editor;

        public AndroidSettings(Context context)
        {
            _context = context;
        }

        public int TweetCount
        {
            get { return GetInt(nameof(TweetCount)); }
            set { Put(nameof(TweetCount), value); }
        }

        public void Save()
        {
            //Commit changes and dispose
            if (_editor != null)
            {
                _editor.Commit();
                _editor.Dispose();
                _editor = null;
            }
            if (_preferences != null)
            {
                _preferences.Dispose();
                _preferences = null;
            }
        }

        T GetObject<T>(string key) where T : class
        {
            //Use a local instance of preferences for "Get"
            using (var preferences = PreferenceManager.GetDefaultSharedPreferences(_context))
            {
                string json = preferences.GetString(key, string.Empty);
                if (string.IsNullOrEmpty(json))
                    return null;
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        string GetString(string key)
        {
            //Use a local instance of preferences for "Get"
            using (var preferences = PreferenceManager.GetDefaultSharedPreferences(_context))
            {
                return preferences.GetString(key, string.Empty);
            }
        }

        bool GetBool(string key)
        {
            //Use a local instance of preferences for "Get"
            using (var preferences = PreferenceManager.GetDefaultSharedPreferences(_context))
            {
                return preferences.GetBoolean(key, false);
            }
        }

        int GetInt(string key)
        {
            //Use a local instance of preferences for "Get"
            using (var preferences = PreferenceManager.GetDefaultSharedPreferences(_context))
            {
                return preferences.GetInt(key, 0);
            }
        }

        void Put(string key, object value)
        {
            CheckPreferences();

            _editor.PutString(key, JsonConvert.SerializeObject(value));
            _editor.Commit();
        }

        void Put(string key, string value)
        {
            CheckPreferences();

            _editor.PutString(key, value);
            _editor.Commit();
        }

        void Put(string key, bool value)
        {
            CheckPreferences();

            _editor.PutBoolean(key, value);
            _editor.Commit();
        }

        void Put(string key, int value)
        {
            CheckPreferences();

            _editor.PutInt(key, value);
            _editor.Commit();
        }

        void CheckPreferences()
        {
            //Create preferences & editor if needed
            if (_preferences == null)
                _preferences = PreferenceManager.GetDefaultSharedPreferences(_context);
            if (_editor == null)
                _editor = _preferences.Edit();
        }
    }
}