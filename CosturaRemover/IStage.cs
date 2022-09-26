using dnlib.DotNet;

namespace CosturaRemover
{
    public interface IStage
    {
        void Execute(ModuleDefMD module);
    }
}