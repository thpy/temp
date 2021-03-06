﻿using System;
using System.IO;
using System.Collections.Generic;

namespace Indihiang.Cores.Features
{
    public class GeneralFeature1 : BaseLogAnalyzeFeature
    {
        public GeneralFeature1(EnumLogFile logFile)
            : base(logFile)
        {
            _featureName = LogFeature.GENERAL;

            _logs.Add("General", new LogCollection());
            _logs.Add("IPServer", new LogCollection());
            _logs.Add("TotalData", new LogCollection());
            
        }
        protected override bool RunFeature(List<string> header, string[] item)
        {
            switch (_logFile)
            {
                case EnumLogFile.NCSA:
                    break;
                case EnumLogFile.MSIISLOG:
                    break;
                case EnumLogFile.W3CEXT:
                    RunW3cext(header, item);
                    break;
            }

            return true;
        }
        protected override bool RunFeature(string id, List<string> header, string[] item)
        {
            switch (_logFile)
            {
                case EnumLogFile.NCSA:
                    break;
                case EnumLogFile.MSIISLOG:
                    break;
                case EnumLogFile.W3CEXT:
                    RunW3cext(header, item);
                    break;
            }

            return true;
        }

        private void RunW3cext(List<string> header, string[] item)
        {
            int index = header.IndexOf("date");
            int index2 = header.IndexOf("s-ip");
           
            if (index == -1 || index2 == -1)
                return;

            string key = item[index];
            string key2 = item[index2];

            try
            {
                if (!string.IsNullOrEmpty(key) && key != "-")
                {
                    if (!_logs["General"].Colls.ContainsKey(key))
                        _logs["General"].Colls.Add(key, new WebLog(key, ""));

                    if (_logs["TotalData"].Colls.ContainsKey("TotalData"))
                    {
                        int val = Convert.ToInt32(_logs["TotalData"].Colls["TotalData"].Items["TotalData"]);
                        val++;
                        _logs["TotalData"].Colls["TotalData"].Items["TotalData"] = val.ToString();
                    }
                    else
                    {
                        _logs["TotalData"].Colls.Add("TotalData", new WebLog("TotalData", "1"));
                    }
                }
                if (!string.IsNullOrEmpty(key2) && key2 != "-")
                {
                    if (!_logs["IPServer"].Colls.ContainsKey(key2))
                        _logs["IPServer"].Colls.Add(key2, new WebLog(key2, ""));
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("GeneralFeature Error: {0}", err.Message));
            }            
        }
        private void RunW3cext(string id,List<string> header, string[] item)
        {
            int index = header.IndexOf("date");
            int index2 = header.IndexOf("s-ip");

            if (index == -1 || index2 == -1)
                return;

            string key = item[index];
            string key2 = item[index2];

            try
            {
                string path = String.Format("{0}\\Temp\\{1}\\", Environment.CurrentDirectory,id);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                if (!string.IsNullOrEmpty(key) && key != "-")
                {
                    FeatureLogFile logFile = new FeatureLogFile();

                    string generalFile = String.Format("{0}{1}-General.tmp", path, _featureName.ToString());                    
                    logFile.LogFile = generalFile;
                    logFile.InsertUniqueData(key);


                    string totalFile = String.Format("{0}{1}-TotalData.tmp", path, _featureName.ToString());
                    logFile.LogFile = totalFile;
                    logFile.UpdateCount("total", 1);

                }
                if (!string.IsNullOrEmpty(key2) && key2 != "-")
                {
                    FeatureLogFile logFile = new FeatureLogFile();
                    string serverFile = String.Format("{0}{1}-IPServer.tmp", path, _featureName.ToString());

                    logFile.LogFile = serverFile;
                    logFile.InsertUniqueData(key2);              
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("GeneralFeature Error: {0}", err.Message));
            }
        }

        protected override bool RunSynchFeatureData(Dictionary<string, LogCollection> newItem)
        {
            bool success = false;

            try
            {
                foreach (KeyValuePair<string, LogCollection> pair in newItem)
                {
                    if (pair.Key == "General")
                    {
                        foreach (KeyValuePair<string, WebLog> pair2 in pair.Value.Colls)
                        {
                            if (!_logs[pair.Key].Colls.ContainsKey(pair2.Key))
                                _logs[pair.Key].Colls.Add(pair2.Key, new WebLog(pair2.Key, ""));
                        }
                    }
                    if (pair.Key == "IPServer")
                    {
                        foreach (KeyValuePair<string, WebLog> pair2 in pair.Value.Colls)
                        {
                            if (!_logs[pair.Key].Colls.ContainsKey(pair2.Key))
                                _logs[pair.Key].Colls.Add(pair2.Key, new WebLog(pair2.Key, ""));
                        }
                    }
                    if (pair.Key == "TotalData")
                    {
                        foreach (KeyValuePair<string, WebLog> pair2 in pair.Value.Colls)
                        {
                            if (_logs["TotalData"].Colls.ContainsKey(pair2.Key))
                            {
                                foreach (KeyValuePair<string, string> pair3 in pair2.Value.Items)
                                {
                                    if (_logs["TotalData"].Colls[pair2.Key].Items.ContainsKey(pair3.Key))
                                    {
                                        int val1 = Convert.ToInt32(_logs["TotalData"].Colls[pair2.Key].Items[pair3.Key]);
                                        int val2 = Convert.ToInt32(pair3.Value);

                                        _logs["TotalData"].Colls[pair2.Key].Items[pair3.Key] = Convert.ToString(val1 + val2);
                                    }
                                    else
                                        _logs["TotalData"].Colls[pair2.Key].Items.Add(pair3.Key, pair3.Value);
                                }
                            }
                            else
                            {
                                _logs["TotalData"].Colls.Add(pair2.Key, pair2.Value);
                            }
                        }
                    }
                }
                success = true;
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Error Synch: {0}", err.Message));
            }

            return success;
        }

        protected override void DumpToFile(StreamWriter sw)
        {
            try
            {
                foreach (KeyValuePair<string, LogCollection> pair in _logs)
                {
                    if (pair.Key == "General" || pair.Key == "IPServer")
                    {
                        foreach (KeyValuePair<string, WebLog> pair2 in pair.Value.Colls)
                        {
                            string data = String.Format("#{0};{1}", pair.Key, pair2.Key);
                            sw.WriteLine(data);
                        }
                    }
                    if (pair.Key == "TotalData")
                    {
                        foreach (KeyValuePair<string, WebLog> pair2 in pair.Value.Colls)
                        {
                            if (_logs["TotalData"].Colls.ContainsKey(pair2.Key))
                            {
                                foreach (KeyValuePair<string, string> pair3 in pair2.Value.Items)
                                {
                                    string data = String.Format("#{0};{1};{2}", pair.Key, pair2.Key, pair3.Value);
                                    sw.WriteLine(data);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Error DumpToFile: {0}", err.Message));
            }
        }

        
    }
}
