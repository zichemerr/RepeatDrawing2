using System;
using System.Collections.Generic;

namespace LD.Locator
{
    public class ServiceLocator<T> : IServiceLocator<T>
    {
        protected Dictionary<Type, T> _itemsMap { get; }

        public ServiceLocator()
        {
            _itemsMap = new Dictionary<Type, T>();
        }

        public TP Get<TP>() where TP : T
        {
            var type = typeof(TP);

            if (_itemsMap.ContainsKey(type) == false)
            {
                throw new Exception($"Class dont find {type.Name}");
            }

            return (TP)_itemsMap[type];
        }

        public TP Register<TP>(TP service) where TP : T
        {
            var type = service.GetType();

            if (_itemsMap.ContainsKey(type))
            {
                throw new Exception($"Second register error {type.Name}");
            }

            _itemsMap[type] = service;

            return service;
        }

        public void UnRegister<TP>(TP service) where TP : T
        {
            var type = service.GetType();

            if (_itemsMap.ContainsKey(type))
            {
                _itemsMap.Remove(type);
            }
        }
    }
}
