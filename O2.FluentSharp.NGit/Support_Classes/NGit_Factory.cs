﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentSharp.ExtensionMethods;
using Sharpen;

namespace FluentSharp.Support_Classes
{
    public class NGit_Factory
    {
        public static OutputStream New_OutputStream()
        {
            return Type_ByteArrayOutputStream().ctor()
                                               .cast<OutputStream>();
        }

        public static Assembly Dll_Sharpen()
        {
            return "Sharpen.dll".assembly();
        }

        public static Type Type_ByteArrayOutputStream()
        {
            return Dll_Sharpen().type("ByteArrayOutputStream");
        }

    }
}
