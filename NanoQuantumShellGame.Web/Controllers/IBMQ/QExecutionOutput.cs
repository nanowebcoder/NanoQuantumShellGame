using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoQuantumShellGame.Web
{
    public class QExecutionOutput
    {
        public Result result { get; set; }
        public DateTime startDate { get; set; }
        public long modificationDate { get; set; }
        public double time { get; set; }
        public DateTime endDate { get; set; }
        public Status status { get; set; }
        public string deviceRunType { get; set; }
        public Ip ip { get; set; }
        public Calibration calibration { get; set; }
        public int shots { get; set; }
        public ParamsCustomize paramsCustomize { get; set; }
        public bool deleted { get; set; }
        public bool userDeleted { get; set; }
        public string id { get; set; }
        public string codeId { get; set; }
        public string deviceId { get; set; }
        public string userId { get; set; }
        public Code code { get; set; }

        public class P
        {
            public List<int> qubits { get; set; }
            public List<string> labels { get; set; }
            public List<double> values { get; set; }
        }

        public class AdditionalData
        {
            public int seed { get; set; }
        }

        public class Data
        {
            public string creg_labels { get; set; }
            public P p { get; set; }
            public AdditionalData additionalData { get; set; }
            public string qasm { get; set; }
            public string serialNumberDevice { get; set; }
            public double time { get; set; }
        }

        public class Result
        {
            public DateTime date { get; set; }
            public Data data { get; set; }
        }

        public class Status
        {
            public string id { get; set; }
        }

        public class Ip
        {
            public string ip { get; set; }
            public string country { get; set; }
            public string continent { get; set; }
        }

        public class Calibration
        {
        }

        public class ParamsCustomize
        {
            public int seed { get; set; }
        }

        public class MeasureCreg
        {
            public int line { get; set; }
            public int bit { get; set; }
        }

        public class Gate
        {
            public string name { get; set; }
            public string qasm { get; set; }
            public int position { get; set; }
            public MeasureCreg measureCreg { get; set; }
            public int? to { get; set; }
        }

        public class Playground
        {
            public string name { get; set; }
            public int line { get; set; }
            public List<Gate> gates { get; set; }
        }

        public class JsonQASM
        {
            public List<object> gateDefinitions { get; set; }
            public string topology { get; set; }
            public List<Playground> playground { get; set; }
            public int numberGates { get; set; }
            public bool hasMeasures { get; set; }
            public int numberColumns { get; set; }
            public string include { get; set; }
        }

        public class Code
        {
            public string type { get; set; }
            public bool active { get; set; }
            public int versionId { get; set; }
            public string idCode { get; set; }
            public string name { get; set; }
            public JsonQASM jsonQASM { get; set; }
            public string qasm { get; set; }
            public string codeType { get; set; }
            public DateTime creationDate { get; set; }
            public bool deleted { get; set; }
            public long orderDate { get; set; }
            public bool userDeleted { get; set; }
            public string id { get; set; }
            public string userId { get; set; }
        }





    }
}
