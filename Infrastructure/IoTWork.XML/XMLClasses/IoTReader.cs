﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace IoTWork.XML
{
    // 
    // Codice sorgente generato automaticamente da xsd, versione=4.6.1055.0.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class iotreader
    {

        private iotreaderDevice deviceField;

        private iotreaderManager managerField;

        private iotreaderDispatcher dispatcherField;

        private iotreaderSensor[] sensorsField;

        private iotreaderTrigger[] triggersField;

        private iotreaderChain[] chainsField;

        /// <remarks/>
        public iotreaderDevice device
        {
            get
            {
                return this.deviceField;
            }
            set
            {
                this.deviceField = value;
            }
        }

        /// <remarks/>
        public iotreaderManager manager
        {
            get
            {
                return this.managerField;
            }
            set
            {
                this.managerField = value;
            }
        }

        /// <remarks/>
        public iotreaderDispatcher dispatcher
        {
            get
            {
                return this.dispatcherField;
            }
            set
            {
                this.dispatcherField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("sensor", IsNullable = false)]
        public iotreaderSensor[] sensors
        {
            get
            {
                return this.sensorsField;
            }
            set
            {
                this.sensorsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("trigger", IsNullable = false)]
        public iotreaderTrigger[] triggers
        {
            get
            {
                return this.triggersField;
            }
            set
            {
                this.triggersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("chain", IsNullable = false)]
        public iotreaderChain[] chains
        {
            get
            {
                return this.chainsField;
            }
            set
            {
                this.chainsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDevice
    {

        private ushort networkField;

        private ushort ringField;

        private ushort regionField;

        private ushort idField;

        private string nameField;

        /// <remarks/>
        public ushort network
        {
            get
            {
                return this.networkField;
            }
            set
            {
                this.networkField = value;
            }
        }

        /// <remarks/>
        public ushort ring
        {
            get
            {
                return this.ringField;
            }
            set
            {
                this.ringField = value;
            }
        }

        /// <remarks/>
        public ushort region
        {
            get
            {
                return this.regionField;
            }
            set
            {
                this.regionField = value;
            }
        }

        /// <remarks/>
        public ushort id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderManager
    {

        private iotreaderManagerClient clientField;

        private iotreaderManagerFormatter formatterField;

        private iotreaderManagerCompressor compressorField;

        private iotreaderManagerSigner signerField;

        private ushort aliveTimeoutInMillisecondsField;

        /// <remarks/>
        public iotreaderManagerClient client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }

        /// <remarks/>
        public iotreaderManagerFormatter formatter
        {
            get
            {
                return this.formatterField;
            }
            set
            {
                this.formatterField = value;
            }
        }

        /// <remarks/>
        public iotreaderManagerCompressor compressor
        {
            get
            {
                return this.compressorField;
            }
            set
            {
                this.compressorField = value;
            }
        }

        /// <remarks/>
        public iotreaderManagerSigner signer
        {
            get
            {
                return this.signerField;
            }
            set
            {
                this.signerField = value;
            }
        }

        /// <remarks/>
        public ushort AliveTimeoutInMilliseconds
        {
            get
            {
                return this.aliveTimeoutInMillisecondsField;
            }
            set
            {
                this.aliveTimeoutInMillisecondsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderManagerClient
    {

        private string uriField;

        private string typeField;

        /// <remarks/>
        public string Uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderManagerFormatter
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderManagerCompressor
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderManagerSigner
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcher
    {

        private iotreaderDispatcherClient clientField;

        private iotreaderDispatcherFormatter formatterField;

        private iotreaderDispatcherCompressor compressorField;

        private iotreaderDispatcherSigner signerField;

        private iotreaderDispatcherThrottling throttlingField;

        /// <remarks/>
        public iotreaderDispatcherClient client
        {
            get
            {
                return this.clientField;
            }
            set
            {
                this.clientField = value;
            }
        }

        /// <remarks/>
        public iotreaderDispatcherFormatter formatter
        {
            get
            {
                return this.formatterField;
            }
            set
            {
                this.formatterField = value;
            }
        }

        /// <remarks/>
        public iotreaderDispatcherCompressor compressor
        {
            get
            {
                return this.compressorField;
            }
            set
            {
                this.compressorField = value;
            }
        }

        /// <remarks/>
        public iotreaderDispatcherSigner signer
        {
            get
            {
                return this.signerField;
            }
            set
            {
                this.signerField = value;
            }
        }

        /// <remarks/>
        public iotreaderDispatcherThrottling throttling
        {
            get
            {
                return this.throttlingField;
            }
            set
            {
                this.throttlingField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcherClient
    {

        private string uriField;

        private string typeField;

        /// <remarks/>
        public string Uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcherFormatter
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcherCompressor
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcherSigner
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderDispatcherThrottling
    {

        private uint valueField;

        private string typeField;

        /// <remarks/>
        public uint value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderSensor
    {

        private ushort uniqueIdField;

        private string uniqueNameField;

        private string triggerUniqueNameField;

        private string chainUniqueNameField;

        private string libraryPathField;

        private string parametersField;

        private string typeField;

        /// <remarks/>
        public ushort UniqueId
        {
            get
            {
                return this.uniqueIdField;
            }
            set
            {
                this.uniqueIdField = value;
            }
        }

        /// <remarks/>
        public string UniqueName
        {
            get
            {
                return this.uniqueNameField;
            }
            set
            {
                this.uniqueNameField = value;
            }
        }

        /// <remarks/>
        public string TriggerUniqueName
        {
            get
            {
                return this.triggerUniqueNameField;
            }
            set
            {
                this.triggerUniqueNameField = value;
            }
        }

        /// <remarks/>
        public string ChainUniqueName
        {
            get
            {
                return this.chainUniqueNameField;
            }
            set
            {
                this.chainUniqueNameField = value;
            }
        }

        /// <remarks/>
        public string LibraryPath
        {
            get
            {
                return this.libraryPathField;
            }
            set
            {
                this.libraryPathField = value;
            }
        }

        /// <remarks/>
        public string Parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderTrigger
    {

        private ushort uniqueIdField;

        private string uniqueNameField;

        private uint withIntervalInMillisecondsField;

        private bool repeatForeverField;

        private string typeField;

        /// <remarks/>
        public ushort UniqueId
        {
            get
            {
                return this.uniqueIdField;
            }
            set
            {
                this.uniqueIdField = value;
            }
        }

        /// <remarks/>
        public string UniqueName
        {
            get
            {
                return this.uniqueNameField;
            }
            set
            {
                this.uniqueNameField = value;
            }
        }

        /// <remarks/>
        public uint WithIntervalInMilliseconds
        {
            get
            {
                return this.withIntervalInMillisecondsField;
            }
            set
            {
                this.withIntervalInMillisecondsField = value;
            }
        }

        /// <remarks/>
        public bool RepeatForever
        {
            get
            {
                return this.repeatForeverField;
            }
            set
            {
                this.repeatForeverField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderChain
    {

        private ushort uniqueIdField;

        private string uniqueNameField;

        private iotreaderChainPipe[] pipesField;

        private string priorityField;

        /// <remarks/>
        public ushort UniqueId
        {
            get
            {
                return this.uniqueIdField;
            }
            set
            {
                this.uniqueIdField = value;
            }
        }

        /// <remarks/>
        public string UniqueName
        {
            get
            {
                return this.uniqueNameField;
            }
            set
            {
                this.uniqueNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pipe", IsNullable = false)]
        public iotreaderChainPipe[] pipes
        {
            get
            {
                return this.pipesField;
            }
            set
            {
                this.pipesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class iotreaderChainPipe
    {

        private uint withIntervalInMillisecondsField;

        private bool withIntervalInMillisecondsFieldSpecified;

        private string libraryPathField;

        private string parametersField;

        private ushort stageField;

        private string typeField;

        /// <remarks/>
        public uint WithIntervalInMilliseconds
        {
            get
            {
                return this.withIntervalInMillisecondsField;
            }
            set
            {
                this.withIntervalInMillisecondsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool WithIntervalInMillisecondsSpecified
        {
            get
            {
                return this.withIntervalInMillisecondsFieldSpecified;
            }
            set
            {
                this.withIntervalInMillisecondsFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string LibraryPath
        {
            get
            {
                return this.libraryPathField;
            }
            set
            {
                this.libraryPathField = value;
            }
        }

        /// <remarks/>
        public string Parameters
        {
            get
            {
                return this.parametersField;
            }
            set
            {
                this.parametersField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort stage
        {
            get
            {
                return this.stageField;
            }
            set
            {
                this.stageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }
}