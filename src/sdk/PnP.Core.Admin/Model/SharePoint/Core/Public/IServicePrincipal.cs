using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PnP.Core.Admin.Model.SharePoint
{
    /// <summary>
    /// Manage the SharePoint apps service principal
    /// </summary>
    public interface IServicePrincipal
    {
        /// <summary>
        /// Approves the specified permission request
        /// </summary>
        /// <param name="id">permission request id</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>permission grant</returns>
        [Obsolete("Use IApp.ApprovePermissionRequests instead")]
        IPermissionGrant ApprovePermissionRequest(string id, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Approves the specified permission request
        /// </summary>
        /// <param name="id">permission request id</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>permission grant</returns>
        [Obsolete("Use IApp.ApprovePermissionRequestsAsync instead")]
        Task<IPermissionGrant> ApprovePermissionRequestAsync(string id, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Denies the specified permission request
        /// </summary>
        /// <param name="id">permission request id</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        [Obsolete("Use RevokeGrant2 instead")]
        void DenyPermissionRequest(string id, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Denies the specified permission request
        /// </summary>
        /// <param name="id">permission request id</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        [Obsolete("Use RevokeGrant2Async instead")]
        Task DenyPermissionRequestAsync(string id, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Lists pending permission requests
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use ListGrants2 instead")]
        List<IPermissionRequest> GetPermissionRequests(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Lists pending permission requests
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use ListGrants2Async instead")]
        Task<List<IPermissionRequest>> GetPermissionRequestsAsync(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Enable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        Task<IServicePrincipalProperties> Enable2Async(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Enable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        IServicePrincipalProperties Enable2(VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Enable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use Enable2Async instead")]
        Task<IServicePrincipalProperties> EnableAsync(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Enable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use Enable2 instead")]
        IServicePrincipalProperties Enable(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Disable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        Task<IServicePrincipalProperties> Disable2Async(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Disable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        IServicePrincipalProperties Disable2(VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Disable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use Disable2Async instead")]
        Task<IServicePrincipalProperties> DisableAsync(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Disable the Service Principal
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of permission requests</returns>
        [Obsolete("Use Disable2 instead")]
        IServicePrincipalProperties Disable(VanityUrlOptions vanityUrlOptions = null);


        /// <summary>
        /// Get all granted permissions of the SharePoint Online Client Extensibility Web Application Principal  
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>The list of granted OAuth2 permissions</returns>
        IPermissionGrant2[] ListGrants2(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Get all granted permissions of the SharePoint Online Client Extensibility Web Application Principal  
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>The list of granted OAuth2 permissions</returns>
        Task<IPermissionGrant2[]> ListGrants2Async(VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Lists all granted Permissions
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of granted permissions</returns>
        [Obsolete("Use ListGrants2Async instead")]
        Task<IEnumerable<IPermissionGrant>> ListGrantsAsync(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Lists all granted Permissions
        /// </summary>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the list of granted permissions</returns>
        [Obsolete("Use ListGrants2 instead")]
        IEnumerable<IPermissionGrant> ListGrants(VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Grants scope on a resource
        /// </summary>
        /// <param name="resource">The name of the resource, e.g. Microsoft Graph</param>
        /// <param name="scope">The scope to grant, e.g. 'User.ReadBasic.All'</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>The resulting <see cref="IPermissionGrant2"/></returns>
        Task<IPermissionGrant2> AddGrant2Async(string resource, string scope, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Grants scope on a resource
        /// </summary>
        /// <param name="resource">The name of the resource, e.g. Microsoft Graph</param>
        /// <param name="scope">The scope to grant, e.g. 'User.ReadBasic.All'</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>The resulting <see cref="IPermissionGrant2"/></returns>
        IPermissionGrant2 AddGrant2(string resource, string scope, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Grants a Permission
        /// </summary>
        /// <param name="resource">Resource of the permission</param>
        /// <param name="scope">Scope of the permission</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the new permission grant</returns>
        [Obsolete("Use AddGrant2Async instead")]
        Task<IPermissionGrant> AddGrantAsync(string resource, string scope, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        /// Grants a Permission
        /// </summary>
        /// <param name="resource">Resource of the permission</param>
        /// <param name="scope">Scope of the permission</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the new permission grant</returns>
        [Obsolete("Use AddGrant2 instead")]
        IPermissionGrant AddGrant(string resource, string scope, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        ///  Removed a scope from an existing grant
        /// </summary>
        /// <param name="grantId">The id of the grant</param>
        /// <param name="scope">The scope to remove from the grant denoted by grantId</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>The updated <see cref="IPermissionGrant2"/>, or null if the last scope was removed from the grant</returns>
        Task<IPermissionGrant2> RevokeGrant2Async(string grantId, string scope, VanityUrlOptions vanityUrlOptions = null);

        /// <summary>
        ///  Removed a scope from an existing grant
        /// </summary>
        /// <param name="grantId">The id of the grant</param>
        /// <param name="scope">The scope to remove from the grant denoted by grantId</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>The updated <see cref="IPermissionGrant2"/>, or null if the last scope was removed from the grant</returns>
        IPermissionGrant2 RevokeGrant2(string grantId, string scope, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Deletes the whole grant with all scopes.
        /// </summary>
        /// <param name="grantId">The id of the grant</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>void</returns>
        Task DeleteGrant2Async(string grantId, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Deletes the whole grant with all scopes.
        /// </summary>
        /// <param name="grantId">The id of the grant</param>
        /// <param name="vanityUrlOptions"></param>
        /// <returns>void</returns>
        void DeleteGrant2(string grantId, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Revokes a Permission
        /// </summary>
        /// <param name="objectId">Object Id of the permission</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the revoked permission grant</returns>
        [Obsolete("Use RevokeGrant2Async instead")]
        Task<IPermissionGrant> RevokeGrantAsync(string objectId, VanityUrlOptions vanityUrlOptions = null);
        
        /// <summary>
        /// Revokes a Permission
        /// </summary>
        /// <param name="objectId">Object Id of the permission</param>
        /// <param name="vanityUrlOptions">Optionally specify the custom vanity URI's used by this tenant</param>
        /// <returns>the revoked permission grant</returns>
        [Obsolete("Use RevokeGrant2 instead")]
        IPermissionGrant RevokeGrant(string objectId, VanityUrlOptions vanityUrlOptions = null);
    }
}
