using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Options;
using RoslynLibrary.Sections;
using ICSharpCode.Decompiler.TypeSystem;
using System.Reflection;

namespace RoslynLibrary.Services
{
    public class CSharpDecompileService
    {
        private readonly ManagedSection _managedSection;

        public CSharpDecompileService(IOptions<ManagedSection> managedSection)
        {
            _managedSection = managedSection.Value;
        }

        public Result Decompile(string methodName)
        {
            var decompiler = new CSharpDecompiler(Path.Combine(_managedSection.Path, "Assembly-CSharp.dll"), new DecompilerSettings());

            foreach (var type in decompiler.TypeSystem.GetAllTypeDefinitions())
            {
                //if (typeName == type.Name)
                {
                    foreach (var method in type.Methods)
                    {
                        if (method.Name == methodName)
                        {
                            var decompiled = decompiler.DecompileAsString(method.MetadataToken);
                            return new Result() 
                            {
                                TypeName = type.FullName,
                                MethodName = method.FullName,
                                Code = decompiled 
                            };
                        }
                    }
                }
            }

            var files = Directory.GetFiles(_managedSection.Path).Where(s => !s.Contains("Assembly-CSharp.dll"));

            foreach(var file in files)
            {
                decompiler = new CSharpDecompiler(file, new DecompilerSettings());

                foreach (var type in decompiler.TypeSystem.GetAllTypeDefinitions())
                {
                    //if (typeName == type.Name)
                    {
                        foreach (var method in type.Methods)
                        {
                            if (method.Name == methodName)
                            {
                                var decompiled = decompiler.DecompileAsString(method.MetadataToken);

                                return new Result()
                                {
                                    TypeName = type.FullName,
                                    MethodName = method.FullName,
                                    Code = decompiled
                                };
                            }
                        }
                    }
                }
            }

            return null;
        }

        public class Result
        {
            public string TypeName { get; set; }
            public string MethodName { get; set; }
            public string Code { get; set; }
        }
    }
}
