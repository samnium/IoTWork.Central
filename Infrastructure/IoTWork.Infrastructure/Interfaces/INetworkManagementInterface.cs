using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Interfaces
{
    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkManagementInterface : INetworkPickerService, INetworkDeviceService,
        INetworkDeviceQueryService, INetworkDeviceNavigationService, INetworkDeviceTransferService, INetworkCacheService
    {

    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkPickerService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PickerStop", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void PickerStop(Int32 NetworkOrdinal, Int32 RegionOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PickerRestart", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void PickerRestart(Int32 NetworkOrdinal, Int32 RegionOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PickerStart", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void PickerStart(Int32 NetworkOrdinal, Int32 RegionOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PickerConfigureAndRestart", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void PickerConfigureAndRestart(Int32 NetworkOrdinal, Int32 RegionOrdinal);
    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkDeviceService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceConfigureAndRestart", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceConfigureAndRestart(int NetworkOrdinal, int RegionOrdinal, int DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceRestoreFactory", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceRestoreFactory(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceRestoreFactoryAndRestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceRestoreFactoryAndRestartDevice(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceRestartDevice", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceRestartDevice(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceRestartApplication", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceRestartApplication(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceStopAcquire", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceStopAcquire(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceRestartAcquire", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceRestartAcquire(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);
    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkDeviceQueryService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceAskForStatistics", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceAskForStatistics(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceAskForErrors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceAskForErrors(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceAskForAlive", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceAskForAlive(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceAskForUpTime", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceAskForUpTime(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);
    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkDeviceNavigationService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceListOfDllFilesForSensors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceListOfDllFilesForSensors(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceListOfDllFilesForPipes", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceListOfDllFilesForPipes(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);
    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkDeviceTransferService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceUploadRequestForDllFileForSensor", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceUploadRequestForDllFileForSensor(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceUploadRequestForDllFileForPipe", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceUploadRequestForDllFileForPipe(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceUploadConfigurationDeviceFile", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceUploadConfigurationDeviceFile(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeviceUploadConfigurationLogFile", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        void DeviceUploadConfigurationLogFile(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);
    }

    [ServiceContract(Namespace = "http://iotwork.iotnetwork/commands")]
    public interface INetworkCacheService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RegisteredDevices", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Tuple<int,int,int, IPEndPoint>> RegisteredDevices();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RegisteredPickers", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<Tuple<int, int, IPEndPoint>> RegisteredPickers();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDeviceStatistics", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<IoTWork.Protocol.Devices.Note> GetDeviceStatistics(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDeviceErrors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<IoTWork.Protocol.Devices.Note> GetDeviceErrors(Int32 NetworkOrdinal, Int32 RegionOrdinal, Int32 DeviceOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPickerStatistics", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<IoTWork.Protocol.Devices.Note> GetPickerStatistics(Int32 NetworkOrdinal, Int32 RegionOrdinal);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetPickerErrors", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        List<IoTWork.Protocol.Devices.Note> GetPickerErrors(Int32 NetworkOrdinal, Int32 RegionOrdinal);
    }
}
