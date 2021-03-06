﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using VisioAutomation.ShapeSheet;

namespace VisioAutomation.Scripting.ShapeSheet
{
    public class CellValueDictionary : NameDictionary<string>
    {
        private readonly CellSRCDictionary srcmap;

        public CellValueDictionary(CellSRCDictionary srcmap, Dictionary<string,string> dictionary)
        {
            if (srcmap == null)
            {
                throw new System.ArgumentNullException(nameof(srcmap));
            }

            this.srcmap = srcmap;

            this.UpdateFrom(dictionary);
        }


        public SRC GetSRC(string name)
        {
            return this.srcmap[name];
        }

        public void UpdateFrom(Dictionary<string,string> from_dic)
        {
            if (from_dic == null)
            {
                throw new System.ArgumentNullException(nameof(from_dic));
            }

            // We are certain all the keys are strings
            foreach (var pair in from_dic)
            {
                string cellname = pair.Key;
                this.UpdateFrom(cellname, pair.Value);
            }
        }

        public void UpdateFrom(string cellname,string cellvalue)
        {
            if (!this.srcmap.ContainsKey(cellname))
            {
                string message = string.Format("Cell \"{0}\" is not supported", cellname);
                throw new System.ArgumentOutOfRangeException(message);
            }

            if (cellvalue == null)
            {
                string message = string.Format("Cell {0} has a null value. Use a non-null value", cellname);
                throw new System.ArgumentOutOfRangeException(message);
            }

            this[cellname] = cellvalue;
        }
    }
}