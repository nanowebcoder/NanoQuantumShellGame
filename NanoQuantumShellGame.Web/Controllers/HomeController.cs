using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;

namespace NanoQuantumShellGame.Web.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ShellGameModel emptyModel = new ShellGameModel();

            return View(emptyModel);
        }

        [HttpPost]
        public IActionResult Index(ShellGameModel model)
        {

            
            Debug.WriteLine("Preparing to Compute Quantum Results using selected Shell #" + model.ShellSelected + "...");
            model.ComputedShell = "0";
            model.QASM = "";
            model.ExecutionResults = "";

            string token = "INSERTYOURTOKENHERE";


            //Build the processor
            QProcessor qp = new QProcessor(token);


            //login
            QResult result = qp.Login();
            Debug.WriteLine(string.Format("Login result.  Success={0} Message={1}", result.Success.ToString(), result.Message));

            if (result.Success)
            {
                int shell = 0;
                Int32.TryParse(model.ShellSelected, out shell);

                //build the QASM code
                QCode code = new QCode(shell);
                code.name = string.Format("ExperimentID {0} with Shell at {1} ", System.Guid.NewGuid().ToString(), shell.ToString());
                Debug.WriteLine("Code:" + Environment.NewLine + code.qasm);


                //execute the code
                result = qp.ExecuteCode(code);
                Debug.WriteLine(string.Format("Code Executed Success={0}, Data={1}", result.Success.ToString(), result.Message));

                //parse the result and output the location of the coin
                QExecutionOutput x = qp.GetOutputFromMessageData(result.Message);

                string labels = x.result.data.p.labels[0];
                switch (labels)
                {
                    case "00000":
                        Debug.WriteLine("The coin was under Shell #1");
                        model.ComputedShell = "1";
                        break;
                    case "00100":
                        Debug.WriteLine("The coin was under Shell #2");
                        model.ComputedShell = "2";
                        break;
                    case "00010":
                        Debug.WriteLine("The coin was under Shell #3");
                        model.ComputedShell = "3";
                        break;
                    case "00110":
                        Debug.WriteLine("The coin was under Shell #4");
                        model.ComputedShell = "4";
                        break;
                    default:
                        Debug.WriteLine("Something broke!");
                        model.ComputedShell = "0";
                        break;
                }

                model.QASM = JsonConvert.SerializeObject(x.code, Formatting.Indented);
                model.ExecutionResults = JsonConvert.SerializeObject(x.result, Formatting.Indented);

                //now cleanup and delete the results
                QResult deleteResult = qp.DeleteExperiment(x.code.idCode);
            }


            return View(model);




        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
