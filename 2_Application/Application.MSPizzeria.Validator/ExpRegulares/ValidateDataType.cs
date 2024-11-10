using System.Globalization;
using System.Text.RegularExpressions;

namespace Application.MSPizzeria.Validator.ExpRegulares;

public static class ValidateDataType
    {
        #region SOLO CARACTERES ALFABETICOS
        public static bool ContieneSoloCaracteresAlfabeticos(string sCandena)
        {
            #region VARIABLES LOCALES
            var bResp = false;
            #endregion

            try
            {
                var regex = new Regex("^[a-zA-Z]+$");
                if (regex.IsMatch(sCandena))
                {
                    bResp = true;
                }
            }
            #pragma warning disable 168
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception jj)
            // ReSharper restore EmptyGeneralCatchClause
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region SOLO CARACTERES NUMERICOS
        public static bool ContieneSoloCaracteresNumericos(string sCandena)
        {
            #region VARIABLES LOCALES
            var bResp = false;
            #endregion

            try
            {
                var regex = new Regex("^[0-9]+$");
                if (regex.IsMatch(sCandena))
                {
                    bResp = true;
                }
            }
            #pragma warning disable 168
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception jj)
            // ReSharper restore EmptyGeneralCatchClause
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region VALIDAR EXPRESION REGULAR
        public static bool ExpresionRegular(string sCandena, string sExpresionRegular)
        {
            #region VARIABLES LOCALES
            bool bResp = false;
            #endregion

            try
            {
                var regex = new Regex(sExpresionRegular);
                if (regex.IsMatch(sCandena))
                {
                    bResp = true;
                }
            }
            #pragma warning disable 168
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception jj)
            // ReSharper restore EmptyGeneralCatchClause
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region SOLO FECHAS
        public static bool EsFecha(string sCadena)
        {
            #region VARIABLES LOCALES
            var bResp = false;
            #endregion
            try
            {
                DateTime dtFecha;
                CultureInfo culture;
                DateTimeStyles styles;
                culture = new CultureInfo("en-US");
                styles = DateTimeStyles.None;
                bResp = DateTime.TryParse(sCadena, culture, styles, out dtFecha);
            }
            #pragma warning disable 168
            catch (Exception jj)
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region VALORES DECIMALES
        public static bool EsDecimal(string sCadena)
        {
            #region VARIABLES LOCALES
            var bResp = false;
            #endregion
            try
            {
                double isItNumeric;
                bResp = Double.TryParse(Convert.ToString(sCadena), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out isItNumeric);
            }
            #pragma warning disable 168
            catch (Exception jj)
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region VALORES ENTEROS
        public static bool EsEntero(string sCadena)
        {
            #region VARIABLES LOCALES
            var bResp = false;
            #endregion
            try
            {
                var i = 0;
                bResp = int.TryParse(sCadena, out i);
            }
            #pragma warning disable 168
            catch (Exception jj)
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region VALORES ALFANUMERICOS
        public static bool ContieneValoresAlfaNumericos(string sCandena)
        {
            #region VARIABLES LOCALES
            bool bResp = false;
            #endregion

            try
            {
                var regex = new Regex("^[A-Z0-9 a-z]*$");
                if (regex.IsMatch(sCandena))
                {
                    bResp = true;
                }
            }
            #pragma warning disable 168
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception jj)
            // ReSharper restore EmptyGeneralCatchClause
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        #region SOLO DIRECCIONES DE CORREO ELECTRONICO
        public static bool ContieneUnaDireccionDeCorreo(string sCandena)
        {
            #region VARIABLES LOCALES
            bool bResp = false;
            #endregion

            try
            {
                var regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (regex.IsMatch(sCandena))
                {
                    bResp = true;
                }
            }
            #pragma warning disable 168
            // ReSharper disable EmptyGeneralCatchClause
            catch (Exception jj)
            // ReSharper restore EmptyGeneralCatchClause
            #pragma warning restore 168
            {
                bResp = false;
            }
            return bResp;
        }
        #endregion

        //FIN
    }