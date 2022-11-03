using ProgettoIngegneriaSoftware.Models.DB_Models.Authentication;

namespace ProgettoIngegneriaSoftware.Services
{
    public interface ILoginTokenModelService
    {

        #region CREATE

        /// <summary>
        /// Asynchronously creates a new <see cref="LoginTokenModel"/> and adds it to the <see cref="UserModel"/> instance retrieved from the data source using for reference the <see cref="UserModel"/> parameter.
        /// </summary>
        /// <param name="userModel">The <see cref="UserModel"/> used to filter the data source where it's going to be added the new instance of <see cref="LoginTokenModel"/>.</param>
        /// <returns>the newly created <see cref="LoginTokenModel"/> instance or <c>null</c> if no <see cref="UserModel"/> has been found in the data source.</returns>
        public Task<LoginTokenModel?> CreateAsync(UserModel userModel);
        /// <summary>
        /// Asynchronously creates a new <see cref="LoginTokenModel"/> and adds it to the <see cref="UserModel"/> instance retrieved from the data source using the <c>Username</c> property to filter it.
        /// </summary>
        /// <param name="username">The <see cref="UserModel"/>'s Username property value used to filter the data source where it's going to be added the new instance of <see cref="LoginTokenModel"/>.</param>
        /// <returns>the newly created <see cref="LoginTokenModel"/> instance or <c>null</c> if no <see cref="UserModel"/> with that Username has been found in the data source.</returns>
        public Task<LoginTokenModel?> CreateAsync(string username);
        /// <summary>
        /// Asynchronously creates a new <see cref="LoginTokenModel"/> and adds it to the <see cref="UserModel"/> instance retrieved from the data source using the Id property value.
        /// </summary>
        /// <param name="userModelId">The <see cref="UserModel"/>'s Id property value used to filter the data source where it's going to be added the new instance of <see cref="LoginTokenModel"/>.</param>
        /// <returns>the newly created <see cref="LoginTokenModel"/> instance or <c>null</c> if no <see cref="UserModel"/>with that Id has been found in the data source.</returns>
        public Task<LoginTokenModel?> CreateAsync(Guid userModelId);

        #endregion CREATE

        #region READ

        /// <summary>
        /// Asynchronously gets an instance of <see cref="LoginTokenModel"/> by its Id from the data source.
        /// </summary>
        /// <param name="id">the <see cref="LoginTokenModel"/>'s Id property value used to filter the data source.</param>
        /// <returns>the instance of <see cref="LoginTokenModel"/> with that Id or <c>null</c> if no <see cref="LoginTokenModel"/> has been found in the data source.</returns>
        public Task<LoginTokenModel?> GetAsync(int id);
        /// <summary>
        /// Asynchronously gets an instance of <see cref="LoginTokenModel"/> using its Token property value to filter the data source.
        /// </summary>
        /// <param name="loginTokenValue">the <see cref="LoginTokenModel"/>'s Token property value used to filter the data source.</param>
        /// <returns>the instance of <see cref="LoginTokenModel"/> with that Token property value or <c>null</c> if no <see cref="LoginTokenModel"/> with that Token property value has been found in the data source.</returns>
        public Task<LoginTokenModel?> GetAsync(string loginTokenValue);
        /// <summary>
        /// Tells if a <see cref="LoginTokenModel"/> with a specific Token property value exists in the data source.
        /// </summary>
        /// <param name="loginTokenValue">The <see cref="LoginTokenModel"/> Token property value used to filter the data source.</param>
        /// <returns><c>true</c> if exists an instance of <see cref="LoginTokenModel"/> with that specific Token property value
        /// or <c>false</c> if it does not exists in the data source.</returns>
        public Task<bool> Exists(string loginTokenValue);
        /// <summary>
        /// Tells if a <see cref="LoginTokenModel"/> with a specific Id exists in the data source.
        /// </summary>
        /// <param name="id">The <see cref="LoginTokenModel"/> Id property value used to filter the data source.</param>
        /// <returns><c>true</c> if exists an instance of <see cref="LoginTokenModel"/> with that Id 
        /// or <c>false</c> if it does not exists in the data source.</returns>
        public Task<bool> Exists(int id);

        public Task<bool> IsValidAsync(string username, string loginToken);

        #endregion READ

        #region UPDATE

