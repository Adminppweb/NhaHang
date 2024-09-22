namespace NhaHang.Infrastructure.Commons
{
    public enum TypeFilter
    {

        //
        // Informational 1xx
        //
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Category = 1,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Rating = 2,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Price = 3,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Attribute = 4,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Brand = 5,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Size = 6,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Color = 7,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Discount = 8,
        /// <summary>
        /// Bảo hành
        /// </summary>
        WarrantyPeriod = 9,

        ProductAttributeGroup = 10
    }; //enum TypeFilter,


    public enum TypeFilterNews
    {

        //
        // Informational 1xx
        //
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Category = 1,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Rating = 2,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Price = 3,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Attribute = 4,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Brand = 5,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Size = 6,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Color = 7,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Discount = 8
    }; //enum TypeFilter

    public enum PositionFilter
    {

        //
        // Informational 1xx
        //
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Top = 1,
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        Aside = 2,

        Layout1 = 4,
        Layout2 = 5,
        Layout3 = 6,
        Layout4 = 7
    }

    public enum Breadcrumb
    {

        Category,
        News,
        Blog
    }

    /// <summary>
    /// Author: Pham Duy Tan
    /// CreatedOn: 17/05/2023
    /// Dủng để quản lý tạp chí - Tin tức
    /// 
    /// </summary>
    public enum TypeBlog
    {
        News,
        KNowledge,
        Trend,
        Style,
        Share
    }
}
