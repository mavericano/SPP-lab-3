using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser
{
    public class ReaderImpl : IReader
    {
        private string _path;
        private Assembly _asm;
        private List<AssemblyData> _typeList;

        private List<MethodInfo> GetMethods(Type type)
        {
            List<MethodInfo> methods = new List<MethodInfo>();
            foreach (var method in type.GetMethods())
            {
                if (method.Module.Name.Equals(_path.Substring(_path.LastIndexOf("\\") + 1)))
                {
                    methods.Insert(0, method);
                }
            }
            return methods;
        }

        private FieldInfo[] GetFields(Type type)
        {
            return type.GetFields(); ;
        }

        private PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties();
        }

        private Assembly LoadAssembly(string path)
        {
            return Assembly.LoadFrom(path);
        }

        private List<Type> GetTypes()
        {
            List<Type> types = new List<Type>();
            foreach (var type in _asm.GetTypes())
            {
                if (type.Namespace != "System.Runtime.CompilerServices" && type.Namespace != "Microsoft.CodeAnalysis")
                    types.Insert(0, type);
            }
            return types;
        }

        public List<AssemblyData> GetAssemblyData(string path)
        {
            _path = path;
            _asm = LoadAssembly(path);
            _typeList = new List<AssemblyData>();
            List<Type> asmTypes = GetTypes();
            foreach (var type in asmTypes)
            {
                AssemblyData data = new AssemblyData();
                data.nameSpace = type.Namespace;
                data.type = type.Name;
                data.fields = GetFields(type).ToList();
                data.methods = GetMethods(type);
                data.properties = GetProperties(type).ToList();
                _typeList.Add(data);
            }
            return _typeList;
        }
    }
}
