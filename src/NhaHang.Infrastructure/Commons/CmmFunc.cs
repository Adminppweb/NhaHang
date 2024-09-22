using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NhaHang.Infrastructure.Commons
{
    public class CmmFunc
    {
        /// <summary>
        /// Lấy giá trị của enum 
        /// </summary>
        /// <param name="value">Enum: Đối tượng enum</param>
        /// <returns>string: Chuỗi giá trị</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Lấy danh sách item enum
        /// </summary>
        /// <typeparam name="T">T: Đối tượng enum</typeparam>
        /// <returns>Danh sách enum</returns>
        public static IEnumerable<T> EnumToList<T>()
        {
            Type enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            Array enumValArray = Enum.GetValues(enumType);
            List<T> enumValList = new List<T>(enumValArray.Length);

            foreach (int val in enumValArray)
            {
                enumValList.Add((T)Enum.Parse(enumType, val.ToString()));
            }

            return enumValList;
        }

        /// <summary>
        /// Convert string to enum
        /// </summary>
        /// <typeparam name="T">Class enum cần convert</typeparam>
        /// <param name="strValue">Giá trị chuỗi</param>
        /// <returns>Trả thành giá trị là kiểu enum</returns>
        public static T ParseEnum<T>(string strValue)
        {
            return (T)Enum.Parse(typeof(T), strValue, true);
        }

        /// <summary>
        /// Get Property của Object dựa vào tên
        /// </summary>
        /// <param name="obj">Đối tường chứa property</param>
        /// <param name="strPropName">Tên Property</param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(object obj, string strPropName)
        {
            Type type = obj.GetType();
            return type.GetProperty(strPropName);
        }

        /// <summary>
        /// Set giá trị cho thuộc tính của Object dựa vào Tên Field
        /// </summary>
        /// <param name="obj">Object muốn set giá trị</param>
        /// <param name="strPropName">Tên thuộc tính muốn set giá trị</param>
        /// <param name="value">Giá trị muốn set</param>
        public static void SetPropertyValueByName(object obj, string strPropName, object value)
        {
            try
            {
                PropertyInfo propInfo = GetProperty(obj, strPropName);
                if (propInfo != null)
                {
                    Type t = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                    propInfo.SetValue(obj, safeValue, null);
                }
            }
            catch (Exception ex)
            {
                ////SPListUtilities.TrackingError("Author: hunglt - Method: SetPropertyValueByName - File: CmmFunc - Project: VuThao.Uniben.Common", "obj: " + obj.ToString() + " - strPropName: " + strPropName + " - value: " + value + " - Error: " + ex.ToString());
            }
        }

        public static void Base64Decode(ref string strPostData)
        {
            if ((strPostData.Length % 4) != 0)
                return;
            // Check that the string matches the base64 layout
            Regex regex = new Regex(@"[^-A-Za-z0-9+/=]|=[^=]|={3,}$");
            if (regex.Match(strPostData).Success)
            {
                return;
            }
            try
            {
                // If not exception is cought, then it is a base64 string
                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strPostData));
                strPostData = Encoding.UTF8.GetString(Convert.FromBase64String(strPostData));
                return;
            }
            catch
            {
            }
        }

        /////<summary> Get string from IDictionary<string, object></summary>
        /// <summary>
        /// string Value { get; set; }
        /// </summary>
        ///public string DisplayType { get; set; }
        public static ObjectData GetObjectDictionaryValue<T>(IDictionary<string, T> Object, string key)
        {
            var returnValue = new ObjectData();
            try
            {
                if (Object[key] != null)
                {
                    var keyValue = Object.FirstOrDefault(x => x.Key == key);
                    returnValue = new ObjectData
                    {
                        Key = key,
                        Data = keyValue.Value
                    };
                }
            }
            catch (Exception ex)
            {
                returnValue = new ObjectData
                {
                    Key = key,
                    Data = null
                };
                Console.WriteLine(ex.ToString());
            }
            return returnValue;
        }

    }

    public class ObjectData
    {
        public string Key { get; set; }
        public Object Data { get; set; }
    }
}
