using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Uygulama1.Models
{
    public class MyContext: DbContext
    {
        public MyContext():base("MyDataBase")
        {

        }
        public DbSet<Urun> Urun { get; set; }
    }
}