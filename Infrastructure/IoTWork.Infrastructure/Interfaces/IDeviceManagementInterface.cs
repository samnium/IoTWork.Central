using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.ServiceModel;
using System.IO;
using System.ServiceModel.Web;

namespace IoTWork.Infrastructure.Interfaces
{
    //
    // http://stackoverflow.com/questions/1935040/how-to-handle-large-file-uploads-via-wcf
    // http://www.c-sharpcorner.com/uploadfile/dhananjaycoder/multiple-service-contracts-in-wcf-service/
    // https://msdn.microsoft.com/en-us/library/ms731913.aspx
    // https://msdn.microsoft.com/en-us/library/ms789010.aspx
    //

    [ServiceContract(Namespace = "http://iotwork.iotreader/commands")]
    public interface IDeviceManagementInterface: IDeviceService, IQueryService, INavigationService, ITransferService
    {

    }


    [ServiceContract(Namespace = "http://iotwork.iotreader/commands")]
    public interface IDeviceService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void RestoreFactory();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void RestoreFactoryAndRestartDevice();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void RestartDevice();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartApplication", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void RestartApplication();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "StopAcquire", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void StopAcquire();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RestartAcquire", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void RestartAcquire();
    }

    [ServiceContract(Namespace = "http://iotwork.iotreader/commands")]
    public interface IQueryService
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

    [ServiceContract(Namespace = "http://iotwork.iotreader/commands")]
    public interface INavigationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListOfDllFilesForSensors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Tuple<string, DateTime>> ListOfDllFilesForSensors();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListOfDllFilesForPipes", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Tuple<string, DateTime>> ListOfDllFilesForPipes();
    }

    [ServiceContract(Namespace = "http://iotwork.iotreader/commands")]
    public interface ITransferService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UploadRequestForDllFileForSensor", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void UploadRequestForDllFileForSensor(String FilePosition, String Signature, Boolean SkipIfPresent);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UploadRequestForDllFileForPipe", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void UploadRequestForDllFileForPipe(String FilePosition, String Signature, Boolean SkipIfPresent);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UploadConfigurationDeviceFile", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void UploadConfigurationDeviceFile(String stream, String Signature);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UploadConfigurationLogFile", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void UploadConfigurationLogFile(String stream, String Signature);
    }
}
