﻿using System;
using System.Configuration;

namespace DeviceTestReporting
{
    public class Globals
    {
        public static string ProductionDBURI
        {
            get
            {
                string db = null;

                try
                {
                    if (ConfigurationManager.AppSettings["db_production_uri"] != null)
                        db = ConfigurationManager.AppSettings["db_production_uri"];
                }
                catch (Exception)
                {

                }
                return db;
            }
        }

        public static string DevelopmentDBURI
        {
            get
            {
                string db = null;

                try
                {
                    if (ConfigurationManager.AppSettings["db_development_uri"] != null)
                        db = ConfigurationManager.AppSettings["db_development_uri"];
                }
                catch (Exception)
                {

                }
                return db;
            }
        }

    }
}
