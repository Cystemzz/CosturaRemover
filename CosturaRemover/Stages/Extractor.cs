using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using dnlib.DotNet;

namespace CosturaRemover.Stages
{
    public class Extractor : IStage
    {
        public void Execute(ModuleDefMD module)
        {
            if (module.Resources.Count == 0) return;
            foreach (var resource in module.Resources)
            {
                if (resource.ResourceType != ResourceType.Embedded) continue;
                if (!resource.Name.StartsWith("costura.") || !resource.Name.EndsWith(".compressed")) continue;
                var embeddedResource = resource as EmbeddedResource;
                var stream = embeddedResource?.CreateReader().AsStream();
                var embeddedBytes = new List<byte>();
                for (var i = 0; i < stream?.Length; i++)
                {
                    var myByte = stream.ReadByte();
                    if (myByte == -1) break;
                    embeddedBytes.Add((byte)myByte);
                }

                File.WriteAllBytes(resource.Name.String.Remove(0, 8).Remove(resource.Name.String.Length - 19, 11),
                    Decompress(embeddedBytes.ToArray()));
            }
        }

        private static byte[] Decompress(byte[] data)
        {
            var output = new MemoryStream();
            new DeflateStream(new MemoryStream(data), CompressionMode.Decompress).CopyTo(output);
            return output.ToArray();
        }

        public override string ToString()
        {
            return "Extractor";
        }
    }
}