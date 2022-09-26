using dnlib.DotNet;
using dnlib.DotNet.Emit;

namespace CosturaRemover.Stages
{
    public class Cleaner : IStage
    {
        public void Execute(ModuleDefMD module)
        {
            foreach (var type in module.Types)
            {
                if (type.Name != "AssemblyLoader" || type.Namespace != "Costura") continue;
                foreach (var instruction in module.GlobalType.FindOrCreateStaticConstructor().Body.Instructions)
                {
                    if (instruction.OpCode != OpCodes.Call) continue;
                    var method = (IMethod)instruction.Operand;
                    if (method.DeclaringType.Name != "AssemblyLoader") continue;
                    module.GlobalType.FindOrCreateStaticConstructor().Body.Instructions.Remove(instruction);
                    break;
                }

                module.Types.Remove(type);
                break;
            }
        }

        public override string ToString()
        {
            return "Cleaner";
        }
    }
}