using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KainLabs.Siren
{
    public class EmbeddedSubEntity : SubEntity
    {
        public EmbeddedSubEntity()
        {
            Class = null;
            Properties = null;
            Entities = null;
            Links = null;
            Actions = null;
        }

        public string Title { get; set; }

        public IEnumerable<string> Class { get; set; }
        public IEnumerable<string> Rel { get; set; }

        /// <summary>
        /// Gets or sets the list of properties.
        /// </summary>
        /// <remarks>
        /// A set of key-value pairs that describe the state of an entity. Optional.
        /// </remarks>
        public JObject Properties { get; set; }

        /// <summary>
        /// Gets or sets related entities.
        /// </summary>
        /// <remarks>
        /// A collection of related sub-entities. 
        /// If a sub-entity contains an href value, it should be treated as an embedded link. 
        /// Clients may choose to optimistically load embedded links. If no href value exists, 
        /// the sub-entity is an embedded entity representation that contains all the 
        /// characteristics of a typical entity. One difference is that a sub-entity 
        /// MUST contain a rel attribute to describe its relationship to the parent entity.
        /// In JSON Siren, this is represented as an array. Optional.
        /// </remarks>
        public IEnumerable<SubEntity> Entities { get; set; }

        /// <summary>
        /// Gets or sets a list of links associated with the entity.
        /// </summary>
        /// <remarks>
        /// A collection of items that describe navigational links, distinct from entity relationships. 
        /// Link items should contain a rel attribute to describe the relationship and an href attribute 
        /// to point to the target URI. Entities should include a link rel to self. In JSON Siren, this is 
        /// represented as "links": [{ "rel": ["self"], "href": "http://api.x.io/orders/1234" }] Optional.
        /// </remarks>
        public IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Gets or sets a list of actions associated with the entity.
        /// </summary>
        /// <remarks>
        /// A collection of action objects, represented in JSON Siren as an array 
        /// such as { "actions": [{ ... }] }. See Actions. Optional
        /// </remarks>
        public IEnumerable<Action> Actions { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Returns the associated Uri for the first Link matching the Relationship.
        /// </summary>
        /// <param name="relationship">A <see cref="string"/> value identifying the Relationship.</param>
        /// <returns>A <see cref="Uri"/> representing the related Link.</returns>
        public Uri GetLinkUri(string relationship)
        {
            var link = Links.FirstOrDefault(x => x.Rel.Any(y => y.ToLower().EndsWith(relationship)));

            if (link == null)
            {
                throw new ApplicationException(string.Format("Invalid Siren relationship: {0}", relationship));
            }

            var url = link.Href.ToString();
            var uri = new Uri(url);
            return uri;
        }

        /// <summary>
        /// Returns the associated Uri for the first Embedded SubEntity matching the Relationship.
        /// </summary>
        /// <param name="relationship">A <see cref="string"/> value identifying the Relationship.</param>
        /// <returns>A <see cref="Uri"/> representing the related Embedded SubEntity.</returns>
        public Uri GetEmbeddedSubEntityUri(string relationship)
        {
            var childEntity = Entities.FirstOrDefault(x => ((EmbeddedSubEntity)x).Rel.Any(y => y.ToLower().EndsWith(relationship))) as EmbeddedSubEntity;

            if (childEntity == null)
            {
                throw new ApplicationException(string.Format("Invalid Siren relationship: {0}", relationship));
            }

            var url = childEntity.ExtensionData["href"].ToString();
            var uri = new Uri(url);
            return uri;
        }

        /// <summary>
        /// Returns the associated Uri for the first Action matching the name.
        /// </summary>
        /// <param name="name">A <see cref="string"/> value representing the Action's name.</param>
        /// <returns>A <see cref="Uri"/> representing the Action.</returns>
        public Uri GetActionUri(string name)
        {
            var action = Actions.FirstOrDefault(x => x.Name == name);

            if (action == null)
            {
                throw new ApplicationException(string.Format("Invalid Siren action: {0}", name));
            }

            var url = action.Href.ToString();
            var uri = new Uri(url);
            return uri;
        }

        /// <summary>
        /// Returns the value of the field associated with the first Action matching the name.
        /// </summary>
        /// <param name="actionName">A <see cref="string"/> value representing the Action's name.</param>
        /// <param name="filedName">A <see cref="string"/> value representing the field's name.</param>
        /// <returns>A <see cref="tring"/> representing the value of the field.</returns>
        public string GetFieldValueForAction(string actionName, string fieldName)
        {
            var action = Actions.FirstOrDefault(x => x.Name == actionName);

            if (action == null)
            {
                throw new ApplicationException(string.Format("Invalid Siren action: {0}", actionName));
            }

            var field = action.Fields.FirstOrDefault(x => x.Name == fieldName);

            if (field == null)
            {
                throw new ApplicationException(string.Format("Invalid Field name: {0}", fieldName));
            }

            var value = field.Value.ToString();
            return value;
        }

        /// <summary>
        /// Returns whether or not the class indicates it matches the specified classId.
        /// </summary>
        /// <param name="classId">A <see cref="string"/> value identifying the class type.</param>
        /// <returns>A <see cref="bool"/> indicating the validity of the entity.</returns>
        public bool ValidateClass(string classId)
        {
            var value = Class.Contains(classId);
            return value;
        }
    }
}
