using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseLogger
{
    private FirebaseApp _app;

    public UniTask InitializeFirebase()
    {
        return FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                _app = FirebaseApp.DefaultInstance;
                Debug.Log("Firebase has initialized successfully.");
            }
            else
            {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        }).AsUniTask();
    }

    public void LogEvent(string eventName)
    {
        FirebaseAnalytics.LogEvent(eventName);
        Debug.Log("Event " + eventName + " has been logged.");
    }

    public void LogEventWithParameters(string eventName, params Parameter[] parameters)
    {
        FirebaseAnalytics.LogEvent(eventName, parameters);
    }
}