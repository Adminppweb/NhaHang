﻿/****************************** Module Header ******************************\
Module Name:  PivotRow.cs
Project:      CSEFPivotOperation
Copyright (c) Microsoft Corporation.

This sample demonstrates how to implement the Pivot and Unpivot operation in 
Entity Framework.
This file includes the definition of PivotRow class that stores the Pivot 
result.

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/en-us/openness/licenses.aspx#MPL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NhaHang.Infrastructures.Commons
{
    /// <summary>
    /// Store the row of the Pivot result.
    /// </summary>
    /// <typeparam name="TypeId">Type of ObjectId</typeparam>
    /// <typeparam name="TypeAttr">Type of Attribute</typeparam>
    /// <typeparam name="TypeValue">Type of Value</typeparam>
    public class PivotRow<TypeId, TypeAttr, TypeValue>
    {
        public TypeId ObjectId { get; set; }
        public IEnumerable<TypeAttr> Attributes { get; set; }
        public IEnumerable<TypeValue> Values { get; set; }
        /// <summary>
        /// Get the Pivot table
        /// </summary>
        /// <param name="attributeNames">the list of attributes</param>
        /// <param name="source">the data of Pivot source</param>
        /// <returns>the Pivot table</returns>
        public static DataTable GetPivotTable(List<TypeAttr> attributeNames,
            List<PivotRow<TypeId, TypeAttr, TypeValue>> source)
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("ID", typeof(TypeId));
            dt.Columns.Add(dc);
            // Creat the new DataColumn for each attribute
            attributeNames.ForEach(name =>
            {
                dc = new DataColumn(name.ToString(), typeof(TypeValue));
                dt.Columns.Add(dc);
            });
            // Insert the data into the Pivot table
            foreach (PivotRow<TypeId, TypeAttr, TypeValue> row in source)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = row.ObjectId;
                List<TypeAttr> attributes = row.Attributes.ToList();
                List<TypeValue> values = row.Values.ToList();
                // Set the value basing the attribute names.
                for (int i = 0; i < values.Count; i++)
                {
                    dr[attributes[i].ToString()] = values[i];
                }
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
    public partial class PivotRowSize
    {
        public decimal? Size00 { get; set; }
        public decimal? Size29 { get; set; }
        public decimal? Size30 { get; set; }
        public decimal? Size31 { get; set; }
        public decimal? Size32 { get; set; }
        public decimal? Size33 { get; set; }
        public decimal? Size34 { get; set; }
        public decimal? Size35 { get; set; }
        public decimal? Size36 { get; set; }
        public decimal? Size37 { get; set; }
        public decimal? Size38 { get; set; }
        public decimal? Size39 { get; set; }
        public decimal? Size40 { get; set; }
        public decimal? Size41 { get; set; }
        public decimal? Size42 { get; set; }
        public decimal? Size43 { get; set; }
        public decimal? Size44 { get; set; }
        public decimal? Size45 { get; set; }
        public decimal? Size46 { get; set; }
        public decimal? SoLuong { get; set; }
    }
}
