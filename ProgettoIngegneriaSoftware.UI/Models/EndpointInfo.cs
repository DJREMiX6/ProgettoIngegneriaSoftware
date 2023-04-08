namespace ProgettoIngegneriaSoftware.UI.Models;

public class EndpointInfo
{

    #region CTORS

    public EndpointInfo(string controllerName, string name, string endpoint, string httpMethod)
    {
        ControllerName = controllerName;
        Name = name;
        Endpoint = endpoint;
        HttpMethod = httpMethod;
    }

    #endregion CTORS

    #region PROPS

    public string ControllerName { get; private set; }
    public string Name { get; private set; }
    public string Endpoint { get; private set; }
    public string HttpMethod { get; private set; }

    #endregion PROPS

}