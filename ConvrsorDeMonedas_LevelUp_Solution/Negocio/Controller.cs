﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public static class Controller
    {
        public static void CrearCarpetas()
        {
            if (!Directory.Exists(@"C:\archivos"))
            {
                Directory.CreateDirectory(@"C:\archivos");
                Directory.CreateDirectory(@"C:\archivos\inbox");
                Directory.CreateDirectory(@"C:\archivos\proceso");
                Directory.CreateDirectory(@"C:\archivos\backup");
                Directory.CreateDirectory(@"C:\archivos\final");
            }
           
        }
    }
}
