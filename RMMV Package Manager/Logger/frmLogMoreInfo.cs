using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Indieteur.BasicLoggingSystem;

namespace RMMV_PackMan
{
    public partial class frmLogMoreInfo : Form
    {
        const string TIME_STAMP_TEXT_FORMAT = "ddd, dd MMMM yyyy HH:mm:ss.fff";
        public frmLogMoreInfo()
        {
            InitializeComponent();
        }

        public frmLogMoreInfo(LogMessage logMessage)
        {
            InitializeComponent();
            if (logMessage.HasDateAndTime)
            {
                lblTimeStamp.Enabled = true;
                txtTimeStamp.Enabled = true;
                txtTimeStamp.Text = logMessage.TimeStamp.ToString(TIME_STAMP_TEXT_FORMAT);
               
            }
            if (!string.IsNullOrWhiteSpace(logMessage.Message))
            {
                txtMessage.Enabled = true;
                txtMessage.Text = logMessage.Message;
                lblMessage.Enabled = true;
            }

            if (logMessage is LogMessageWLevel logMessageWLevel)
            {
                comboLogLevel.SelectedIndex = LogLevelToComboIndex(logMessageWLevel.LogLevel);
            }
            else
            {
                comboLogLevel.SelectedIndex = 0;
                return;
            }

            if (logMessage is DebugLogMessageLevel debugLogMessageLevel)
            {
                if (!string.IsNullOrWhiteSpace(debugLogMessageLevel.Namespace))
                {
                    lblNamespace.Enabled = true;
                    txtNamespace.Enabled = true;
                    txtNamespace.Text = debugLogMessageLevel.Namespace;
                }
                if (debugLogMessageLevel.AdditionalMessages != null && debugLogMessageLevel.AdditionalMessages.Length > 0)
                {
                    groupAdditionalMessage.Enabled = true;
                    comboAdditionalMessagesIndex.Enabled = true;
                    lblIndex.Enabled = true;
                    for (int i = 0; i < debugLogMessageLevel.AdditionalMessages.Length; ++i)
                        comboAdditionalMessagesIndex.Items.Add(new IntAndString(debugLogMessageLevel.AdditionalMessages[i], i));
                    comboAdditionalMessagesIndex.SelectedIndex = 0;
                }
                if (debugLogMessageLevel.Exception != null)
                {
                    Type exceptionType = debugLogMessageLevel.Exception.GetType();
                    lblExceptionType.Enabled = true;
                    txtExceptionType.Enabled = true;
                    txtExceptionType.Text = exceptionType.ToString();
                    if (!string.IsNullOrWhiteSpace(debugLogMessageLevel.Exception.Message))
                    {
                        lblExceptionMessage.Enabled = true;
                        txtExceptionMessage.Enabled = true;
                        txtExceptionMessage.Text = debugLogMessageLevel.Exception.Message;
                    }
                    PropertyInfo[] propertyInfoArray = exceptionType.GetProperties();
                    FieldInfo[] fieldInfoArray = exceptionType.GetFields();
                    if (propertyInfoArray != null && propertyInfoArray.Length > 0)
                    {
                        groupExceptionFields.Enabled = true;
                        lblExceptionField.Enabled = true;
                        comboExceptionFields.Enabled = true;
                        foreach (PropertyInfo pInfo in propertyInfoArray)
                        {
                            object value = pInfo.GetValue(debugLogMessageLevel.Exception);
                            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                                comboExceptionFields.Items.Add(new StringAndString(pInfo.Name, value.ToString()));
                        }
                      
                    }
                    if (fieldInfoArray != null && fieldInfoArray.Length > 0)
                    {
                        groupExceptionFields.Enabled = true;
                        lblExceptionField.Enabled = true;
                        comboExceptionFields.Enabled = true;
                        foreach (FieldInfo fInfo in fieldInfoArray)
                        {
                            object value = fInfo.GetValue(debugLogMessageLevel.Exception);
                            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                                comboExceptionFields.Items.Add(new StringAndString(fInfo.Name, value.ToString()));
                        }
                    }
                    if (comboExceptionFields.Items.Count > 0)
                        comboExceptionFields.SelectedIndex = 0;

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        int LogLevelToComboIndex(BasicLoggerLogLevel logLevel)
        {
            switch (logLevel)
            {
                case BasicLoggerLogLevel.CriticalError:
                    return 3;
                case BasicLoggerLogLevel.Error:
                    return 2;
                case BasicLoggerLogLevel.Information:
                    return 0;
                default:
                    return 1;
            }
        }

        class IntAndString
        {
            public string String { get; set; }
            public int Integer { get; set; }

            public IntAndString(string @string, int integer)
            {
                String = @string;
                Integer = integer;
            }

            public override string ToString()
            {
                return (Integer + 1).ToString();
            }
        }

        class StringAndString
        {
            public string String1 { get; set; }
            public string String2 { get; set; }
            public StringAndString(string string1, string string2)
            {
                String1 = string1;
                String2 = string2;
            }

            public override string ToString()
            {
                return String1;
            }
        }

        private void comboAdditionalMessagesIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntAndString tag = comboAdditionalMessagesIndex.SelectedItem as IntAndString;
            if (!string.IsNullOrWhiteSpace(tag.String))
            {
                lblAdditionalMessage.Enabled = true;
                txtAdditionalMessages.Enabled = true;
                txtAdditionalMessages.Text = tag.String;
            }
            else
            {
                lblAdditionalMessage.Enabled = false;
                txtAdditionalMessages.Enabled = false;
                txtAdditionalMessages.Text = string.Empty;
            }
            
        }

        private void comboExceptionFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            StringAndString tag = comboExceptionFields.SelectedItem as StringAndString;
            if (!string.IsNullOrWhiteSpace(tag.String2))
            {
                lblExceptionFieldMessage.Enabled = true;
                txtExceptionFieldMessage.Enabled = true;
                txtExceptionFieldMessage.Text = tag.String2;
            }
            else
            {
                lblExceptionFieldMessage.Enabled = false;
                txtExceptionFieldMessage.Enabled = false;
                txtExceptionFieldMessage.Text = string.Empty;
            }
        }
    }
}
