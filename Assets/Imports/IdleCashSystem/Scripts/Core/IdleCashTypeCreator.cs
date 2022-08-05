using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace IdleCashSystem.Core
{
    //[CreateAssetMenu(menuName = MenuName)]
    public class IdleCashTypeCreator : ScriptableObject
    {
        private const string MenuName = "My Creators/" + nameof(IdleCashTypeCreator);
        private const string CreatorAssetPath = "Creators";
        private const string BlankType = "";
        
        
        [SerializeField] private string[] realTypes;
        [SerializeField] private string[] letters;
        [SerializeField] private IdleCashTypeCreationMode creationMode;


        private static IdleCashTypeCreator m_Instance;

        private static IdleCashTypeCreator Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    var instances = Resources.LoadAll<IdleCashTypeCreator>(CreatorAssetPath);

                    if (instances.Length == 0)
                    {
                        throw new NullReferenceException($"{nameof(IdleCashTypeCreator)} cannot be found in Resources folders! Please Create one from {MenuName}.");
                    }

                    m_Instance = instances[0];
                }

                return m_Instance;
            }
        }
        
        
        private List<string> m_Types;

        private static List<string> Types
        {
            get
            {
                if (Instance.m_Types == null)
                {
                    CreateTypes();
                }

                return Instance.m_Types;
            }
        }

        public static string FirstType => Types[0];

        public static string LastType => Types.Last();


        private void OnValidate()
        {
            CreateTypes();
        }


        public static bool IsValidType(string type)
        {
            return Types.Contains(type);
        }

        public static int GetTypeIndex(string type)
        {
            return Types.IndexOf(type);
        }

        public static string GetNextType(string type)
        {
            var typeIndex = GetTypeIndex(type);
            var nextIndex = typeIndex + 1;
            return nextIndex >= Types.Count ? null : Types[nextIndex];
        }
        
        public static string GetPreviousType(string type)
        {
            var typeIndex = GetTypeIndex(type);
            var previousIndex = typeIndex - 1;
            return previousIndex < 0 ? null : Types[previousIndex];
        }
        
        
        private static void CreateTypes()
        {
            Instance.m_Types = new List<string>();
            
            var creationMode = Instance.creationMode;

            if (creationMode.Check(IdleCashTypeCreationMode.Blank))
            {
                CreateBlankType();
            }

            if (creationMode.Check(IdleCashTypeCreationMode.Reals))
            {
                CreateRealTypes();
            }

            if (creationMode.Check(IdleCashTypeCreationMode.SingleLetters))
            {
                CreateSingleLetterTypes();
            }
            
            if (creationMode.Check(IdleCashTypeCreationMode.DoubleLetters))
            {
                CreateDoubleLetterTypes();
            }
        }

        private static void CreateBlankType()
        {
            Instance.m_Types.Add(BlankType);
        }

        private static void CreateRealTypes()
        {
            foreach (var realType in Instance.realTypes)
            {
                Instance.m_Types.Add(realType);
            }
        }

        private static void CreateSingleLetterTypes()
        {
            foreach (var letter in Instance.letters)
            {
                Instance.m_Types.Add(letter);
            }
        }
        
        private static void CreateDoubleLetterTypes()
        {
            var letters = Instance.letters;
            
            foreach (var firstLetter in letters)
            {
                foreach (var secondLetter in letters)
                {
                    var newDoubleLetterType = firstLetter + secondLetter;
                    Instance.m_Types.Add(newDoubleLetterType);
                }
            }
        }
    }

    [Flags]
    public enum IdleCashTypeCreationMode
    {
        Blank = 1 << 0,
        Reals = 1 << 1,
        SingleLetters = 1 << 2,
        DoubleLetters = 1 << 3
    }

    public static class IdleCashTypeCreationModeExtensions
    {
        public static bool Check(this IdleCashTypeCreationMode creationMode, IdleCashTypeCreationMode otherCreationMode)
        {
            return (creationMode & otherCreationMode) == otherCreationMode;
        }
    }
}
