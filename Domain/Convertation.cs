using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace UserWallet.Domain
{
    public class Convertation
    {
    }
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class ValCurs
    {

        private ValCursValType[] valTypeField;

        private string dateField;

        private string nameField;

        private string descriptionField;

        /// <remarks/>
        [XmlElement("ValType")]
        public ValCursValType[] ValType
        {
            get
            {
                return this.valTypeField;
            }
            set
            {
                this.valTypeField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }


        /// <remarks/>
        [XmlAttribute()]
        public string Name
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

        /// <remarks/>
        [XmlAttribute()]
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class ValCursValType
    {

        private ValCursValTypeValute[] valuteField;

        private string typeField;

        /// <remarks/>
        [XmlElement("Valute")]
        public ValCursValTypeValute[] Valute
        {
            get
            {
                return this.valuteField;
            }
            set
            {
                this.valuteField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute()]
        public string Type
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
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public partial class ValCursValTypeValute
    {

        private string nominalField;

        private string nameField;

        private float valueField;

        private string codeField;

        /// <remarks/>
        public string Nominal
        {
            get
            {
                return this.nominalField;
            }
            set
            {
                this.nominalField = value;
            }
        }

        /// <remarks/>
        public string Name
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

        /// <remarks/>
        public float Value
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
        [XmlAttribute()]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }
}
