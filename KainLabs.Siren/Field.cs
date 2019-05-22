﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace KainLabs.Siren
{
    /// <summary>
    /// Fields represent controls inside of actions.
    /// </summary>
    public class Field
    {
        public Field() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class with required properties.
        /// </summary>
        /// <param name="name">Name of the field</param>
        public Field(string name)
        {
            Name = name;
            Type = FieldTypes.Text;
            Value = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class with required properties.
        /// </summary>
        /// <param name="name">Name of the field</param>
        /// <param name="@type">Type of the field</param>
        public Field(string name, FieldTypes @type)
        {
            Name = name;
            Type = @type;
            Value = null;
        }

        /// <summary>
        /// Gets or sets the name of the field
        /// </summary>
        /// <remarks>
        /// A name describing the control. Required
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the field type.
        /// </summary>
        /// <remarks>
        /// The field type. This may include any of the input types specified in HTML5.
        /// When missing, the default value is text. Serialization of these fields will 
        /// depend on the value of the action's type attribute. See type under Actions, 
        /// above. Optional.
        /// </remarks>
        [JsonConverter(typeof(FieldTypes.FieldTypesJsonConverter))]
        public FieldTypes Type { get; set; }

        /// <summary>
        /// Gets or sets the default value of the field.
        /// </summary>
        /// <remarks>
        /// A value assigned to the field. Optional.
        /// </remarks>
        public object Value { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> ExtensionData { get; } = new Dictionary<string, object>();
    }
}
