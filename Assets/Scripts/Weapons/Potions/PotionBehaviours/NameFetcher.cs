using System.IO;
using System.Reflection;
using UnityEditor;

public class PotionNameRecovery : Editor
{
    private string enumText = @"namespace VR_Prototype 
{
public enum PotionNames
    {\n";


    [InitializeOnEnterPlayMode]
    public void RecoverPotionNames()
    {
        string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        DirectoryInfo directory = new DirectoryInfo(dirName);
        FileInfo[] files = directory.GetFiles("*.cs");

        foreach (FileInfo file in files)
        {
            enumText += file.Name.Replace(".cs", "") + ",\n";
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