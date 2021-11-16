using Microsoft.ML.Data;

namespace AnomalyDetection.Common
{
    public class ModelInput
    {
        [LoadColumn(0), NoColumn, ColumnName(@"UDI") ]
        public float UDI { get; set; }

        [LoadColumn(1), ColumnName(@"Product ID")]
        public string Product_ID { get; set; }

        [LoadColumn(2), ColumnName(@"Type")]
        public string Type { get; set; }

        [LoadColumn(3), ColumnName(@"Air temperature")]
        public float Air_temperature { get; set; }

        [LoadColumn(4), ColumnName(@"Process temperature")]
        public float Process_temperature { get; set; }

        [LoadColumn(5), ColumnName(@"Rotational speed")]
        public float Rotational_speed { get; set; }

        [LoadColumn(6), ColumnName(@"Torque")]
        public float Torque { get; set; }

        [LoadColumn(7), ColumnName(@"Tool wear")]
        public float Tool_wear { get; set; }

        [LoadColumn(8), ColumnName(@"Machine failure")]
        public float Machine_failure { get; set; }

        [LoadColumn(9),NoColumn, ColumnName(@"TWF")]
        public float TWF { get; set; }

        [LoadColumn(10), NoColumn, ColumnName(@"HDF")]
        public float HDF { get; set; }

        [LoadColumn(11), NoColumn, ColumnName(@"PWF")]
        public float PWF { get; set; }

        [LoadColumn(12), NoColumn, ColumnName(@"OSF")]
        public float OSF { get; set; }

        [LoadColumn(13), NoColumn, ColumnName(@"RNF")]
        public float RNF { get; set; }
    }
}