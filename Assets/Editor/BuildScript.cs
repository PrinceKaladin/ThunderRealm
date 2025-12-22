using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "ThunderRealm.aab";
        string apkPath = "ThunderRealm.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 ="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";
        string keystorePass = "qqwwee";
        string keyAlias = "thunder0";
        string keyPass = "qqwwee";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
