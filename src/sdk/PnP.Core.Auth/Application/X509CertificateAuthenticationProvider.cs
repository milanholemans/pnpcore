﻿using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using PnP.Core.Auth.Services.Builder.Configuration;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PnP.Core.Auth
{
    /// <summary>
    /// Authentication Provider based on the X.509 Certificate
    /// </summary>
    public sealed class X509CertificateAuthenticationProvider : OAuthAuthenticationProvider
    {
        /// <summary>
        /// The X.509 Certificate to use for authentication
        /// </summary>
        public X509Certificate2 Certificate { get; set; }

        // Instance private member, to keep the token cache at service instance level
        private IConfidentialClientApplication confidentialClientApplication;

        /// <summary>
        /// Public constructor for external users
        /// </summary>
        public X509CertificateAuthenticationProvider(string clientId, string tenantId,
            StoreName storeName, StoreLocation storeLocation, string thumbprint,
            ILogger<OAuthAuthenticationProvider> logger)
            : base(logger)
        {
            this.Init(new PnPCoreAuthenticationCredentialConfigurationOptions
            {
                ClientId = clientId,
                TenantId = tenantId,
                X509Certificate = new PnPCoreAuthenticationX509CertificateOptions
                {
                    StoreName = storeName,
                    StoreLocation = storeLocation,
                    Thumbprint = thumbprint
                }
            });
        }

        /// <summary>
        /// Public constructor leveraging DI to initialize the ILogger interfafce
        /// </summary>
        /// <param name="logger">The instance of the logger service provided by DI</param>
        public X509CertificateAuthenticationProvider(ILogger<OAuthAuthenticationProvider> logger)
            : base(logger)
        {
        }

        /// <summary>
        /// Initializes the X509Certificate Authentication Provider
        /// </summary>
        /// <param name="options">The options to use</param>
        internal override void Init(PnPCoreAuthenticationCredentialConfigurationOptions options)
        {
            // We need the X509Certificate options
            if (options.X509Certificate == null)
            {
                throw new ConfigurationErrorsException(
                    PnPCoreAuthResources.X509CertificateAuthenticationProvider_InvalidConfiguration);
            }

            // We need the certificate thumbprint
            if (string.IsNullOrEmpty(options.X509Certificate.Thumbprint))
            {
                throw new ConfigurationErrorsException(PnPCoreAuthResources.X509CertificateAuthenticationProvider_LogInit);
            }

            ClientId = !string.IsNullOrEmpty(options.ClientId) ? options.ClientId : AuthGlobals.DefaultClientId;
            TenantId = !string.IsNullOrEmpty(options.TenantId) ? options.TenantId : AuthGlobals.OrganizationsTenantId;
            Certificate = X509CertificateUtility.LoadCertificate(
                options.X509Certificate.StoreName,
                options.X509Certificate.StoreLocation,
                options.X509Certificate.Thumbprint);

            // Log the initialization information
            this.Log?.LogInformation(PnPCoreAuthResources.X509CertificateAuthenticationProvider_LogInit,
                options.X509Certificate.Thumbprint,
                options.X509Certificate.StoreName,
                options.X509Certificate.StoreLocation);

            // Build the MSAL client
            if (TenantId.Equals(AuthGlobals.OrganizationsTenantId, StringComparison.InvariantCultureIgnoreCase))
            {
                confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(ClientId)
                    .WithAuthority(AadAuthorityAudience.AzureAdMultipleOrgs)
                    .WithCertificate(Certificate)
                    .Build();
            }
            else
            {
                confidentialClientApplication = ConfidentialClientApplicationBuilder
                    .Create(ClientId)
                    .WithTenantId(TenantId)
                    .WithCertificate(Certificate)
                    .Build();
            }
        }

        /// <summary>
        /// Authenticates the specified request message.
        /// </summary>
        /// <param name="resource">Request uri</param>
        /// <param name="request">The <see cref="HttpRequestMessage"/> to authenticate.</param>
        /// <returns>The task to await.</returns>
        public override async Task AuthenticateRequestAsync(Uri resource, HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer",
                await GetAccessTokenAsync(resource).ConfigureAwait(false));
        }

        /// <summary>
        /// Gets an access token for the requested resource and scope
        /// </summary>
        /// <param name="resource">Resource to request an access token for</param>
        /// <param name="scopes">Scopes to request</param>
        /// <returns>An access token</returns>
        public override async Task<string> GetAccessTokenAsync(Uri resource, string[] scopes)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            if (scopes == null)
            {
                throw new ArgumentNullException(nameof(scopes));
            }

            AuthenticationResult tokenResult = null;

            try
            {
                // Try to get the token from the tokens cache
                tokenResult = await confidentialClientApplication.AcquireTokenForClient(scopes).ExecuteAsync().ConfigureAwait(false);
            }
            catch (MsalServiceException)
            {
                // Handle the various exceptions
                throw;
            }

            // Log the access token retrieval action
            this.Log?.LogInformation(PnPCoreAuthResources.X509CertificateAuthenticationProvider_LogAccessTokenRetrieval,
                resource, scopes.Aggregate(string.Empty, (c, n) => c + ", " + n).TrimEnd(','));

            // Return the Access Token, if we've got it
            // In case of any exception while retrieving the access token, 
            // MSAL will throw an exception that we simply bubble up
            return tokenResult.AccessToken;
        }

        /// <summary>
        /// Gets an access token for the requested resource 
        /// </summary>
        /// <param name="resource">Resource to request an access token for</param>
        /// <returns>An access token</returns>
        public override async Task<string> GetAccessTokenAsync(Uri resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException(nameof(resource));
            }

            // Use the .default scope if the scopes are not provided
            return await GetAccessTokenAsync(resource,
                new string[] { $"{resource.Scheme}://{resource.Authority}/.default" }).ConfigureAwait(false);
        }
    }
}
