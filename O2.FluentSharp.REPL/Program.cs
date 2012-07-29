﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel;
using O2.DotNetWrappers.ExtensionMethods;
using O2.DotNetWrappers.DotNet;
namespace O2.FluentSharp.REPL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AssemblyResolver.setAssemblyResolver();     
            start(args);
        }

        //we need to do this on a separate method so that the JIT doesn't compile this before Main execution
        public static void start(string[] args)
        {
            if (args.size() > 0)
                launch.o2Gui(args);
            else
                open.scriptEditor();
            
        }
    }   
}
