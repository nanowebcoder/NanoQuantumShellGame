using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoQuantum.Core
{
    public class QCode
    {
        public string name = "";
        public string qasm = "";
        public string codeType = "QASM2";

        public string deviceRunType = "simulator";
        public int shots = 1;
        public int seed = 1;


        public QCode(int coinUnderShellNumber)
        {
            //5 qubits and 5 classical registers
            string preCode = "include \"qelib1.inc\";qreg q[5];creg c[5];";

            //everything starting at the "double Hadamard"
            string postControlledNot = @"h q[1];
                                        h q[2];
                                        x q[1];
                                        x q[2];
                                        h q[2];
                                        cx q[1], q[2];
                                        h q[2];
                                        x q[1];
                                        x q[2];
                                        h q[1];
                                        h q[2];
                                        measure q[1] -> c[1];
                                        measure q[2] -> c[2];";

            string initCode = "";
            switch (coinUnderShellNumber)
            {

                case 1:  //#State=00
                    initCode = @"h q[1];
                                h q[2];
                                s q[1];
                                s q[2];
                                h q[2];
                                cx q[1],q[2];
                                h q[2];
                                s q[1];
                                s q[2];
                                ";
                    break;
                case 2:  //#State=01
                    initCode = @"h q[1];
                                h q[2];
                                s q[2];
                                h q[2];
                                cx q[1],q[2];
                                h q[2];
                                s q[2];";
                    break;
                case 3: //#State=10
                    initCode = @"h q[1];
                                h q[2];
                                s q[1];
                                h q[2];
                                cx q[1],q[2];
                                h q[2];
                                s q[1];";
                    break;
                case 4: //#State=11
                    initCode = @"h q[1];
                            h q[2];
                            h q[2];
                            cx q[1],q[2];
                            h q[2];
                            ";
                    break;
                default:
                    throw new Exception("There can only be 4 shells.");
            }
            this.qasm = preCode + initCode + postControlledNot;
            this.shots = 500;

        }
    }
}
