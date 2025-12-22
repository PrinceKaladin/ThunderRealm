using System.Linq;
using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

public static class BuildScript
{
    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        // Включаем сборку App Bundle (Google Play)
        EditorUserBuildSettings.buildAppBundle = true;

        // Устанавливаем Bundle Version Code из переменной окружения Appcircle (если нужно)
        // В Appcircle можно задать переменную, например AC_ANDROID_VERSION_CODE
        string versionCodeStr = Environment.GetEnvironmentVariable("AC_ANDROID_VERSION_CODE");
        if (int.TryParse(versionCodeStr, out int versionCode))
        {
            Debug.Log($"Bundle version code set to {versionCode}");
            PlayerSettings.Android.bundleVersionCode = versionCode;
        }
        else
        {
            Debug.Log("Android version code not provided or invalid – using default from Player Settings");
        }

        // Создаём папку для вывода, если её нет (на всякий случай)
        System.IO.Directory.CreateDirectory("android");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "android/android.aab",  // Фиксированный путь, который потом копируется в $AC_OUTPUT_DIR
            target = BuildTarget.Android,
            options = BuildOptions.None,
            scenes = GetScenes()
        };

        Debug.Log("Starting Android build (AAB)...");
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Android build completed successfully!");
        }
        else
        {
            Debug.LogError("Android build failed!");
            EditorApplication.Exit(1); // Завершаем с ошибкой, чтобы CI увидел фейл
        }
    }

    [MenuItem("Build/Build iOS")]
    public static void BuildIos()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "ios",
            target = BuildTarget.iOS,
            options = BuildOptions.None,
            scenes = GetScenes()
        };

        Debug.Log("Building iOS");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Built iOS");
    }

    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "win/" + Application.productName + ".exe",
            target = BuildTarget.StandaloneWindows64, // Рекомендуется явно указывать 64-bit
            options = BuildOptions.None,
            scenes = GetScenes()
        };

        Debug.Log("Building Windows");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Built Windows");
    }

    [MenuItem("Build/Build Mac")]
    public static void BuildMac()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
        {
            locationPathName = "mac/" + Application.productName + ".app",
            target = BuildTarget.StandaloneOSX,
            options = BuildOptions.None,
            scenes = GetScenes()
        };

        Debug.Log("Building StandaloneOSX");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
        Debug.Log("Built StandaloneOSX");
    }

    private static string[] GetScenes()
    {
        return EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();
    }
}