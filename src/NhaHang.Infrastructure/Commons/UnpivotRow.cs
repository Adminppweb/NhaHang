/****************************** Module Header ******************************\
Module Name:  UnpivotRow.cs
Project:      CSEFPivotOperation
Copyright (c) Microsoft Corporation.

This sample demonstrates how to implement the Pivot and Unpivot operation in 
Entity Framework.
This file includes the definition of UnpivotRow class that stores the Unpivot 
result.

This source is subject to the Microsoft Public License.
See http://www.microsoft.com/en-us/openness/licenses.aspx#MPL.
All other rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

namespace NhaHang.Infrastructure.Commons
{
    /// <summary>
    /// Store the row of the Unpivot result.
    /// </summary>
    /// <typeparam name="TypeId">type of ObjectId</typeparam>
    /// <typeparam name="TypeAttr">type of Attribute</typeparam>
    /// <typeparam name="TypeValue">type of Value</typeparam>
    public class UnpivotRow<SoHoaDon, Kieu, Size, Soluong>
    {
        public SoHoaDon soHoaDon { get; set; }
        public Kieu kieu { get; set; }
        public Size size { get; set; }
        public Soluong soluong { get; set; }
    }
    //sửa 19/12

    //public class UnpivotRow_KT<_Id,_SoHoaDon, _Kieu, _Size, _SoLuong>
    //{
    //    public _Id Id { get; set; }
    //    public _SoHoaDon SoHoaDon { get; set; }
    //    public _Kieu Kieu { get; set; }
    //    public _Size Size { get; set; }
    //    public _SoLuong SoLuong { get; set; }
    //}
    public class UnpivotRow_KT<_Id, _SoHoaDon, _Kieu, _Size, _SoLuong, _GiaNhap, _GiaXuat>
    {
        public _Id Id { get; set; }
        public _SoHoaDon SoHoaDon { get; set; }
        public _Kieu Kieu { get; set; }
        public _Size Size { get; set; }
        public _SoLuong SoLuong { get; set; }
        public _GiaNhap GiaNhap { get; set; } //19/02
        public _GiaXuat GiaXuat { get; set; } //19/02
    }
    public class UnpivotRow_KhoVM<_Id, _NoiNhan, _Kieu, _Size, _SoLuong, _GiaNhap, _GiaXuat>
    {
        public _Id Id { get; set; }
        public _NoiNhan NoiNhan { get; set; }
        public _Kieu Kieu { get; set; }
        public _Size Size { get; set; }
        public _SoLuong SoLuong { get; set; }
        public _GiaNhap GiaNhap { get; set; } //19/02
        public _GiaXuat GiaXuat { get; set; } //19/02


    }

    public class UnpivotRow_KhoSale<_Id, _SoHoaDon, _Kieu, _Size, _SoLuong, _GiaNhap, _GiaXuat>
    {
        public _Id Id { get; set; }
        public _SoHoaDon SoHoaDon { get; set; }
        public _Kieu Kieu { get; set; }
        public _Size Size { get; set; }
        public _SoLuong SoLuong { get; set; }
        public _GiaNhap GiaNhap { get; set; } //19/02
        public _GiaXuat GiaXuat { get; set; } //19/02
    }


    public class UnpivotRow_KhoSaleVM<_Id, _NoiNhan, _Kieu, _Size, _SoLuong, _GiaNhap, _GiaXuat>
    {
        public _Id Id { get; set; }
        public _NoiNhan NoiNhan { get; set; }
        public _Kieu Kieu { get; set; }
        public _Size Size { get; set; }
        public _SoLuong SoLuong { get; set; }
        public _GiaNhap GiaNhap { get; set; } //19/02
        public _GiaXuat GiaXuat { get; set; } //19/02


    }


    public class UnpivotRow_KhoSi<Kieu, Size, Soluong>
    {
        public Kieu kieu { get; set; }
        public Size size { get; set; }
        public Soluong soluong { get; set; }
    }

    public class UnpivotRow_KhoTong<Kieu, Size, Soluong, Id>
    {
        public Kieu kieu { get; set; }
        public Size size { get; set; }
        public Soluong soluong { get; set; }
        public Id id { get; set; }
    }
    // Phân Thùng
    public class UnpivotRow_PhanThung<VungMien, Kieu, Size, Soluong>
    {
        public VungMien vungMien { get; set; }
        public Kieu kieu { get; set; }
        public Size size { get; set; }
        public Soluong soluong { get; set; }
    }





}
