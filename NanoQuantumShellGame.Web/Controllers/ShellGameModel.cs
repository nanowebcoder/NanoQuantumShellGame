using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace NanoQuantumShellGame.Web
{
    public class ShellGameModel
    {
        public string ShellSelected { get; set; }
        public string ComputedShell { get; set; }
        public string QASM { get; set; }
        public string ExecutionResults { get; set; }

    }
}
