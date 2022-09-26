using System;
using System.Collections.Generic;
using CosturaRemover.Stages;
using dnlib.DotNet;

namespace CosturaRemover
{
    public static class Program
    {
        private static readonly List<IStage> Stages = new List<IStage>
        {
            new Extractor(),
            new FodyRemover(),
            new Cleaner(),
            new ResourceRemover()
        };

        public static void Main(string[] args)
        {
            var module = ModuleDefMD.Load(args[0]);
            foreach (var stage in Stages)
            {
                Console.WriteLine($"[+] Executing Stage: {stage}");
                stage.Execute(module);
            }
            module.Write(args[0].Replace(".exe", ".nocostura.exe"));
            Console.WriteLine("[+] Costura removed, press any key to exit");
            Console.ReadKey();
        }
    }
}