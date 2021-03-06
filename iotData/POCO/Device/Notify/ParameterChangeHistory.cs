﻿using iotDbConnector.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace iotDatabaseConnector.DAL.POCO.Device.Notify
{
    [DataContract(IsReference = true)]
    public class ParameterChangeHistory
    {
        [Key]
        [Required]
        [DataMember]
        public int ParameterChangeId { get; set; }

        [Required]
        [DataMember]
        public virtual DeviceParameter Property { get; set; }

        [Required]
        [DataMember]
        public DateTime  Date { get; set; }

        [Required]
        [DataMember]
        public string Value { get; set; }

        public ParameterChangeHistory()
        {
            Date = DateTime.Now;
        }
       
    }
}
