using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace UnityScriptTemplates
{

    /// <summary>
    /// This file adds a menu item just below "Create->C# Script" for creating some different script templates.
    /// You can add your own templates by copy-pasting the code in this file and editing the paths.a
    /// </summary>
    public static class CreateCustomClass
    {
        /// <summary>
        /// Path to the template file for a Card.
        /// Using .cs.txt extension to match Unity's built-in templates, but this is _not_ neccessary.
        /// </summary>
        private static string TemplatePathCard
        {
            get { return Application.dataPath + "/Editor/ScriptTemplates/CardTemplate.cs.txt"; }
        }

        /// <summary>
        /// ProjectWindowUtil.CreateScriptAsset is the method that makes the magic happen.
        /// It has two parameters:
        /// - templatePath, the absolute path to the template file.
        /// - destName, the suggested file name for the new asset.
        ///
        /// It seems like this method is usually called from c++; it's a private method, and nothing in ProjectWindowUtil calls it, but if you add a breakpoint
        /// in it when hitting Create-C# script, the breakpoint is hit, with no stack trace.
        /// </summary>
        private static MethodInfo CreateScriptAsset
        {
            get
            {
                var projectWindowUtilType = typeof(ProjectWindowUtil);
                return projectWindowUtilType.GetMethod("CreateScriptAsset", BindingFlags.NonPublic | BindingFlags.Static);
            }
        }

        /// <summary>
        /// Adds a menu item for creating a new Card file.
        /// </summary>
        [MenuItem("Assets/Create/C# Script Template/Card", priority = 81)] // Create/C# Script has priority 80, so this puts it just below that.
        public static void CreateCard()
        {
            CreateScriptAsset.Invoke(null, new object[] { TemplatePathCard, "NewCard.cs" });
        }

        /// <summary>
        /// Validates that the Card template exists.
        /// It's pretty important to check that the template path exist. If you call this method with an empty template Unity creates an invisible asset you cannot
        /// interact with, which will stick around until you restart Unity. 
        /// </summary>
        [MenuItem("Assets/Create/C# Script Template/Card", true, priority = 81)]
        public static bool CreateCardValidate()
        {
            return File.Exists(TemplatePathCard) && CreateScriptAsset != null;
        }
        
    }
}