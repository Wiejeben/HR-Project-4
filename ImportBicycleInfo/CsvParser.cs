﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImportBicycleInfo
{
    class CsvParser
    {
        private string FileName;
        private int SkipRows;
        private List<string[]> Rows = new List<string[]>();

        public CsvParser(string fileName, int skipRows = 0)
        {
            this.FileName = fileName;
            this.SkipRows = skipRows;
            this.Parse();
        }

        private void Parse()
        {
            using (TextFieldParser parser = new TextFieldParser(Path.GetFullPath(@"Resources\" + this.FileName +  ".csv")))
            {
                // Set delimiters
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                int currentRow = 0;

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (currentRow >= this.SkipRows)
                        this.Rows.Add(fields);

                    currentRow++;
                }
            }
        }

        public List<BikeTheft> ParseBikeTheft()
        {
            List<BikeTheft> thefts = new List<BikeTheft>();

            foreach (var row in this.Rows)
            {
                if (BikeTheft.ValidCSVRow(row))
                    thefts.Add(BikeTheft.ParseCSVRow(row));
            }

            return thefts;
        }

        public List<BikeContainer> ParseBikeContainer()
        {
            List<BikeContainer> thefts = new List<BikeContainer>();

            foreach (var row in this.Rows)
            {
                if (BikeContainer.ValidCSVRow(row))
                    thefts.Add(BikeContainer.ParseCSVRow(row));
            }

            return thefts;
        }
    }
}