        /// <summary>
        /// Asynchronously updates an instance of <see cref="LoginTokenModel"/> with values from another instance of <see cref="LoginTokenModel"/> filtering the data source with the given Id.
        /// </summary>
        /// <param name="id">The Id to filter the data source.</param>
        /// <param name="loginTokenModel">The <see cref="LoginTokenModel"/> used as reference to update the values.</param>
        /// <returns>The updated <see cref="LoginTokenModel"/> instance or <c>null</c> if no <see cref="LoginTokenModel"/> with that Id has been found in the data source.</returns>
        public Task<LoginTokenModel?> UpdateAsync(int id, LoginTokenModel loginTokenModel);
        /// <summary>
        /// Asynchronously updates an instance of <see cref="LoginTokenModel"/> with values from another instance of <see cref="LoginTokenModel"/> filtering the data source with the instance Id.
        /// </summary>
        /// <param name="loginTokenModel">The <see cref="LoginTokenModel"/> used as reference to update the values.</param>
        /// <returns>The updated <see cref="LoginTokenModel"/> instance or <c>null</c> if no <see cref="LoginTokenModel"/> with that Id has been found in the data source.</returns>
        public Task<LoginTokenModel?> UpdateAsync(LoginTokenModel loginTokenModel);
        /// <summary>
        /// Asynchronously updates an instance of <see cref="LoginTokenModel"/> with values from another instance of <see cref="LoginTokenModel"/> filtering the data source with the instance Id.
        /// </summary>
        /// <param name="loginTokenValue">The <see cref="LoginTokenModel"/>'s Token property value to filter the data source.</param>
        /// <param name="loginTokenModel">The <see cref="LoginTokenModel"/> used as reference to update the values.</param>
        /// <returns>The updated <see cref="LoginTokenModel"/> instance
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> with that Token property value has been found in the data source.</returns>
        public Task<LoginTokenModel?> UpdateAsync(string loginTokenValue, LoginTokenModel loginTokenModel);
        /// <summary>
        /// Asynchronously sets as expired a <see cref="LoginTokenModel"/> instance in the data source.
        /// </summary>
        /// <param name="loginTokenValue">The <see cref="LoginTokenModel"/>'s Token property value used to filter the data source.</param>
        /// <returns>the expired instance of <see cref="LoginTokenModel"/> 
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> with that Token property value has been found in the data source.</returns>
        public Task<LoginTokenModel?> ExpireAsync(string loginTokenValue);
        /// <summary>
        /// Asynchronously sets as expired a <see cref="LoginTokenModel"/> instance in the data source.
        /// </summary>
        /// <param name="id">The <see cref="LoginTokenModel"/>'s Id used to filter the data source.</param>
        /// <returns>the expired instance of <see cref="LoginTokenModel"/> 
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> with that Id has been found in the data source.</returns>
        public Task<LoginTokenModel?> ExpireAsync(int id);

        #endregion UPDATE

        #region DELETE

        /// <summary>
        /// Asynchronously deletes an instance of <see cref="LoginTokenModel"/> from the data source filtering it by its Id.
        /// </summary>
        /// <param name="id">The <see cref="LoginTokenModel"/> Id to filter the data source.</param>
        /// <returns>the deleted <see cref="LoginTokenModel"/>
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> with that Id has been found in the data source.</returns>
        public Task<LoginTokenModel?> DeleteAsync(int id);
        /// <summary>
        /// Asynchronously deletes an instance of <see cref="LoginTokenModel"/> from the data source filtering it by its Token property.
        /// </summary>
        /// <param name="loginTokenValue">The <see cref="LoginTokenModel"/>'s Token property value to filter the data source.</param>
        /// <returns>the deleted <see cref="LoginTokenModel"/>
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> with that Token property value has been found in the data source.</returns>
        public Task<LoginTokenModel?> DeleteAsync(string loginTokenValue);
        /// <summary>
        /// Asynchronously deletes an instance of <see cref="LoginTokenModel"/> from the data source.
        /// </summary>
        /// <param name="loginTokenModel">The <see cref="LoginTokenModel"/> Instance used to filter the data source.</param>
        /// <returns>the deleted <see cref="LoginTokenModel"/>
        /// or <c>null</c> if no <see cref="LoginTokenModel"/> has been found in the data source.</returns>
        public Task<LoginTokenModel?> DeleteAsync(LoginTokenModel loginTokenModel);

        #endregion DELETE

    }
}
