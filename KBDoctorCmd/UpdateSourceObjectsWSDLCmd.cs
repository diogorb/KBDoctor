﻿using System;
using System.Diagnostics;
using Artech.Architecture.Common.Services;
using Artech.MsBuild.Common;
using Concepto.Packages.KBDoctorCore.Sources;

namespace KBDoctorCmd
{
    public class UpdateSourceObjectsWSDLCmd : ArtechTask
    {
        public override bool Execute()
        {
            bool isSuccess = true;
            Stopwatch watch = null;
            OutputSubscribe();
            IOutputService output = CommonServices.Output;
            output.StartSection("Update WSDL source");
            try
            {
                watch = new Stopwatch();
                watch.Start();

                if (KB == null)
                {
                    output.AddErrorLine("No hay ninguna KB abierta en el contexto actual.");
                    isSuccess = false;
                }
                else
                {
                    output.AddLine("KBDoctor",string.Format(KB.Name, KB.Location));
                    API.SaveObjectsWSDLSource(KB, output);
                }

            }
            catch (Exception e)
            {
                output.AddErrorLine(e.Message);
                isSuccess = false;
            }
            finally
            {
                output.EndSection("Update WSDL source ", isSuccess);
                OutputUnsubscribe();
            }
            return isSuccess;
        }
    }
}
