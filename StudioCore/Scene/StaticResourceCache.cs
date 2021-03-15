using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace StudioCore.Scene
{
    internal static class StaticResourceCache
    {
        private static readonly Dictionary<ShaderSetCacheKey, (Shader, Shader)> s_shaderSets
            = new Dictionary<ShaderSetCacheKey, (Shader, Shader)>();

        public static (Shader vs, Shader fs) GetShaders(
            GraphicsDevice gd,
            ResourceFactory factory,
            string name)
        {
            SpecializationConstant[] constants = ShaderHelper.GetSpecializations(gd);
            ShaderSetCacheKey cacheKey = new ShaderSetCacheKey(name, constants);
            if (!s_shaderSets.TryGetValue(cacheKey, out (Shader vs, Shader fs) set))
            {
                set = ShaderHelper.LoadSPIRV(gd, factory, name);
                s_shaderSets.Add(cacheKey, set);
            }

            return set;
        }

        public static void DestroyAllDeviceObjects()
        {
            foreach (KeyValuePair<ShaderSetCacheKey, (Shader, Shader)> kvp in s_shaderSets)
            {
                kvp.Value.Item1.Dispose();
                kvp.Value.Item2.Dispose();
            }
            s_shaderSets.Clear();
        }
    }
}
