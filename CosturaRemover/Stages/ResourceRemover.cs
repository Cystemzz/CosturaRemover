using dnlib.DotNet;

namespace CosturaRemover.Stages
{
    public class ResourceRemover : IStage
    {
        public void Execute(ModuleDefMD module)
        {
            var count = module.Resources.Count;
            for (var i = 0; i < count; i++)
            {
                if (!module.Resources[i].Name.StartsWith("costura.")) continue;
                module.Resources.RemoveAt(i);
                count--;
                i--;
            }
        }

        public override string ToString()
        {
            return "ResourceRemover";
        }
    }
}