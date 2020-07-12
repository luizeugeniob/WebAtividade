namespace Helpers
{
    public static class TextoHelper
    {
        /// <summary>
        /// Remove caracteres não numéricos.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string texto)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            return reg.Replace(texto, string.Empty);
        }
    }
}
