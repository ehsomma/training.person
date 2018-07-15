// ReSharper disable once CheckNamespace
namespace Newtonsoft.Json.Linq
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using global::Newtonsoft.Json.Linq;

    /// <summary>Extension para Json.Net.</summary>
    public static class JObjectExtensions
    {
        #region Methods

        /// <summary>Convert JObject into Dictionary.</summary>
        /// <param name="obj">Clase a extender.</param>
        /// <param name="name">No tengo idea!.</param>
        /// <returns>Un Dictionary.</returns>
        /// <remarks>
        /// Source: http://stackoverflow.com/questions/14886800/convert-jobject-into-dictionarystring-object-is-it-possible.
        /// </remarks>
        [SuppressMessage("Apollo.StyleCop.ApolloRules.ReadabilityRules", "SC2101:FunctionMustHaveOnlyOneReturn", Justification = "Usa lambda.")]
        public static Dictionary<string, object> Bagify(this JToken obj, string name = null)
        {
            name = name ?? "obj";
            JObject jobojectAux = obj as JObject;
            if (jobojectAux != null)
            {
                IEnumerable<KeyValuePair<string, object>> asBag = from prop in jobojectAux.Properties()
                                                                  let propName = prop.Name
                                                                  let propValue =
                                                                      prop.Value is JValue
                                                                          ? new Dictionary<string, object>()
                                                                                {
                                                                                    {
                                                                                        prop.Name,
                                                                                        prop.Value
                                                                                    }
                                                                                }
                                                                          : prop.Value.Bagify(prop.Name)
                                                                  select
                                                                      new KeyValuePair<string, object>(
                                                                      propName,
                                                                      propValue);
                return asBag.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            if (obj is JArray)
            {
                IJEnumerable<JToken> vals = (obj as JArray).Values();
                object[] alldicts = vals.SelectMany(val => val.Bagify(name)).Select(x => x.Value).ToArray();
                return new Dictionary<string, object>() { { name, (object)alldicts } };
            }

            if (obj is JValue)
            {
                return new Dictionary<string, object>() { { name, (obj as JValue) } };
            }

            return new Dictionary<string, object>() { { name, null } };
        }

        #endregion
    }
}