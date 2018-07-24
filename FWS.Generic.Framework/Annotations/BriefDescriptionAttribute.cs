using System;

namespace FWS.Generic.Framework.Annotations
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class BriefDescriptionAttribute : Attribute
    {
        private readonly string _description;
        /// <summary>
        /// Gets the brief description stored in this attribute.
        /// </summary>
        public string Description
        {
            get
            {
                return this._description;
            }
        }

        /// <summary>
        /// Creates new brief description attribute.
        /// </summary>
        /// <param name="description">The description to store in this attribute.</param>
        public BriefDescriptionAttribute(string description)
            : base()
        {
            this._description = description;
        }
    }
}