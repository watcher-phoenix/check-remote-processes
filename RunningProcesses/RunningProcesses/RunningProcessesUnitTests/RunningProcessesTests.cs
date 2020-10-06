using NUnit.Framework;
using RunningProcesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningProcessesUnitTests
{
    public class RunningProcessesTests
    {

        [Test]
        public void CheckingRunnerStatus()
        {
           // var list = new List<AppsToCheck>();
            var check = new EmailRunnerStatus();
            check.CheckRunnerStatus();
        }

    }
}
