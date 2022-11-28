using System.IO;
using System;
using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace VR_Prototype
{
    public class PotionNameRecovery
    {
        private static string enumText = @"namespace VR_Prototype 
{
public enum PotionNames
    {
";


        private static void HandleTypeAssignation(string potName)
        {
            PotionComponentsList potionComponents = Resources.Load<PotionComponentsList>("TestPotionList");
            List<PotionComponents> componentList = potionComponents.potionComponents;
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i].potionName.ToString() == potName)
                {
                    System.Type testType = System.Type.GetType(potName);
                    componentList[i] = new PotionComponents(componentList[i], System.Type.GetType(potName));
                    return;
                }
            }
            Type testtype = Type.GetType(potName);
            componentList.Add(new PotionComponents((PotionNames)Enum.Parse(typeof(PotionNames), potName), Type.GetType(potName)));

        }

        private static void HandleTypeAssignation(List<Type> type)
        {
            PotionComponentsList potionComponents = Resources.Load<PotionComponentsList>("TestPotionList");
            List<PotionComponents> componentList = potionComponents.potionComponents;

        }


        [InitializeOnEnterPlayMode]
        public static void RecoverPotionNamesFromAssembly(EnterPlayModeOptions opt)
        {
            List<Type> potionTypes = new List<Type>();
            foreach (Type type in Assembly.GetAssembly(typeof(Potion)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Potion))))
            {
                potionTypes.Add(type);

                enumText += "\t\t" + type.Name + ",\n";
            }


            enumText = enumText.Remove(enumText.Length - 1);
            enumText += @"
    }
}
";
            string dirName = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            dirName += "\\VR_Prototype\\Assets\\Scripts\\Weapons\\Potions\\PotionNames";
            File.WriteAllText(dirName + "\\PotionNames.cs", enumText);


            HandleTypeAssignation(potionTypes);

        }

        //        public static void RecoverPotionNames(EnterPlayModeOptions options)
        //        {



        //            string dirName = Path.GetDirectoryName(Directory.GetCurrentDirectory());
        //            dirName += "\\VR_Prototype\\Assets\\Scripts\\Weapons\\Potions\\PotionBehaviours";
        //            DirectoryInfo directory = new DirectoryInfo(dirName);
        //            FileInfo[] files = directory.GetFiles("*.cs");

        //            foreach (FileInfo file in files)
        //            {
        //                string potName = "";
        //                if (file.Name != "NameFetcher.cs")
        //                {
        //                    potName = file.Name.Replace(".cs", ""); 
        //                    enumText += "\t\t" + potName + ",\n";
        //                }
        //            }

        //            enumText = enumText.Remove(enumText.Length - 1);
        //            enumText += @"
        //    }
        //}
        //";
        //            string targetDir = dirName.Replace("PotionBehaviours", "PotionNames");

        //            File.WriteAllText(targetDir + "\\PotionNames.cs", enumText);

        //            foreach (string name in Enum.GetNames(typeof(PotionNames)))
        //            {
        //                HandleTypeAssignation(name);
        //            }
        //        }

    }
}