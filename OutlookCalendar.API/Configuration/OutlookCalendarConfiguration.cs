using Microsoft.Extensions.Configuration;
using OutlookCalendar.API.Extensions;
using OutlookCalendar.Domain.Core.Models;
using OutlookCalendar.Domain.Core.Repositories;


namespace OutlookCalendar.API.Configuration
{
    public class OutlookCalendarConfiguration : IOutlookCalendarConfiguration
    {
        /// <summary>
        /// Interfaz Configuración
        /// </summary>
        private readonly IConfiguration _configuration;

        public OAuth2Model OAuth2Model { get; private set; }


        /// <summary>
        /// Constructor Configuración Migración Tarjetas
        /// </summary>
        /// <param name="configuration">Configurador</param>
        public OutlookCalendarConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            OAuth2Model = new OAuth2Model();
            LoadCustomSettings();
        }
        /// <summary>
        /// Carga Configuracion Presonalizada
        /// </summary>
        private void LoadCustomSettings()
        {
            var _oAuth2Model = _configuration.GetOptions<OAuth2Model>("AwsSettings");

            OAuth2Model.AuthorizationEndpoint = _oAuth2Model.AuthorizationEndpoint;
            OAuth2Model.ClientId = _oAuth2Model.ClientId;
            OAuth2Model.ClientSecret = _oAuth2Model.ClientSecret;
            OAuth2Model.ListenPort = _oAuth2Model.ListenPort;
            OAuth2Model.Scope = _oAuth2Model.Scope;
            OAuth2Model.TokenEndpoint = _oAuth2Model.TokenEndpoint;
        }
    }
}
