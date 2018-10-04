﻿using Artech.Architecture.Common.Collections;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.Common.Services;
using Artech.Architecture.UI.Framework.Services;
using Artech.Genexus.Common.Helpers;
using Artech.Genexus.Common.Objects;
using Artech.Udm.Framework.References;
using Artech.Architecture.UI.Framework.Helper;
using Artech.Common.Framework.Commands;
using Artech.Genexus.Common.Entities;
using Artech.Genexus.Common;

using System;
using System.Collections.Generic;
using System.Text;

using Artech.Genexus.Common.Collections;
using System.Text.RegularExpressions;


using System.Linq;
using System.Xml;
using System.IO;

using System.Xml.Xsl;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Collections;
using System.Threading;
using System.Globalization;
using Artech.Packages.Patterns.Definition;
using Artech.Packages.Patterns.Engine;
using Artech.Packages.Patterns;
using Artech.Packages.Patterns.Objects;
using Artech.Genexus.Common.Parts;
using Artech.Udm.Framework;
//using Artech.Genexus.Common.Resources;
using Concepto.Packages.KBDoctorCore.Sources;

namespace Concepto.Packages.KBDoctor
{
    static class KbStats
    {
        public static void CountGeneratedByPattern()
        {
            IKBService kbserv = UIServices.KB;

            string title = "KBDoctor - Count objects generated by patterns";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);

                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);


                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);
                writer.AddTableHeader(new string[] { "Name", "Type", "Description" });


                Int32 Objcount = 0;
                Int32 ObjNoPattern = 0;
                Int32 ObjOther = 0;
                Int32 ObjNoGenerate = 0;

                foreach (KBObject obj in kbserv.CurrentModel.Objects.GetAll())
                {
                    Objcount += 1;
                    if (KBDoctorCore.Sources.Utility.isGenerated(obj) && (obj is WebPanel || obj is Transaction || obj is WorkPanel || obj is DataSelector))
                    {
                        //El objeto es generado con algun pattern?
                        Boolean isGeneratedWithPattern = false;
                        PatternDefinition pattern;
                        if (InstanceManager.IsInstanceObject(obj, out pattern))
                            isGeneratedWithPattern = true;

                        //El objeto tiene algun pattern asociado? En caso de transacciones y webpanels del WW+
                        bool isParentPattern = false;
                        foreach (PatternDefinition p in PatternEngine.Patterns)
                        {
                            if (PatternInstance.Get(obj, p.Id) != null)
                                isParentPattern = true;
                        }

                        if (!isGeneratedWithPattern && !isParentPattern)
                        {
                            ObjNoPattern += 1;
                            writer.AddTableData(new string[] { Functions.linkObject(obj), obj.TypeDescriptor.Name, obj.Description });
                        }

                    }


                }

                writer.AddTableData(new string[] { " ", " ", " " });
                Int32 IndexP = Objcount == 0 ? 0 : (ObjNoPattern * 100 / Objcount);
                writer.AddTableData(new string[] { "Objects no generated with patterns / Total objects", IndexP.ToString() + " %", "Obj.No Patterns=" + ObjNoPattern.ToString() + " Total Objects=" + Objcount.ToString() });

                string texto = "Objects no generated with patterns / Total objects = " + IndexP.ToString() + " %" + ",No generated with Patterns=" + ObjNoPattern.ToString() + ", Total Objects=" + Objcount.ToString();

                Functions.AddLineSummary("SummaryPatternGeneration.txt", texto);


                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }
        }



        public static void ListObjDiffMasterPage2()
        {
            IKBService kbserv = UIServices.KB;

            Dictionary<string, KBObjectCollection> dic = new Dictionary<string, KBObjectCollection>();

            string title = "KBDoctor - Objects";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);


                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);

                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);

                output.AddLine("KBDoctor", "Listing objects...");
                writer.AddTableHeader(new string[] { "Tipo", "Object", "Description", "Last Update", "Timestamp" });
                foreach (KBObject obj in kbserv.CurrentModel.Objects.GetAll())
                {
                    writer.AddTableData(new string[] { obj.TypeDescriptor.Name, obj.Name, obj.Description, obj.LastUpdate.ToString(), obj.Timestamp.ToString() });
                }
                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }
        }

        public static void ListLastReports()
        {
            IKBService kbserv = UIServices.KB;

            Dictionary<string, KBObjectCollection> dic = new Dictionary<string, KBObjectCollection>();

            string title = "KBDoctor - List last Reports";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);

                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);

                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);

                writer.AddTableHeader(new string[] { "Report", "", "Date Generated" });

                string[] fileEntries = Directory.GetFiles(kbserv.CurrentKB.UserDirectory, "kbdoctor*.html");

                foreach (string fileName in fileEntries)
                {

                    string dateFilename = "";
                    int days = (int)(DateTime.Now - File.GetLastWriteTime(fileName)).TotalDays;
                    int hours = (int)(DateTime.Now - File.GetLastWriteTime(fileName)).TotalHours;
                    int mins = (int)(DateTime.Now - File.GetLastWriteTime(fileName)).TotalMinutes;

                    if (days > 0)
                        dateFilename = days.ToString() + " day(s) ago";
                    else
                        if (hours > 0)
                        dateFilename = hours.ToString() + " hour(s) ago";
                    else
                        dateFilename = mins.ToString() + " minutes(s) ago";

                    string shortFilename = Path.GetFileName(fileName);
                    shortFilename = shortFilename.Replace("kbdoctor.", "");
                    shortFilename = shortFilename.Replace(".html", "");
                    shortFilename = Regex.Replace(shortFilename, "(\\B[A-Z])", " $1");

                    string fileLink = "<a href=\"file:///" + fileName + "\">" + shortFilename + "</a>";
                    writer.AddTableData(new string[] { fileLink, dateFilename, File.GetLastWriteTime(fileName).ToString() });

                }
                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }
        }
        
        public static void ListObj()
        {
            IKBService kbserv = UIServices.KB;


            Dictionary<string, KBObjectCollection> dic = new Dictionary<string, KBObjectCollection>();

            string title = "KBDoctor - Objects";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);


                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);

                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);
                int numObj = 0;

                DateTime limite = new DateTime(2017, 06, 30);
                output.AddLine("KBDoctor", "Fecha Limite : " + limite.ToString());


                writer.AddTableHeader(new string[] { "Type", "Object", "Description", "Module", "Public", "Last Update", "Is Main", "TimeStamp", "Is Generated", "Protocol", "AppLocation" });

                foreach (KBObject obj in kbserv.CurrentModel.Objects.GetAll())
                {
                    if (obj != null)
                        if (Functions.isRunable(obj) && KBDoctorCore.Sources.Utility.isGenerated(obj) && (obj.LastUpdate >= limite || obj.Timestamp >= limite))
                        {
                            string desc = obj.Description.Replace(",", " ");
                            desc = desc.Replace(">", "");
                            desc = desc.Replace("<", "");
                            string objAppGenerator = obj.GetPropertyValueString("AppGenerator");
                            string objProtocol = obj.GetPropertyValueString("CALL_PROTOCOL");
                            if (objProtocol == "Internal")
                                objProtocol = "";

                            string isMain = obj.GetPropertyValue<bool>("IsMain") ? "True" : "";
                            string isGenerated = KBDoctorCore.Sources.Utility.isGenerated(obj) ? "Yes" : "";


                            string appLocation = obj.UserName.ToString();
                            writer.AddTableData(new string[] { obj.TypeDescriptor.Name +" ", Functions.linkObject(obj), desc,
                            obj.Module.Name, obj.IsPublic.ToString(),obj.LastUpdate.ToString(),isMain, obj.Timestamp.ToString(), isGenerated, objProtocol, appLocation});

                            numObj += 1;
                            if ((numObj % 200) == 0)
                                output.AddLine("KBDoctor", obj.TypeDescriptor.Name + "," + obj.Name + "," + obj.Description); //+ "," + obj.Timestamp.ToString());
                        }
                }
                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }
        }
        

        public static void ListMainObj()
        {
            IKBService kbserv = UIServices.KB;


            Dictionary<string, KBObjectCollection> dic = new Dictionary<string, KBObjectCollection>();

            string title = "KBDoctor - Main Objects";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);


                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);

                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);

                writer.AddTableHeader(new string[] { "Type", "Object", "Description", "Module", "Public", "Generator", "Protocol", "Last Update" });

                KBCategory mainCategory = Functions.MainCategory(kbserv.CurrentModel);
                foreach (KBObject obj in mainCategory.AllMembers)
                {
                    if (obj != null)
                    {
                        string desc = obj.Description.Replace(",", " ");
                        desc = desc.Replace(">", "");
                        desc = desc.Replace("<", "");
                        string objAppGenerator = obj.GetPropertyValueString("AppGenerator");
                        string objProtocol = obj.GetPropertyValueString("CALL_PROTOCOL");


                        string isGenerated = KBDoctorCore.Sources.Utility.isGenerated(obj) ? "Yes" : "";


                        writer.AddTableData(new string[] { obj.TypeDescriptor.Name +" ", Functions.linkObject(obj), desc,
                                obj.Module.Name, obj.IsPublic.ToString(), objAppGenerator, objProtocol, obj.LastUpdate.ToShortDateString()});


                        output.AddLine("KBDoctor", obj.TypeDescriptor.Name + "," + obj.Name + "," + obj.Description); //+ "," + obj.Timestamp.ToString());
                    }
                }
                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }

        }


        public static void ListProc()
        {
            IKBService kbserv = UIServices.KB;


            Dictionary<string, KBObjectCollection> dic = new Dictionary<string, KBObjectCollection>();

            string title = "KBDoctor - Procedure";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);
                KBModel kbmodel = kbserv.CurrentModel;


                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);

                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);

                writer.AddTableHeader(new string[] { "Type", "Object", "Commit ", "Module", "Public", "Generator", "Protocol", "Last Update" });
                string commitOnExit = "";

                foreach (KBObject obj in kbmodel.Objects.GetAll())
                {
                    commitOnExit = "";
                    if (obj is Procedure)
                    {
                        if (obj.VersionDate >= DateTime.Now.AddDays(-45))
                        {
                            object aux = obj.GetPropertyValue("CommitOnExit");
                            if (aux != null)
                            {
                                commitOnExit = aux.ToString();
                            }

                        }
                        writer.AddTableData(new string[] { obj.TypeDescriptor.Name +" ", Functions.linkObject(obj), commitOnExit ,
                                obj.Module.Name, obj.IsPublic.ToString(), "", "", obj.LastUpdate.ToShortDateString()});


                        output.AddLine("KBDoctor", obj.TypeDescriptor.Name + "," + obj.Name + "," + obj.Description); //+ "," + obj.Timestamp.ToString());
                    }
                }
                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }
        }



        public static void List2(KBObject obj, string objLocation, Dictionary<string, KBObjectCollection> dic,KBDoctorXMLWriter writer)
        {
            string objMasterPage = obj.GetPropertyValueString("MasterPage");
            writer.AddTableData(new string[] { obj.TypeDescriptor.Name, Functions.linkObject(obj), objLocation, objMasterPage });
            if (ObjectsHelper.IsCallalable(obj))
            {
                foreach (EntityReference reference in obj.GetReferences())
                {
                    KBObject objRef = KBObject.Get(obj.Model, reference.To);
                    string typeDescriptor = obj.TypeDescriptor.Name;
                    List<string> list = new List<string> { "WebPanel", "Transaction", "WorkPanel" };

                    if ((objRef != null) && list.Contains(typeDescriptor) && (reference.ReferenceType == ReferenceType.Hard))
                    {
                        int count = 0;
                        string locations = "";
                        KBObjectCollection objColl = new KBObjectCollection();
                        foreach (string loc in dic.Keys)
                        {
                            if ((loc != objLocation) && (list.Contains(objRef.TypeDescriptor.Name)))
                            {
                                dic.TryGetValue(loc, out objColl);
                                if (objColl.Contains(obj))
                                {
                                    locations += " " + loc;
                                    count += 1;
                                }
                            }
                        }
                        if (count > 0)
                        {
                            string objRefMasterPage = objRef.GetPropertyValueString("MasterPage");
                            writer.AddTableData(new string[] { "+-----Called >>" + objRef.TypeDescriptor.Name, Functions.linkObject(objRef), count.ToString() + "-" + locations, objRefMasterPage });
                        }
                    }
                }
            }

        }


        private static KBObjectCollection CreatLocationCollection(string location)
        {
            KBObjectCollection objColl = new KBObjectCollection();
            IKBService kB = UIServices.KB;
            IOutputService output = CommonServices.Output;
            KBCategory mainCategory = KBCategory.Get(kB.CurrentModel, "Main Programs");
            foreach (KBObject obj in mainCategory.AllMembers)
            {
                string objLocation = (string)obj.GetProperty("AppLocation").Value;

                if (objLocation == "")
                    obj.SilentSetPropertyValue("AppLocation", "WEB");

                string objAppGenerator = obj.GetPropertyValueString("AppGenerator");
                string Dircopia;
                if (objAppGenerator.ToUpper().Contains("WEB"))
                    Dircopia = "Web\\bin\\";
                else
                    Dircopia = "bin\\";


                if (objLocation == null)
                    objLocation = "none";

                string letra = "";

                if (location == objLocation)
                {
                    output.AddLine("KBDoctor","set DESTINO=" + objLocation);
                    output.AddLine("KBDoctor","XCOPY " + Dircopia + obj.Name + ".dll %DESTINO%");

                    if (obj is Procedure)
                        letra = "a";
                    if (obj is WorkPanel)
                        letra = "u";
                    if (obj is Transaction)
                        letra = "";

                    output.AddLine("KBDoctor","XCOPY " + Dircopia + letra + obj.Name + ".dll %DESTINO%");
                    AddReferencedObj(objColl, obj, "");

                }

            }
            return objColl;
        }

        private static void AddReferencedObj(KBObjectCollection objColl, KBObject obj, string tabs)
        {

            IKBService kbserv = UIServices.KB;
            IOutputService output = CommonServices.Output;
            objColl.Add(obj);
            string RefTabs = tabs + "    ";

            if (ObjectsHelper.IsCallalable(obj))
            {
                foreach (EntityReference reference in obj.GetReferences())
                {
                    KBObject objRef = KBObject.Get(obj.Model, reference.To);

                    if ((objRef != null) && !(objColl.Contains(objRef)) && (reference.ReferenceType == ReferenceType.Hard) && (ObjectsHelper.IsCallalable(objRef)))
                    {
                        if (!(objRef is Procedure))
                            output.AddLine("KBDoctor",tabs + "XCOPY " + objRef.Name + ".DLL %DESTINO% "); //+ " (" + obj.TypeDescriptor.Name + ")" );

                        AddReferencedObj(objColl, objRef, RefTabs);

                    }
                }
            }
        }

        public static KBObject ObjectPartialName(string objName)
        {
            string[] ns = new[] { "Objects" };

            foreach (KBObject obj in UIServices.KB.CurrentModel.Objects.GetByPartialName(ns, objName))
            {
                if (obj.Name == objName)
                {

                    return obj;
                }

            }
            return null;
        }




        public static void ObjectsWINWEB()
        {
            IKBService kbserv = UIServices.KB;
            KBModel design = kbserv.CurrentModel;
            SpecificationListHelper helper = new SpecificationListHelper(design.Environment.TargetModel);


            string title = "KBDoctor - Objects called win y web";
            try
            {
                string outputFile = Functions.CreateOutputFile(kbserv, title);


                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor", title);


                KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
                writer.AddHeader(title);
                writer.AddTableHeader(new string[] { "Name", "Value", "Observation" });
                foreach (KBObject obj in kbserv.CurrentModel.Objects.GetAll())
                {

                    if (obj is Procedure || obj is Transaction)
                    {
                        output.AddLine("KBDoctor", "Procesing up " + obj.Name);
                        IEnumerable<int> generatorTypes = GetObjectGenerators(obj.Key);

                        string objNamePrior = "";
                        int count = 0;
                        foreach (int genType in generatorTypes)
                        {
                            count += 1;
                        }
                        if (count > 1)
                        {
                            KBObjectCollection objColl = new KBObjectCollection();
                            string mainss = "";

                            output.AddLine("KBDoctor", "Procesing down " + obj.Name);
                            foreach (EntityReference reference in obj.GetReferences())
                            {
                                KBObject objRef = KBObject.Get(obj.Model, reference.To);
                                if ((objRef != null) && (objRef is WorkPanel || objRef is WebPanel) && (reference.ReferenceType == ReferenceType.Hard)) //&& (objRef.TypeDescriptor.Name != "MasterPage") ) 
                                {
                                    if (objNamePrior != obj.Name)
                                    {
                                        string callTree = "";
                                        mainss = MainsOf(obj, objColl, callTree);
                                    }
                                    writer.AddTableData(new string[] { Functions.linkObject(obj), Functions.linkObject(objRef), mainss });
                                    objNamePrior = obj.Name;
                                }
                            }
                        }
                    }
                }

                writer.AddFooter();
                writer.Close();

                KBDoctorHelper.ShowKBDoctorResults(outputFile);
                bool success = true;
                output.EndSection("KBDoctor", title, success);
            }
            catch
            {
                bool success = false;
                KBDoctor.KBDoctorOutput.EndSection(title, success);
            }

        }

        private static IEnumerable<int> GetObjectGenerators(EntityKey key)
        {
            throw new NotImplementedException();
        }

        public static string MainsOf(KBObject obj, KBObjectCollection objColl, string callTree)
        {
            string mains = "";
            if (!objColl.Contains(obj))
            {
                objColl.Add(obj);

                if (obj.GetPropertyValue<bool>("IsMain"))
                {
                    string objAppGenerator = obj.GetPropertyValueString("AppGenerator");
                    if (objAppGenerator.Contains("Java Win") && callTree.Contains("HMaster"))
                    { mains = ""; }
                    else
                    { mains = callTree + "/" + Functions.linkObject(obj) + "(" + objAppGenerator + ")<BR> "; }

                }
                else
                {
                    callTree += obj.Name + "/";
                    foreach (EntityReference reference in obj.GetReferencesTo())
                    {
                        KBObject objRef = KBObject.Get(obj.Model, reference.From);

                        if ((objRef != null) && (obj.Name != objRef.Name) && (reference.ReferenceType == ReferenceType.Hard) && reference.LinkType == LinkType.UsedObject /*&& (obj.TypeDescriptor.Name != "MasterPage")*/ && !objColl.Contains(objRef))
                        {

                            mains += MainsOf(objRef, objColl, callTree);
                        }
                    }
                }
            }
            return mains;
        }

        public static void CompareLastNVGDirectory()
        {

            CompareLastDirectory("NVG");
            
        }

        public static void OpenFolderComparerNavigation()
        {

            Process.Start(KBDoctorHelper.NvgComparerDirectory(UIServices.KB));


        }

        public static void CompareLastOBJDirectory()
        {

           
            CompareLastDirectory("OBJ");
        }


        public static void OpenFolderObjComparerNavigation()
        {

            Process.Start(KBDoctorHelper.ObjComparerDirectory(UIServices.KB));

        }

        private static void CompareLastDirectory(string tipo)
        {

            string dirAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DiffDotNet");
            if (!File.Exists(Path.Combine(dirAppData, "DiffDotNet.exe")))
            {
                Directory.CreateDirectory(dirAppData);
                File.WriteAllBytes(Path.Combine(dirAppData, "MeneesDiffUtils.dll"), Comparer.MeneesDiffUtils);
                File.WriteAllBytes(Path.Combine(dirAppData, "Menees.dll"), Comparer.Menees);
                File.WriteAllBytes(Path.Combine(dirAppData, "DiffDotNet.exe"), Comparer.DiffDotNet);
            }

            // Start the child process.
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
          
            p.StartInfo.FileName = Path.Combine(dirAppData, "DiffDotNet.exe");

            //Saco directorio donde se guardan las comparaciones. 
            IKBService kbserv = UIServices.KB;
            string dir = "";
            if (tipo == "NVG")
                    { dir = KBDoctorHelper.NvgComparerDirectory(kbserv); }
            if (tipo == "OBJ")
                    { dir = KBDoctorHelper.ObjComparerDirectory(kbserv); }
            
            //Me quedo con los ultimos dos directorios
            string d1 = "";
            string d2 = "";
            foreach (string d in Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly))
                {
                    d2 = d1;
                    d1 = d;
                }
            if (d2 == "" || d1 == "")
            {
                string title = "KBDoctor - Compare files";
                IOutputService output = CommonServices.Output;
                output.StartSection("KBDoctor",title);
                output.AddErrorLine("There isn't two directory to compare in " + dir + ". You must generate files first and then compare ");
                output.EndSection("KBDoctor", title, true);

            }
            else
            {
                p.StartInfo.Arguments = d2 + " " + d1;
                p.Start();
            }
        }

        public static void KBInterfaces()
        {
            IKBService kbserv = UIServices.KB;
            KBModel design = kbserv.CurrentModel;
            SpecificationListHelper helper = new SpecificationListHelper(design.Environment.TargetModel);

            string outputFile = kbserv.CurrentKB.UserDirectory + @"\kbdoctor.KBInterfaces.html";
            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            string titulo = "KBDoctor - KB Interfaces";
            IOutputService output = CommonServices.Output;
            output.StartSection(titulo);


           KBDoctorXMLWriter writer = new KBDoctorXMLWriter(outputFile, Encoding.UTF8);
            writer.AddHeader(titulo);
            writer.AddTableHeader(new string[] { "Object", "Variable", "Type", "mains" });

            foreach (KBObject obj in kbserv.CurrentModel.Objects.GetAll())
            {

                if (obj != null)
                {
                    output.AddLine("KBDoctor","Procesing  " + obj.Name);

                 //   IEnumerable<int> generatorTypes = KbStats.GetObjectGenerators(obj.Key);

                    string objNamePrior = "";
                    KBObjectCollection objColl = new KBObjectCollection();
                    string tipo = "";

                    string mainss = "";


                    foreach (EntityReference reference in obj.GetReferences())
                    {
                        KBObject objRef = KBObject.Get(obj.Model, reference.To);

                        if (objRef != null)
                        {
                            if (objRef is ExternalObject)
                            {
                                tipo = "External Object:" + objRef.GetPropertyValueString("ExoType");
                                writer.AddTableData(new string[] { Functions.linkObject(obj), Functions.linkObject(objRef), tipo, mainss });
                            }
                            else
                            {
                                if (objRef is MissingKBObject)
                                {
                                    tipo = "Missing Object";
                                    writer.AddTableData(new string[] { Functions.linkObject(obj), Functions.linkObject(objRef), tipo, mainss });
                                }
                            }

                        }




                    }


                    string sourceWOComments = Functions.ExtractComments(Functions.ObjectSourceUpper(obj));

                    sourceWOComments = sourceWOComments.Replace("\t", " ");

                    AddLineKBInterfazSource(writer, obj, "SHELL ", "CMD.", sourceWOComments, mainss);
                    AddLineKBInterfazSource(writer, obj, "JAVA ", "CMD.", sourceWOComments, mainss);
                    AddLineKBInterfazSource(writer, obj, "CSHARP ", "CMD.", sourceWOComments, mainss);
                    AddLineKBInterfazSource(writer, obj, "SQL ", "CMD.", sourceWOComments, mainss);


                    ObjectsVariablesExternal(obj, writer, mainss);
                    if (obj is Transaction || obj is WebPanel)
                        UserControlUsageCheck(obj, writer, mainss);

                }
            }

            writer.AddFooter();
            writer.Close();

            KBDoctorHelper.ShowKBDoctorResults(outputFile);
            bool success = true;
            output.EndSection(titulo, success);

        }

        private static void AddLineKBInterfazSource( KBDoctorXMLWriter writer, KBObject obj, string texto, string tipo, string sourceWOComments, string mainss)
        {
            string callTree = "";
            KBObjectCollection objColl = new KBObjectCollection();

            if (sourceWOComments.Contains(texto))
            {
                writer.AddTableData(new string[] { Functions.linkObject(obj), "", "CMD." + texto, mainss });
            }

        }

        public static void ObjectsVariablesExternal(KBObject obj,KBDoctorXMLWriter writer, string mainss)
        {
            IKBService kbserv = UIServices.KB;

            string type = "";
            string name = "";
            string variables = "";


            if (KBDoctorCore.Sources.Utility.isGenerated(obj))
            {
                VariablesPart vp = obj.Parts.Get<VariablesPart>();
                if (vp != null)
                {
                    foreach (Variable v in vp.Variables)
                    {
                        type = v.Type.ToString();
                        name = v.Name;

                        if ((!v.IsStandard) && (v.AttributeBasedOn == null) && (type == "GX_USRDEFTYP")) //|| (type==) || (type == "GX_BUSCOMP_LEVEL") )
                        {
                            variables += name + " " + type + " ";
                            string txtDimensions = v.GetPropertyValue<string>(Properties.ATT.DataTypeString);
                            writer.AddTableData(new string[] { Functions.linkObject(obj), name, "Data Type." + txtDimensions, mainss });

                        }


                    }

                }

            }




        }


        public static void UserControlUsageCheck(KBObject obj,KBDoctorXMLWriter writer, string mainss)
        {

            Artech.Genexus.Common.Parts.WebFormPart webpart = obj.Parts.Get<Artech.Genexus.Common.Parts.WebFormPart>();

            if (webpart != null)
            {
                foreach (Artech.Genexus.Common.Parts.WebForm.IWebTag tag in Artech.Genexus.Common.Parts.WebForm.WebFormHelper.EnumerateWebTag(webpart))
                {
                    if (tag.IsUserControl)
                    {
                        string ucType = tag.Properties.GetPropertyValueString("UserControlType");
                        string ctrlName = tag.Properties.GetPropertyValueString("ControlName");
                        writer.AddTableData(new string[] { Functions.linkObject(obj), ctrlName, "UC." + ucType, mainss });
                    }
                }
            }
        }




    }
}