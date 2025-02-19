using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StackLeader
{
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            SqlDependency.Stop(WindowsHelper.connect.ConnectionString);
            base.OnExit(e);
        }
    }
}
