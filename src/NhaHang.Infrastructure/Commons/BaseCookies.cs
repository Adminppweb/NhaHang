namespace NhaHang.Infrastructure.Commons
{
    public class BaseCookies
    {
        #region HttpCookieCollection
        ////public static string TryGetCookie(this HttpCookieCollection cookies, string cookieName, bool readOneTime = false)
        ////{
        ////    string returnValue = string.Empty;
        ////    try
        ////    {
        ////        #region DefaultCookie
        ////        Action<string> getCookieValueAsDefault = delegate (string oCookieName)
        ////        {
        ////            if (cookies.AllKeys.Contains(oCookieName))
        ////            {
        ////                returnValue = cookies[oCookieName].Value;
        ////                returnValue = returnValue.UrlDecode();
        ////                if (readOneTime)
        ////                {
        ////                    HttpContext.Current.Response.Cookies[oCookieName].Expires = DateTime.Now.AddDays(-1);
        ////                }
        ////            }
        ////            else
        ////            {
        ////                returnValue = string.Empty;
        ////            }
        ////        };
        ////        #endregion

        ////        #region Multi part cookie
        ////        Action<string> getCookieValueMultiPart = delegate (string oCookieName)
        ////        {
        ////            int currentPart = 0;
        ////            string cookieValue = string.Empty;
        ////            bool allowNext = true;
        ////            while (allowNext)
        ////            {
        ////                string currentCookieName = oCookieName + "_Part" + currentPart;
        ////                getCookieValueAsDefault(currentCookieName);

        ////                if (!string.IsNullOrEmpty(returnValue))
        ////                {
        ////                    cookieValue += returnValue;
        ////                }
        ////                else
        ////                {
        ////                    allowNext = false;
        ////                }
        ////                currentPart++;
        ////            }
        ////            returnValue = cookieValue;
        ////        };
        ////        #endregion

        ////        getCookieValueAsDefault(cookieName);
        ////        if (string.IsNullOrEmpty(returnValue))
        ////        {
        ////            getCookieValueMultiPart(cookieName);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {

        ////    }
        ////    return returnValue;
        ////}
        #endregion
    }
}
