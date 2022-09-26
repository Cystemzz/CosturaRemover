using dnlib.DotNet;

namespace CosturaRemover.Stages
{
    public class FodyRemover : IStage
    {
        public void Execute(ModuleDefMD module)
        {
            foreach (var type in module.Types)
            {
                if (!type.IsClass || !type.Name.String.EndsWith("_ProcessedByFody")) continue;
                module.Types.Remove(type);
                break;
            }
        }

        public override string ToString()
        {
            return "FodyRemover";
        }
    }
}