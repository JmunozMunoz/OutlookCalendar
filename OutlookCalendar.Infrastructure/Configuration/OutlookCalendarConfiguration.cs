
using OutlookCalendar.Domain.Core.Models;
using System;
using System.Configuration;
using System.Xml;

namespace Dale.Services.EntFileForATM.Infraestructure.Extensions.Configuration
{
    public class OutlookCalendarConfiguration : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var config = new OutlookCalendarConfiguration();

            config.OAuth2Model = new OAuth2Model();

            // Email Config
            var emailSettings = section.SelectSingleNode("OAuth2");
            config.OAuth2Model.ClientId = GetString(emailSettings, "ClientId");
            config.OAuth2Model.ClientSecret = GetString(emailSettings, "ClientSecret");
            config.OAuth2Model.ListenPort = GetInt(emailSettings, "ListenPort");
            config.OAuth2Model.AuthorizationEndpoint = GetString(emailSettings, "AuthorizationEndpoint");
            config.OAuth2Model.TokenEndpoint = GetString(emailSettings, "TokenEndpoint");
            config.OAuth2Model.Scope = GetString(emailSettings, "Scope");
            return config;
        }

        public OAuth2Model OAuth2Model { get; set; }

        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement(node, attrName, Convert.ToString);
        }
        private int GetInt(XmlNode node, string attrName)
        {
            return SetByXElement(node, attrName, Convert.ToInt32);
        }
        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node?.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

    }
}
