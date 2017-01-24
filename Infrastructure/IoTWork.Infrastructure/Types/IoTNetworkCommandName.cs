using IoTWork.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Types
{
    [DataContract]
    [Serializable]
    public enum IoTNetworkCommandName
    {
        #region public
        [EnumMember]
        Pck_StopPicker,
        [EnumMember]
        Pck_RestartPicker,
        [EnumMember]
        Pck_StartPicker,
        [EnumMember]
        Pck_ConfigureAndRestartPicker,

        [EnumMember]
        Dvc_ConfigureAndRestartDevice,
        [EnumMember]
        Dvc_RestoreFactory,
        [EnumMember]
        Dvc_RestoreFactoryAndRestartDevice,
        [EnumMember]
        Dvc_RestartDevice,
        [EnumMember]
        Dvc_RestartApplication,
        [EnumMember]
        Dvc_StopAcquire,
        [EnumMember]
        Dvc_RestartAcquire,

        [EnumMember]
        Dvc_AskForStatistics,
        [EnumMember]
        Dvc_AskForErrors,
        [EnumMember]
        Dvc_AskForAlive,
        [EnumMember]
        Dvc_AskForUpTime,

        [EnumMember]
        Dvc_ListOfDllFilesForSensors,
        [EnumMember]
        Dvc_ListOfDllFilesForPipes,

        [EnumMember]
        Dvc_UploadRequestForDllFileForSensor,
        [EnumMember]
        Dvc_UploadRequestForDllFileForPipe,
        [EnumMember]
        Dvc_UploadConfigurationDeviceFile,
        [EnumMember]
        Dvc_UploadConfigurationLogFile
        #endregion
    }
}

