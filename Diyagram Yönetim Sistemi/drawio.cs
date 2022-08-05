using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diyagram_Yönetim_Sistemi
{
    public class drawio
    {
        drawio(){
            node = "mxfile/diagram";
        }

        protected string node{
            get { return node; }
            set { node = value; }
        }

    }

}