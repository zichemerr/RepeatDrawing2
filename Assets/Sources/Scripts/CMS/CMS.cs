using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace LD
{
    /*public static class CMSMenuItems
    {
        [MenuItem("CMS/Reload")]
        public static void CMSReload()
        {
            CMS.Unload();
            CMS.Init();
        }
    }*/
    
    public static class CMS
    {
        private static List<CMSEntity> all = new List<CMSEntity>();
        static bool isInit;
        
        public static void Init()
        {
            if (isInit)
                return;
            isInit = true;
            
            var subs = ReflectionUtil.FindAllSubslasses<CMSEntity>();

            foreach (var subclass in subs)
            {
                CMSEntity entity = Activator.CreateInstance(subclass) as CMSEntity;

                if (entity.id == null)
                    entity.id = entity.GetType().Name;
                
                all.Add(entity);
            }
        }

        public static T Get<T>(string id = null) where T : CMSEntity
        {
            if (id == null)
                id = typeof(T).Name;

            foreach (var entity in all)
                if (entity.id == id)
                    return entity as T;
            
            throw new Exception("No entity found");
        }

        public static void Unload()
        {
            isInit = false;
            all = new List<CMSEntity>();
        }
    }
    
    public partial class CMSEntity
    {
        public string id;

        public List<EntityComponentDefinition> components = new List<EntityComponentDefinition>();
        
        public T Define<T>() where T : EntityComponentDefinition, new()
        {
            var t = Get<T>();
            
            if (t != null)
                return t;

            var entity_component = new T();
            components.Add(entity_component);
            return entity_component;
        }

        public bool Is<T>(out T unknown) where T : EntityComponentDefinition, new()
        {
            unknown = Get<T>();
            return unknown != null;
        }

        public bool Is<T>() where T : EntityComponentDefinition, new()
        {
            return Get<T>() != null;
        }

        public bool Is(Type type)
        {
            return components.Find(m => m.GetType() == type) != null;
        }

        public T Get<T>() where T : EntityComponentDefinition, new()
        {
            return components.Find(m => m is T) as T;
        }
    }

    public static class ReflectionUtil
    {
        public static Type[] FindAllSubslasses<T>()
        {
            Type baseType = typeof(T);
            Assembly assembly = Assembly.GetAssembly(baseType);

            Type[] types = assembly.GetTypes();
            Type[] subclasses = types.Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract).ToArray();

            return subclasses;
        }
    }

    [Serializable]
    public class EntityComponentDefinition
    {
        
    }
}
