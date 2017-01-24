using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Interfaces
{
    [ServiceContract(Namespace = "http://iotwork.iotpicker/commands")]
    public interface IPickerManagementInterface : IPickerService, IPickerQueryService
    {

    }

    [ServiceContract(Namespace = "http://iotwork.iotpicker/commands")]
    public interface IPickerService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void Stop();
    }


    [ServiceContract(Namespace = "http://iotwork.iotpicker/commands")]
    public interface IPickerQueryService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AskForStatistics", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void AskForStatistics();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AskForErrors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void AskForErrors();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AskForAlive", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void AskForAlive();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AskForUpTime", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void AskForUpTime();
    }
}
