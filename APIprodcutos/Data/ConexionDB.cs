using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace APIprodcutos.Data
{
    public class ConexionDB
    {
        //refencia al connectrion string ubicado en el web config
        public static string cn = ConfigurationManager.ConnectionStrings["ProductosDB"].ToString();
    }
}