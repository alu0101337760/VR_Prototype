using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PotionNameRecovery
{
    private static string enumText = @"namespace VR_Prototype 
{
public enum PotionNames
    {
";


    [InitializeOnEnterPlayMode]
    public static void RecoverPotionNames(EnterPlayModeOptions options)
    {
        string dirName = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        dirName += "\\VR_Prototype\\Assets\\Scripts\\Weapons\\Potions\\PotionBehaviours";
        DirectoryInfo directory = new DirectoryInfo(dirName);
        FileInfo[] files = directory.GetFiles("*.cs");

        foreach (FileInfo file in files)
        {
            if (file.Name != "NameFetcher.cs")
            {
                enumText += "\t\t" + file.Name.Replace(".cs", "") + ",\n";
            }
        }

        enumText = enumText.Remove(enumText.Length - 1);
        enumText += @"
    }
}
";
        string targetDir = dirName.Replace("PotionBehaviours", "PotionNames");

        File.WriteAllText(targetDir + "\\PotionNames.cs", enumText);

    }

}