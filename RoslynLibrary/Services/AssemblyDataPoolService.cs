using Library;
using Library.Models;
using Microsoft.Extensions.Options;
using RoslynLibrary.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynLibrary.Services
{
    public class AssemblyDataPoolService
    {
        private readonly ManagedSection _managedSection;
        public Dictionary<string, AssemblyModel> Assemblies { get; private set; } = new Dictionary<string, AssemblyModel>();
        public Dictionary<string, string> AssembliesText { get; private set; } = new Dictionary<string, string>();

        public AssemblyDataPoolService(IOptions<ManagedSection> managedSection)
        {
            _managedSection = managedSection.Value;
        }

        //public List<string> Search(params string[] searchText)
        //{
        //    var types = Assemblies.Values.SelectMany(s => s.Types).ToArray();

        //    foreach(var type in types)
        //    {
        //        type.Properties.FirstOrDefault(s => s.)
        //        if ()
        //    }

        //    return new List<string>();
        //}

        public void Update()
        {
            foreach (var filePath in Directory.GetFiles(_managedSection.Path).Where(s => s.EndsWith(".dll") && !s.Contains("Newtonsoft.Json.dll")))
            {
                var assemblyModel = AssemblyDataSerializer.ConvertToModel(filePath);
                if (assemblyModel == null)
                {
                    continue;
                }

                var fileName = Path.GetFileName(filePath);
                Assemblies[fileName] = assemblyModel;
                AssembliesText[fileName] = AssemblyDataSerializer.ConvertToText(assemblyModel);
            }
        }
    }
}
