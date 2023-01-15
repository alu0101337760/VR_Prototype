using System.IO;
using System;
using System.Reflection;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.ComponentModel;

namespace VR_Prototype
{
    public class PotionNameRecovery
    {
        private static string enumText = @"namespace VR_Prototype 
{
public enum PotionNames
    {
";

        private static void HandleTypeAssignation(List<Type> types)
        {
            PotionComponentsList potionComponents = Resources.Load<PotionComponentsList>("TestPotionList");
            List<PotionComponents> componentList = potionComponents.potionComponents;

            for (int i = 0; i < types.Count; i++)
            {
                bool broken = false;
                for (int j = 0; j < componentList.Count; j++)
                {
                    if (types[i].Name == componentList[j].potionName.ToString())
                    {
                        componentList[j] = new PotionComponents(componentList[j], types[i]);
                        broken = true;
                        break;
                    }
                }
                if (!broken)
                {
                    componentList.Add(new PotionComponents((PotionNames)Enum.Parse(typeof(PotionNames), types[i].Name), types[i]));
                }
            }
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
    }
}