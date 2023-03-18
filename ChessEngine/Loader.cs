using System.Reflection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using ChessPlayerDLL;
using System.Data;

namespace ChessEngine
{
    public class Loader
    {
        public Loader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveChessPlayerAssembly;
        }

        public static IChessPlayer LoadFromFile(string path)
        {
            var DLL = Assembly.LoadFile(path);
            foreach (Type type in DLL.GetExportedTypes())
            {
                if (type.GetProperty("IsChessPlayer") != null)
                {
                    var wrapper = new ChessPlayerWrapper(type);
                    if (wrapper.IsValid)
                    {
                        return wrapper;
                    }
                }
            }

            throw new Exception($"Unable to find a type with the IsChessPlayer property in the dll {DLL.GetName()}");
        }

        private static Assembly ResolveChessPlayerAssembly(object? sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("ChessPlayerDLL"))
            {
                return typeof(IChessPlayer).Assembly;
            }

            throw new Exception($"Uknown assembly {args.Name}");
        }
    }
}