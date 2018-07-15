// ReSharper disable once CheckNamespace
namespace System.Xml.XPath
{
    using System;

    /// <summary>
    /// Extensiones para facilitar el uso de XPtah.
    /// </summary>
    public static class XPathNavigatorExtension
    {
        #region Methods

        /// <summary>
        /// Devuelve el valor del primer noso hijo solicitado.
        /// </summary>
        /// <param name="navigator">Navegador del documento XML.</param>
        /// <param name="child">Nodo hijo solicitado.</param>
        /// <returns>El valor del nodo hijo solicitado.</returns>
        public static string GetChildValue(this XPathNavigator navigator, string child)
        {
            if (navigator == null)
            {
                throw new ArgumentNullException(nameof(navigator));
            }

            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (child == string.Empty)
            {
                throw new ArgumentException(nameof(child));
            }

            XPathNodeIterator children = navigator.SelectChildren(child, string.Empty);
            children.MoveNext();
            return children.Current.Value;
        }

        #endregion
    }
}
