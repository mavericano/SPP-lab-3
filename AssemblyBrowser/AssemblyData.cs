using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class AssemblyData
    {
        public string nameSpace { get; set; }
        public string type { get; set; }
        public List<MethodInfo> methods { get; set; }
        public List<PropertyInfo> properties { get; set; }
        public List<FieldInfo> fields { get; set; }

        public AssemblyData()
        {
            nameSpace = "";
            type = "";
            methods = new List<MethodInfo>();
            properties = new List<PropertyInfo>();
            fields = new List<FieldInfo>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"Class - {WriteClass()}\n Methods - \n{WriteMethods()}\n Properties - \n{WriteProperties()}\n " +
                           $"Fields - \n{WriteFields()}");

            return builder.ToString();
        }

        private string WriteClass()
        {
            return type;
        }
        private string WriteMethods()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var method in methods)
            {
                builder.Append("\t" + method.ReturnType.Name + " " + method.Name + " (");
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length != 0)
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var par = parameters[i];
                        if (i == parameters.Length - 1)
                            builder.Append(par + ")\n");
                        else
                            builder.Append(par + ", ");
                    }
                else
                {
                    builder.Append(")\n");
                }
            }
            return builder.ToString();
        }

        private string WriteProperties()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var prop in properties)
            {
                builder.Append("\t" + prop.Name + "\n");
            }
            return builder.ToString();
        }

        private string WriteFields()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var field in fields)
            {
                builder.Append("\t" + field.Name + "\n");
            }
            return builder.ToString();
        }
    }
}
